using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MainManager : MonoBehaviour
{
	public TMP_InputField PlayerNameInput;
	public Slider SFXVolumeSlider;
	public Slider BGMVolumeSlider;
	private string gameSceneName = "Game";
	public static MainManager Instance;
	public string PlayerName;
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
		Debug.Log("main manager start");
		if(bestScorePlayer == null)
		{
			bestScorePlayer = new BestScorePlayer();
		}
        // if (PlayerPrefs.HasKey("BGMVolume"))
        // {
        //     float savedVolume = PlayerPrefs.GetFloat("BGMVolume");
        //     BGMVolumeSlider.value = savedVolume;
        // }

        // if (PlayerPrefs.HasKey("SFXVolumeSlider"))
        // {
        //     float savedVolume = PlayerPrefs.GetFloat("SFXVolume");
        //     SFXVolumeSlider.value = savedVolume;
        // }
    }

    public void OnSFXVolumeChanged()
    {
        // float volume = SFXVolumeSlider.value;
		// Debug.Log($"chang sfx {volume}");
        // SoundManager.Instance.SetMusicVolume(volume);
        // PlayerPrefs.SetFloat("SFXVolume", volume);
    }

	public void OnBGMVolumeSlider()
    {
        // float volume = BGMVolumeSlider.value;
        // // SoundManager.Instance.SetMusicVolume(volume);
		// Debug.Log($"chang bgm {volume}");
        // PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void OnStartGame()
    {
        // string playerName = PlayerNameInput.text;
        // PlayerPrefs.SetString("PlayerName", playerName);
		PlayerName = PlayerNameInput.text;
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
