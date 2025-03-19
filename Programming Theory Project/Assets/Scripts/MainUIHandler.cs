using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainUIHandler : MonoBehaviour
{
	public TMP_InputField PlayerNameInput;
	public Slider BGMVolumeSlider;
	public Slider SFXVolumeSlider;
	public Button StartButton;
	public static MainUIHandler Instance;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject); 
			SceneManager.sceneLoaded += OnSceneLoaded;
		}
		else
		{
			Destroy(gameObject);
		}
	}
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		FindUIElements();
	}

	private void Start()
	{
		Debug.Log("UImanager Start");
	}

	private void FindUIElements()
	{
		BGMVolumeSlider = GameObject.Find("BGMVolumeSlider")?.GetComponent<Slider>();
		SFXVolumeSlider = GameObject.Find("SFXVolumeSlider")?.GetComponent<Slider>();
		PlayerNameInput = GameObject.Find("PlayerNameInput")?.GetComponent<TMP_InputField>();
		StartButton = GameObject.Find("StartButton")?.GetComponent<Button>();

		Debug.Log($"FindUIElements, is bgmVolumeSlider null? {BGMVolumeSlider}");

		if (BGMVolumeSlider != null)
			BGMVolumeSlider.onValueChanged.AddListener(SetBGMVolume);
		
		if (SFXVolumeSlider != null)
			SFXVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
		
		if(StartButton != null)
			StartButton.onClick.AddListener(ClickStartButton);
	}

	public void SetBGMVolume(float volume)
	{
		Debug.Log($"UIManager SetBGMVolume! {volume}");
		SoundManager.Instance.SetBGMVolume(volume);
	}

	public void SetSFXVolume(float volume)
	{
		Debug.Log($"UIManager SetSFXVolume! {volume}");
		SoundManager.Instance.SetSFXVolume(volume);
	}

	public string GetPlayerName()
	{
		return PlayerNameInput.text;
	}

	public void ClickStartButton()
	{
		MainManager.Instance.OnStartGame();
	}
}
