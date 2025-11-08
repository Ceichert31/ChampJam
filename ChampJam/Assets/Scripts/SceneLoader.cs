using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string sceneName;
    public void LoadThisScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
