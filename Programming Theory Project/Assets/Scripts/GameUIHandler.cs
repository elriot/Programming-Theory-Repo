using UnityEngine;
using TMPro;
using System;

public class GameUIHandler : MonoBehaviour
{
	public TMP_Text CurrentScoreText;
	public TMP_Text BestScoreText;
	public GameObject GameOverScreen;

	public static GameUIHandler Instance;
	private MainManager mainManager;

	public string PlayerName {get; private set; }

	void Awake()
	{
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
	}
	void Start()
	{
		mainManager = MainManager.Instance;
		PlayerName = PlayerPrefs.GetString("PlayerName").Trim();
		UpdateBestScoreText();
	}

	public void UpdateCurrentScore(int score)
	{
		CurrentScoreText.text =$"Score : {score} ";
		CurrentScoreText.text += $"({PlayerName ?? ""})";
	}

	public void ShowGameOverScreen()
	{
		GameOverScreen.SetActive(true);
	}

	private void UpdateBestScoreText()
	{
		BestScoreText.text = "Best Score : ";
		if (mainManager.bestScorePlayer != null)
		{
			BestScoreText.text += mainManager.bestScorePlayer.IsNullOrEmpty() ? "No Record" : $"{mainManager.bestScorePlayer.score} ({mainManager.bestScorePlayer.name})";
		}
		else
		{
			BestScoreText.text += "No Record";
		}
	}

}
