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

    [Header("Moth Follow 1")]
    [SerializeField] private AudioClip mothFollow1;
    [SerializeField] private float mothFollowVolume1 = 0.75f;
    [SerializeField] private bool mothFollowRandomPitch1 = true;
    [SerializeField] private float mothFollowLowerPitch1 = 0.9f;
    [SerializeField] private float mothFollowHigherPitch1 = 1.1f;

    [Header("Moth Follow 2")]
    [SerializeField] private AudioClip mothFollow2;
    [SerializeField] private float mothFollowVolume2 = 0.75f;
    [SerializeField] private bool mothFollowRandomPitch2 = true;
    [SerializeField] private float mothFollowLowerPitch2 = 0.9f;
    [SerializeField] private float mothFollowHigherPitch2 = 1.1f;

    [Header("Moth Follow 3")]
    [SerializeField] private AudioClip mothFollow3;
    [SerializeField] private float mothFollowVolume3 = 0.75f;
    [SerializeField] private bool mothFollowRandomPitch3 = true;
    [SerializeField] private float mothFollowLowerPitch3 = 0.9f;
    [SerializeField] private float mothFollowHigherPitch3 = 1.1f;

    [Header("Flea Flee 1")]
    [SerializeField] private AudioClip fleaFlee1;
    [SerializeField] private float fleaFleeVolume1 = 0.75f;
    [SerializeField] private bool fleaFleeRandomPitch1 = true;
    [SerializeField] private float fleaFleeLowerPitch1 = 0.9f;
    [SerializeField] private float fleaFleeHigherPitch1 = 1.1f;

    [Header("Flea Flee 2")]
    [SerializeField] private AudioClip fleaFlee2;
    [SerializeField] private float fleaFleeVolume2 = 0.75f;
    [SerializeField] private bool fleaFleeRandomPitch2 = true;
    [SerializeField] private float fleaFleeLowerPitch2 = 0.9f;
    [SerializeField] private float fleaFleeHigherPitch2 = 1.1f;

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
    //soundmanager.instance.whatever
    // temp usage functions
    public void PlaySpiderMove() => PlaySound(spiderMove, spiderMoveVolume, spiderMoveRandomPitch, spiderMoveLowerPitch, spiderMoveHigherPitch);
    public void PlaySpiderBite() => PlaySound(spiderBite, spiderBiteVolume, spiderBiteRandomPitch, spiderBiteLowerPitch, spiderBiteHigherPitch);
    public void PlaySpiderEat() => PlaySound(spiderEat, spiderEatVolume, spiderEatRandomPitch, spiderEatLowerPitch, spiderEatHigherPitch);
    public void PlaySpiderSpit() => PlaySound(spiderSpit, spiderSpitVolume, spiderSpitRandomPitch, spiderSpitLowerPitch, spiderSpitHigherPitch);
    public void PlayMothFollow()
    {
        switch(Random.Range(1, 4))
        {
            case 1:
                PlaySound(mothFollow1, mothFollowVolume1, mothFollowRandomPitch1, mothFollowLowerPitch1, mothFollowHigherPitch1);
                return;
            case 2:
                PlaySound(mothFollow2, mothFollowVolume2, mothFollowRandomPitch2, mothFollowLowerPitch2, mothFollowHigherPitch2);
                return;
            case 3:
                PlaySound(mothFollow3, mothFollowVolume3, mothFollowRandomPitch3, mothFollowLowerPitch3, mothFollowHigherPitch3);
                return;
        }
    }
    public void PlayFleaFlee()
    {
        if(Random.Range(1,3) == 1)
        {
            PlaySound(fleaFlee1, fleaFleeVolume1, fleaFleeRandomPitch1, fleaFleeLowerPitch1, fleaFleeHigherPitch1);
        }
        else
        {
            PlaySound(fleaFlee2, fleaFleeVolume2, fleaFleeRandomPitch2, fleaFleeLowerPitch2, fleaFleeHigherPitch2);
        }
        
    }
    public void PlayWeevilSpook() => PlaySound(weevilSpook, weevilSpookVolume, weevilSpookRandomPitch, weevilSpookLowerPitch, weevilSpookHigherPitch);
}