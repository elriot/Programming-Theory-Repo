using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
	private AudioSource musicSource;
	private AudioSource sfxSource;

	public AudioClip DropBallSound;
	public AudioClip MergeBallSound;
	public AudioClip GameOverSound;
	public AudioClip BackgroundMusic;

	private float SFXVolumeNormalizer;
	// public float SFXVolume;
	// public float BGMVolume;

	void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}

    private void Start()
	{
		SFXVolumeNormalizer = 3f;

		//bgm audio source 
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true; 
        musicSource.playOnAwake = false;
        musicSource.volume = GetBGMVolume(); 

		//sfx audio source
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;
		sfxSource.volume = GetSFXVolume();

        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
		// Debug.Log("PlayBackgroundMusic");
        if (BackgroundMusic != null)
        {
            musicSource.clip = BackgroundMusic;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Background music clip is not assigned.");
        }
    }

	public void PlayDropSound()
    {
        PlaySFXSound(DropBallSound);
    }

    public void PlayMergeSound()
    {
        PlaySFXSound(MergeBallSound);
    }

	public void PlayGameOverSound()
	{
		PlaySFXSound(GameOverSound);
	}


    private void PlaySFXSound(AudioClip clip)
    {
		// Debug.Log($"Player Sound {clip.name} {GetSFXVolume()}");
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip, GetSFXVolume() * SFXVolumeNormalizer);
        }
        else
        {
            Debug.LogWarning("Attempted to play a null audio clip.");
        }
    }
	
	public float GetBGMVolume()
	{
		return PlayerPrefs.HasKey("BGMVolume") ? PlayerPrefs.GetFloat("BGMVolume") : 0.5f;
	}

	public float GetSFXVolume()
	{
		return PlayerPrefs.HasKey("SFXVolume") ? PlayerPrefs.GetFloat("SFXVolume") : 0.5f;
	}

	public void SetBGMVolume(float volume)
	{
		PlayerPrefs.SetFloat("BGMVolume", volume);
		if(musicSource != null)
			musicSource.volume = volume;
	}

	public void SetSFXVolume(float volume)
	{
		// Debug.Log($"SM : SetSFXVolume {volume}");
		PlayerPrefs.SetFloat("SFXVolume", volume);

		if(sfxSource != null)
		{
			sfxSource.volume= volume;
			PlayDropSound();
		}
	}
}
