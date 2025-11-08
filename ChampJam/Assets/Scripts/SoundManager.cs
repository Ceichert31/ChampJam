using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("References")]

    public AudioClip jumpSound;
    public AudioClip hitSound;
    public AudioClip collectSound;

    private AudioSource audioSource;

    private void Awake()
    {
        // singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip, float volume = 1f, bool randomPitch = false, float pitchMin = 0.9f, float pitchMax = 1.1f)
    {
        if (clip == null) 
            return;

        if (randomPitch)
        {
            audioSource.pitch = Random.Range(pitchMin, pitchMax);
        }
        else
        {
            audioSource.pitch = 1f;
        }

        // play
        audioSource.PlayOneShot(clip, volume);
    }

    // temp usage functions
    public void PlayJump() => PlaySound(jumpSound, 0.8f, true, 0.95f, 1.05f);

    public void PlayHit() => PlaySound(hitSound, 1f, true, 0.8f, 1.2f);

    public void PlayCollect() => PlaySound(collectSound, 0.6f, false);
}