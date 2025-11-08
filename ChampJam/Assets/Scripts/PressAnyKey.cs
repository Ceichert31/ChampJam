using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKey : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "MainScene";

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            // Load the specified scene
            SoundManager.Instance.PlayMenuClick();
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}