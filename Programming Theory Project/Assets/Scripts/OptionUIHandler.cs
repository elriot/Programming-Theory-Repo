using UnityEngine;
using UnityEngine.UI;

public class OptionUIHandler : MonoBehaviour
{
	public Slider BGMVolumeSlider;
	public Slider SFXVolumeSlider;

	private void OnEnable()
	{
		if (BGMVolumeSlider == null || SFXVolumeSlider == null || SoundManager.Instance == null)
		{
			return;
		}

		BGMVolumeSlider.value = SoundManager.Instance.GetBGMVolume();
		SFXVolumeSlider.value = SoundManager.Instance.GetSFXVolume();

		BGMVolumeSlider.onValueChanged.AddListener(OnChangeBGMVolume);
		SFXVolumeSlider.onValueChanged.AddListener(OnChangeSFXVolume);
	}

	private void OnDisable()
	{
		BGMVolumeSlider.onValueChanged.RemoveListener(OnChangeBGMVolume);
		SFXVolumeSlider.onValueChanged.RemoveListener(OnChangeSFXVolume);
	}

	public void OnChangeBGMVolume(float volume)
	{
		SoundManager.Instance.SetBGMVolume(BGMVolumeSlider.value);
	}

	public void OnChangeSFXVolume(float volume)
	{
		SoundManager.Instance.SetSFXVolume(SFXVolumeSlider.value);
	}
}