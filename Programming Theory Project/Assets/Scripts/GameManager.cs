using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
	public static GameManager Instance;
	public GameObject[] BallPrefabs;
	[SerializeField] private int ballPrefabsIndexRange {get; set;}
	public int TotalPoint;
	public Vector3 SpawnPos;
	public TMP_Text ScoreText;
	private BallController currentBall;
	private int ballPrefabsLength;

	// Update is called once per frame
	void Start()
	{
		ballPrefabsLength = BallPrefabs.Length;
		ballPrefabsIndexRange = 0;
		if(Instance == null)
		{
			Instance = this;
		}
		UpdateScoreText(TotalPoint);
	}
	void Update()
    {
        if(currentBall == null || currentBall.isDropped && !currentBall.isMovable)
		{
			currentBall = null;
			SpawnBall();
		}
    }

	private void SpawnBall()
	{	
		Debug.Log("SpawnBall");
		int idx = UnityEngine.Random.Range(0, ballPrefabsIndexRange);
		if(idx > ballPrefabsIndexRange)
		{
			Debug.Log($"Out of Range - Ball Index : {idx}");
			return;
		}

		GameObject ballObj = Instantiate(BallPrefabs[idx].gameObject, SpawnPos, BallPrefabs[idx].transform.rotation);
        currentBall = ballObj.GetComponent<BallController>();
        currentBall.isMovable = true;
		currentBall.isDropped = false;
	}

	private void SpawnMergeBall(Vector3 spawnPos, int ballIndex)
	{
		int idx = GetNextBallIndex(ballIndex);
		Debug.Log($"[SpawnMergeBall] prefab sizs : {BallPrefabs.Length}, current Index : {idx}");
		GameObject targetBall = BallPrefabs[idx].gameObject;
		GameObject newBall = Instantiate(targetBall, spawnPos, targetBall.transform.rotation);
	}

	public void Merge(GameObject ball1, GameObject ball2)
	{
		// if(ball1 != null && ball2 != null)
		{
			Debug.Log("Merge ball");
			BallController bc = ball1.GetComponent<BallController>();
			Vector3 pos = (ball1.transform.position + ball2.transform.position) / 2;
			int ballIndex = GetBallIndexByTag(bc.gameObject.tag); // if ball tag is "Ball_0" then return 0

			SpawnMergeBall(pos, ballIndex);
			currentBall = null;

			Destroy(ball1);
			Destroy(ball2);
		}
	}

	public void UpdatePoint(int point)
	{
		TotalPoint += point; 
		UpdateScoreText(TotalPoint);
		// Debug.Log($"update Point : {point}");
	}
	private void UpdateScoreText(int score)
	{
		ScoreText.text = $"Score : {score}";
	}

	private int GetBallIndexByTag(string tag)
	{
		return int.Parse(tag.Substring(tag.LastIndexOf("_")+1, 1));
	}

	public void BallMovementCompleted()
	{
		currentBall = null;
	}

	private int GetNextBallIndex(int ballIndex)
	{
		if(ballPrefabsLength-1 > ballPrefabsIndexRange && ballPrefabsIndexRange >= ballIndex)
		{
			ballPrefabsIndexRange++;
			Debug.Log($"here BallPrefabsIndexRange : {ballPrefabsIndexRange}");	
		}
		return ballPrefabsIndexRange;
	}
}
