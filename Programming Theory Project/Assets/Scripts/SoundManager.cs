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
	public float SFXVolume;
	public float BGMVolume;

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
		SFXVolume = GetSFXVolume();
		BGMVolume = GetBGMVolume() * 0.3f;

		//bgm audio source 
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true; 
        musicSource.playOnAwake = false;
        musicSource.volume = BGMVolume; 

		//sfx audio source
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
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
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip, SFXVolume);
        }
        else
        {
            Debug.LogWarning("Attempted to play a null audio clip.");
        }
    }
	
	private float GetBGMVolume()
	{
		return PlayerPrefs.HasKey("BGMVolume") ? PlayerPrefs.GetFloat("BGMVolume") : 0.5f;
	}

	private float GetSFXVolume()
	{
		return PlayerPrefs.HasKey("SFXVolume") ? PlayerPrefs.GetFloat("SFXVolume") : 0.5f;
	}

	public void SetBGMVolume(float volume)
	{
		musicSource.volume = volume;
	}

	public void SetSFXVolume(float volume)
	{
		sfxSource.volume= volume;
	}
}
