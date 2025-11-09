using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasAnimationMethods : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private string sceneToLoad = "MainScene";

    public GameObject music;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void PlayStartSFX()
    {
        source.Play();
        music.SetActive(false);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
