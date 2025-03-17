using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
	private AudioSource audioSource;

	public AudioClip DropBallSound;
	public AudioClip MergeBallSound;
	public AudioClip BGMSound;
	public AudioClip GameOverSound;
	public float EffectVolume;
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
	}

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
		if(BGMVolume > 0 && BGMSound != null)
		{
			audioSource.loop = true;
			audioSource.volume = BGMVolume;
			audioSource.Play();
		}
    }

    public void PlayDropSound()
    {
        PlaySound(DropBallSound, EffectVolume);
    }

    public void PlayMergeSound()
    {
        PlaySound(MergeBallSound, EffectVolume);
    }

	public void PlayGameOverSound()
	{
		PlaySound(GameOverSound, EffectVolume);
	}


    private void PlaySound(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning("Attempted to play a null audio clip.");
        }
    }
}
