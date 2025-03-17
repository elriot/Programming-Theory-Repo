using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	public GameObject[] BallPrefabs;
	[SerializeField] private int ballPrefabsIndexRange;
	public int TotalPoint;
	public Vector3 SpawnPos;
	public TMP_Text ScoreText;
	private BallController currentBall;
	private int BallPrefabsLength => BallPrefabs.Length;
	private int idx = 0;

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
		ballPrefabsIndexRange = 1;
		UpdateScoreText();
		SpawnBall();
	}

	private void Update()
	{
		if(currentBall == null)
			SpawnBall();
	}

	public void SpawnBall()
	{
		Debug.Log("Spawn Ball");
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
		currentBall.isMovable = true;
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
		ScoreText.text = $"Score : {TotalPoint}";
	}

	public void BallMovementCompleted(BallController ball)
	{
		if(currentBall == null)
		{
			Debug.Log("current Ball null");
			return;
		}
		BallController ballBc = ball.GetComponent<BallController>();
		Debug.Log($"ball deactivate ! currentBall is {currentBall.BallName}, ball is {ballBc.BallName}");
		Debug.Log($"do they same object? {ball == currentBall}");
		if(ball.BallName == currentBall.BallName)
		{
			Debug.Log("here");
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
		newBall.GetComponent<Rigidbody>().useGravity = true;
		ballController.isMovable = false;
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
}
