using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainUIHandler : MonoBehaviour
{
	public TMP_InputField PlayerNameInput;
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
		PlayerNameInput = GameObject.Find("PlayerNameInput")?.GetComponent<TMP_InputField>();
		StartButton = GameObject.Find("StartButton")?.GetComponent<Button>();
		ControlButton = GameObject.Find("ControlButton")?.GetComponent<Button>();

		if (StartButton != null)
			StartButton.onClick.AddListener(ClickStartButton);

		if(PlayerNameInput != null)
		{
			// PlayerNameInput.onValueChanged.AddListener(OnChangePlayerName);
			SetPlayerNameValue();
		}
	}

	private void SetPlayerNameValue()
	{
		PlayerNameInput.text = PlayerPrefs.GetString("PlayerName");
	}
	
	public void ClickStartButton()
	{
		// PlayerPrefs.SetString("PlayerName", PlayerNameInput.text);
		MainManager.Instance.OnStartGame();
	}
}
