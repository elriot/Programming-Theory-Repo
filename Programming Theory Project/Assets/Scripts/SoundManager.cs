using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
	private AudioSource audioSource;

	public AudioClip DropBallSound;
	public AudioClip MergeBallSound;
	public AudioClip BGMSound;
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
        PlaySound(DropBallSound);
		audioSource.PlayOneShot(DropBallSound, EffectVolume);
    }

    public void PlayMergeSound()
    {
        PlaySound(MergeBallSound);
    }

    // 일반적인 사운드 재생 함수
    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, EffectVolume);
        }
        else
        {
            Debug.LogWarning("Attempted to play a null audio clip.");
        }
    }
}
