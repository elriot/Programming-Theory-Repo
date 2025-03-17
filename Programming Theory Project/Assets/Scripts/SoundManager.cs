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
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}

		        
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true; // 반복 재생
        musicSource.playOnAwake = false;
        musicSource.volume = BGMVolume; // 기본 볼륨 설정

        // 효과음용 AudioSource 생성
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;
	}
    private void Start()
    {
        // 게임 시작과 함께 배경음악 재생
        PlayBackgroundMusic();
    }

    // 배경음악 재생
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
        PlaySFXSound(DropBallSound, SFXVolume);
    }

    public void PlayMergeSound()
    {
        PlaySFXSound(MergeBallSound, SFXVolume);
    }

	public void PlayGameOverSound()
	{
		PlaySFXSound(GameOverSound, SFXVolume);
	}


    private void PlaySFXSound(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning("Attempted to play a null audio clip.");
        }
    }
}
