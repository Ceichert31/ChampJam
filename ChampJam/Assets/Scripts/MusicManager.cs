using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource baseMusic;
    [SerializeField] private AudioSource mothMusic;
    [SerializeField] private AudioSource fleaMusic;
    [SerializeField] private AudioSource beetleMusic;

    void Start()
    {
        Activate();
    }

    void Update()
    {
        GetActiveBugs();
    }

    void Activate()
    {
        baseMusic.Play();
        mothMusic.Play();
        mothMusic.volume = 0;
        fleaMusic.Play();
        fleaMusic.volume = 0;
        beetleMusic.Play();
        beetleMusic.volume = 0;
    }
    
    void GetActiveBugs()
    {
        if (GameManager.Instance.HasMothOnScreen())
        {
            mothMusic.volume = Mathf.Lerp(mothMusic.volume, 1, 5 * Time.deltaTime);
        }
        else
        {
            mothMusic.volume = Mathf.Lerp(mothMusic.volume, 0, 5 * Time.deltaTime);
        }

        if (GameManager.Instance.HasFlyOnScreen())
        {
            fleaMusic.volume = Mathf.Lerp(fleaMusic.volume, 1, 5 * Time.deltaTime);
        }
        else
        {
            fleaMusic.volume = Mathf.Lerp(fleaMusic.volume, 0, 5 * Time.deltaTime);
        }

        if (GameManager.Instance.HasBeetleOnScreen())
        {
            beetleMusic.volume = Mathf.Lerp(beetleMusic.volume, 1, 5 * Time.deltaTime);
        }
        else
        {
            beetleMusic.volume = Mathf.Lerp(beetleMusic.volume, 0, 5 * Time.deltaTime);
        }
    }
}
