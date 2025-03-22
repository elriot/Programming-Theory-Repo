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
	public Button ControlButton;
	public static MainUIHandler Instance;
	public GameObject ControlModal;

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
		if(SceneManager.GetActiveScene().name == "Main")
			FindUIElements();
	}

	private void Start()
	{
		// Debug.Log("UImanager Start");
	}

	private void FindUIElements()
	{
		// Debug.Log("FindUIElements");
		BGMVolumeSlider = GameObject.Find("BGMVolumeSlider")?.GetComponent<Slider>();
		SFXVolumeSlider = GameObject.Find("SFXVolumeSlider")?.GetComponent<Slider>();
		PlayerNameInput = GameObject.Find("PlayerNameInput")?.GetComponent<TMP_InputField>();
		StartButton = GameObject.Find("StartButton")?.GetComponent<Button>();
		ControlButton = GameObject.Find("ControlButton")?.GetComponent<Button>();

		// Debug.Log($"FindUIElements, is bgmVolumeSlider null? {BGMVolumeSlider}");

		if (BGMVolumeSlider != null)
		{
			BGMVolumeSlider.onValueChanged.AddListener(OnChangeBGMVolume);
			SetBGMVolumeSliderValue();
		}


		if (SFXVolumeSlider != null)
		{
			SFXVolumeSlider.onValueChanged.AddListener(OnChangeSFXVolume);
			SetSFXVolumeSliderValue();
		}


		if (StartButton != null)
			StartButton.onClick.AddListener(ClickStartButton);

		if(PlayerNameInput != null)
		{
			// PlayerNameInput.onValueChanged.AddListener(OnChangePlayerName);
			SetPlayerNameValue();
		}
	}

	private void SetSFXVolumeSliderValue()
	{
		SFXVolumeSlider.value = SoundManager.Instance.GetSFXVolume();
	}

	private void SetBGMVolumeSliderValue()
	{
		BGMVolumeSlider.value = SoundManager.Instance.GetBGMVolume();
	}

	public void OnChangeBGMVolume(float volume = 0.5f)
	{
		
		SoundManager.Instance.SetBGMVolume(volume);
		// Debug.Log($"Set bgm change {volume}");
		
	}

	public void OnChangeSFXVolume(float volume = 0.5f)
	{
		SoundManager.Instance.SetSFXVolume(volume);	
	}

	// public void OnChangePlayerName(string name)
	// {
	// 	PlayerPrefs.SetString("PlayerName", name);	
	// }

	private void SetPlayerNameValue()
	{
		PlayerNameInput.text = PlayerPrefs.GetString("PlayerName");
	}
	
	// public void ClickControlButton()
	// {
	// 	GameObject modal = GameObject.Find("ControlModal");
	// 	modal.SetActive(true);
	// 	// ControlModal.gameObject.isActive(true);
	// }

	

	public void ClickStartButton()
	{
		// PlayerPrefs.SetString("PlayerName", PlayerNameInput.text);
		MainManager.Instance.OnStartGame();
	}
}
