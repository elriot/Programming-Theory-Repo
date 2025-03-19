using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MainManager : MonoBehaviour
{
	
	private string gameSceneName = "Game";
	private string mainSceneName = "Main";
	public static MainManager Instance;
	public BestScorePlayer bestScorePlayer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
	private void Start()
    {
		if(bestScorePlayer == null)
		{
			bestScorePlayer = new BestScorePlayer();
		}
    }

	void Update()
	{
		if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && SceneManager.Equals(SceneManager.GetActiveScene(), SceneManager.GetSceneByName(mainSceneName)))
		{
			//Debug.Log("same scene");
			OnStartGame();
		}
	}

    public void OnStartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

	public class BestScorePlayer
	{
		public int score {get; private set;}
		public string name {get; private set;}
		public BestScorePlayer()
		{
			this.score = 0;
			this.name = "";
		}
		public void ReplaceBestScorePlayer(string newName, int newScore)
		{
			name = newName;
			score = newScore;
		}
		public bool IsNullOrEmpty()
		{
			return score == 0 && name == "";
		}

	}

}
