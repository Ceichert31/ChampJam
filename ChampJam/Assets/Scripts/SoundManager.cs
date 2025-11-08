using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("References")]

    [Header("Spider Move")]
    [SerializeField] private AudioClip spiderMove;
    [SerializeField] private float spiderMoveVolume = 0.75f;
    [SerializeField] private bool spiderMoveRandomPitch = true;
    [SerializeField] private float spiderMoveLowerPitch = 0.9f;
    [SerializeField] private float spiderMoveHigherPitch = 1.1f;

    [Header("Spider Bite")]
    [SerializeField] private AudioClip spiderBite;
    [SerializeField] private float spiderBiteVolume = 0.75f;
    [SerializeField] private bool spiderBiteRandomPitch = true;
    [SerializeField] private float spiderBiteLowerPitch = 0.9f;
    [SerializeField] private float spiderBiteHigherPitch = 1.1f;

    [Header("Spider Eat")]
    [SerializeField] private AudioClip spiderEat;
    [SerializeField] private float spiderEatVolume = 0.75f;
    [SerializeField] private bool spiderEatRandomPitch = true;
    [SerializeField] private float spiderEatLowerPitch = 0.9f;
    [SerializeField] private float spiderEatHigherPitch = 1.1f;

    [Header("Spider Spit")]
    [SerializeField] private AudioClip spiderSpit;
    [SerializeField] private float spiderSpitVolume = 0.75f;
    [SerializeField] private bool spiderSpitRandomPitch = true;
    [SerializeField] private float spiderSpitLowerPitch = 0.9f;
    [SerializeField] private float spiderSpitHigherPitch = 1.1f;

    [Header("Moth Follow")]
    [SerializeField] private AudioClip mothFollow;
    [SerializeField] private float mothFollowVolume = 0.75f;
    [SerializeField] private bool mothFollowRandomPitch = true;
    [SerializeField] private float mothFollowLowerPitch = 0.9f;
    [SerializeField] private float mothFollowHigherPitch = 1.1f;

    [Header("Flea Flee")]
    [SerializeField] private AudioClip fleaFlee;
    [SerializeField] private float fleaFleeVolume = 0.75f;
    [SerializeField] private bool fleaFleeRandomPitch = true;
    [SerializeField] private float fleaFleeLowerPitch = 0.9f;
    [SerializeField] private float fleaFleeHigherPitch = 1.1f;

    [Header("Weevil Spook")]
    [SerializeField] private AudioClip weevilSpook;
    [SerializeField] private float weevilSpookVolume = 0.75f;
    [SerializeField] private bool weevilSpookRandomPitch = true;
    [SerializeField] private float weevilSpookLowerPitch = 0.9f;
    [SerializeField] private float weevilSpookHigherPitch = 1.1f;

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
    public void PlaySpiderMove() => PlaySound(spiderMove, spiderMoveVolume, spiderMoveRandomPitch, spiderMoveLowerPitch, spiderMoveHigherPitch);
    public void PlaySpiderBite() => PlaySound(spiderBite, spiderBiteVolume, spiderBiteRandomPitch, spiderBiteLowerPitch, spiderBiteHigherPitch);
    public void PlaySpiderEat() => PlaySound(spiderEat, spiderEatVolume, spiderEatRandomPitch, spiderEatLowerPitch, spiderEatHigherPitch);
    public void PlaySpiderSpit() => PlaySound(spiderSpit, spiderSpitVolume, spiderSpitRandomPitch, spiderSpitLowerPitch, spiderSpitHigherPitch);
    public void PlayMothFollow() => PlaySound(mothFollow, mothFollowVolume, mothFollowRandomPitch, mothFollowLowerPitch, mothFollowHigherPitch);
    public void PlayFleaFlee() => PlaySound(fleaFlee, fleaFleeVolume, fleaFleeRandomPitch, fleaFleeLowerPitch, fleaFleeHigherPitch);
    public void PlayWeevilSpook() => PlaySound(weevilSpook, weevilSpookVolume, weevilSpookRandomPitch, weevilSpookLowerPitch, weevilSpookHigherPitch);
}