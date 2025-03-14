using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
	public static GameManager Instance;
	public int Level = 1;
	public int TotalPoint;
	public GameObject[] BallPrefabs;
	public Vector3 SpawnPos;
	private BallController currentBall;

	// Update is called once per frame
	void Start()
	{
		if(Instance == null)
		{
			Instance = this;
		}
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
		int idx = UnityEngine.Random.Range(0, Level);
		if(idx >= Level)
		{
			Debug.Log("Out of Range - Ball Index");
			return;
		}

		GameObject ballObj = Instantiate(BallPrefabs[idx].gameObject, SpawnPos, BallPrefabs[idx].transform.rotation);
        currentBall = ballObj.GetComponent<BallController>();
        currentBall.isMovable = true;
		currentBall.isDropped = false;
	}

	private void SpawnMergeBall(Vector3 spawnPos, int level)
	{
		Debug.Log("spawn merge ball " + level + " " + spawnPos);
		// GameObject targetBall = BallPrefabs[level-1].gameObject;
		// Instantiate(targetBall, spawnPos, targetBall.transform.rotation);
		// currentBall = null;
        // currentBall = ballObj.GetComponent<BallController>();
        // currentBall.isMovable = true;
	}

	public void Merge(GameObject ball1, GameObject ball2)
	{
		// if(ball1 != null && ball2 != null)
		{
			BallController bc = ball1.GetComponent<BallController>();
			Vector3 pos = (ball1.transform.position + ball2.transform.position) / 2;
			int level = GetLevelByTag(bc.gameObject.tag) + 1;
			Debug.Log($"level : {level}");

			Level = Math.Max(level, Level);
			Debug.Log($"Level : {Level}");
			SpawnMergeBall(pos, level);

			Debug.Log("Dfd");

			Destroy(ball1);
			Destroy(ball2);
		}
	}

	public void UpdatePoint(int point)
	{
		TotalPoint += point;
	}

	private int GetLevelByTag(string tag)
	{
		return int.Parse(tag.Substring(tag.LastIndexOf("_")+1, 1));
	}

	public void BallMovementCompleted()
	{
		currentBall = null;
	}
}
