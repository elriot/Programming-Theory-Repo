using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	public GameObject[] BallPrefabs;
	[SerializeField] private int ballPrefabsIndexRange;
	public int TotalPoint;
	public Vector3 SpawnPos;
	public TMP_Text CurrentScoreText;
	public TMP_Text BestScoreText;
	public GameObject GameOverScreen;
	private BallController currentBall;
	private int BallPrefabsLength => BallPrefabs.Length;
	// private int idx = 0;
	private float lastInputTime = 0f;
	private float inputCooldown = 2f;
	public GameObject FocalPoint;
	public bool isGameOver {get; private set;}
	private MainManager mainManager;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}
	private void Start()
	{
		mainManager = MainManager.Instance;
		ballPrefabsIndexRange = 1;
		UpdateScoreText();
		UpdateBestScoreText();
		SpawnBall();
	}

	private void Update()
	{
		if(isGameOver)
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}
		else
		{
			if (currentBall == null)
				SpawnBall();

			// if (!currentBall.isDropped && Input.GetKeyDown(KeyCode.Space) && ((lastInputTime == 0f) || (Time.time - lastInputTime >= inputCooldown)))
			if (!currentBall.isDropped && Input.GetKeyDown(KeyCode.Space))
			{
				currentBall.Drop();
				SoundManager.Instance.PlayDropSound();
				// lastInputTime = Time.time;
			}
		}
	}

	void FixedUpdate()
	{
		if(isGameOver) return;
		
		if (currentBall != null)
			MoveCurrentBall();
	}

	private void MoveCurrentBall()
	{
		if (currentBall.isDropped) return;

		float horizontalInput = Input.GetAxis("Arrow Horizontal");
		float verticalInput = Input.GetAxis("Arrow Vertical");

		Transform focalPointTransnform = FocalPoint.transform;
        Vector3 moveDirection = (focalPointTransnform.forward * verticalInput + focalPointTransnform.right * horizontalInput).normalized;

		Rigidbody rb = currentBall.GetComponent<Rigidbody>();
        rb.MovePosition(rb.position + moveDirection * currentBall.moveSpeed * Time.fixedDeltaTime);
		// Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
		// Rigidbody rb = currentBall.GetComponent<Rigidbody>();
		// rb.MovePosition(rb.position + movement * currentBall.moveSpeed * Time.fixedDeltaTime);
	}


	public void SpawnBall()
	{
		// Debug.Log("Spawn Ball");
		if (ballPrefabsIndexRange < 0)
		{
			Debug.LogError("Ball Prefabs Index Range is zero or negative.");
			return;
		}

		int idx = UnityEngine.Random.Range(0, ballPrefabsIndexRange);
		if (idx >= ballPrefabsIndexRange)
		{
			Debug.LogError($"Out of Range - Ball Index : {idx}");
			return;
		}

		GameObject ballObj = Instantiate(BallPrefabs[idx], SpawnPos, Quaternion.identity);
		currentBall = ballObj.GetComponent<BallController>();
		currentBall.BallName = "ball_" + idx;
		Debug.Log($"Spawn Ball Index : {idx}, name : {currentBall.BallName}");
		idx++;
	}


	public void AddPoints(int point)
	{
		TotalPoint += point;
		UpdateScoreText();
	}

	private void UpdateScoreText()
	{
		CurrentScoreText.text = $"Score : {TotalPoint} ({MainManager.Instance?.PlayerName??""})";
	}

	public void BallMovementCompleted(BallController ball)
	{
		if (currentBall == ball)
		{
			Debug.Log($"Ball Movement Completed: {ball.BallName}");
			currentBall = null;
		}
	}

	public void SpawnMergeBall(Vector3 position, int level)
	{
		Debug.Log($"Spawn Merge Ball : {level}");

		if (level < 0 || level >= BallPrefabs.Length)
		{
			Debug.LogError("Invalid level for spawning merge ball.");
			return;
		}

		GameObject newBall = Instantiate(BallPrefabs[level], position, Quaternion.identity);

		BallController ballController = newBall.GetComponent<BallController>();
		Rigidbody rb = newBall.GetComponent<Rigidbody>();

		if (rb == null)
		{
			Debug.LogError("Rigidbody is missing from the merged ball prefab!");
			return;
		}

		ballController.isDropped = true;
		ballController.Drop();

		Debug.Log($"use gravity after Drop(): {rb.useGravity}");
		AddPoints(ballController.Point);
		SoundManager.Instance.PlayMergeSound();
	}

	public int GetBallIndexByTag(string tag)
	{
		if (string.IsNullOrEmpty(tag) || !tag.Contains("_"))
		{
			Debug.LogError($"Invalid tag format: {tag}");
			return -1;
		}

		string indexString = tag.Substring(tag.LastIndexOf("_") + 1);
		return int.TryParse(indexString, out int index) ? index : -1;
	}

	public void GameOver()
	{
		Debug.Log("GAME OVER!!!!!!!");
		SoundManager.Instance.PlayGameOverSound();
		isGameOver = true;
		GameOverScreen.SetActive(true);
		if(TotalPoint >= mainManager.bestScorePlayer.score || mainManager.bestScorePlayer.IsNullOrEmpty())
		{
			Debug.Log("here!");
			mainManager.bestScorePlayer.ReplaceBestScorePlayer(mainManager.PlayerName, TotalPoint);
		}
	}

	public bool isCurrentBall(GameObject ball)
	{
		return ball.GetComponent<BallController>().GetInstanceID() == currentBall.GetInstanceID();
	}

	private void UpdateBestScoreText()
	{
		Debug.Log("bestscore player : " + mainManager);
		BestScoreText.text = "Best Score : ";
		if(mainManager.bestScorePlayer != null)
		{
			BestScoreText.text += mainManager.bestScorePlayer.IsNullOrEmpty() ? "No Record" : mainManager.bestScorePlayer.score;
		}
		else 
		{
			BestScoreText.text += "No Record";
		}
	}
}
