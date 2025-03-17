using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
	public TMP_InputField PlayerNameInput;
	public Slider SFXVolumeSlider;
	public Slider BGMVolumeSlider;
	private string gameSceneName = "Game";

	private void Start()
    {
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("BGMVolume");
            BGMVolumeSlider.value = savedVolume;
        }

        if (PlayerPrefs.HasKey("SFXVolumeSlider"))
        {
            float savedVolume = PlayerPrefs.GetFloat("SFXVolume");
            SFXVolumeSlider.value = savedVolume;
        }
    }

    public void OnSFXVolumeChanged()
    {
        float volume = SFXVolumeSlider.value;
		Debug.Log($"chang sfx {volume}");
        // SoundManager.Instance.SetMusicVolume(volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

	public void OnBGMVolumeSlider()
    {
        float volume = BGMVolumeSlider.value;
        // SoundManager.Instance.SetMusicVolume(volume);
		Debug.Log($"chang bgm {volume}");
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void OnStartGame()
    {
        string playerName = PlayerNameInput.text;
        PlayerPrefs.SetString("PlayerName", playerName);

        SceneManager.LoadScene(gameSceneName);
    }

}
