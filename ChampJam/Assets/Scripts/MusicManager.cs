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
        fleaMusic.Play();
        beetleMusic.Play();
    }
    
    void GetActiveBugs()
    {
        if (GameManager.Instance.HasMothOnScreen())
        {
            mothMusic.volume = 1;
        }
        else
        {
            mothMusic.volume = 0;
        }

        if (GameManager.Instance.HasFlyOnScreen())
        {
            fleaMusic.volume = 1;
        }
        else
        {
            fleaMusic.volume = 0;
        }

        if (GameManager.Instance.HasFlyOnScreen())
        {
            beetleMusic.volume = 1;
        }
        else
        {
            beetleMusic.volume = 0;
        }
    }
}
