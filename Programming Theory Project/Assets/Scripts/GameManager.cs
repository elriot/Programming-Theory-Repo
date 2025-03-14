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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        ballPrefabsIndexRange = 0;
        UpdateScoreText();
    }

    private void Update()
    {
        if (currentBall == null || !currentBall.isMovable)
        {
            currentBall = null;
            SpawnBall();
        }
    }

    private void SpawnBall()
    {
        if (ballPrefabsIndexRange < 0)
        {
            Debug.LogError("Ball Prefabs Index Range is zero or negative.");
            return;
        }

        int idx = UnityEngine.Random.Range(0, ballPrefabsIndexRange);
        if (idx > ballPrefabsIndexRange)
        {
            Debug.LogError($"Out of Range - Ball Index : {idx}");
            return;
        }

		Debug.Log($"Spawn Ball Index : {idx}");
        GameObject ballObj = Instantiate(BallPrefabs[idx], SpawnPos, Quaternion.identity);
        currentBall = ballObj.GetComponent<BallController>();
		currentBall.isMovable = true;
    }


    public void UpdatePoint(int point)
    {
        TotalPoint += point;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        ScoreText.text = $"Score : {TotalPoint}";
    }

    public void BallMovementCompleted()
    {
        currentBall = null;
    }

    public void SpawnMergeBall(Vector3 position, int level)
    {
        if (level < 0 || level >= BallPrefabs.Length)
        {
            Debug.LogError("Invalid level for spawning merge ball.");
            return;
        }

        GameObject newBall = Instantiate(BallPrefabs[level], position, Quaternion.identity);
		newBall.GetComponent<BallController>().isMovable = false;
    }
}
