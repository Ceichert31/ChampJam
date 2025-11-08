using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameOnKeyPress : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "GameScene";

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}