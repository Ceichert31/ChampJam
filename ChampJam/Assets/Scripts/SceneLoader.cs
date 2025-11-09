using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string sceneName;
    public void LoadThisScene()
    {
        SoundManager.Instance.PlayMenuClick();
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        SoundManager.Instance.PlayMenuQuit();

        Invoke(nameof(QuitApp), 2.0f);
    }

    private void QuitApp() => Application.Quit();

    public void ReloadScene()
    {
        SoundManager.Instance.PlayMenuClick();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
