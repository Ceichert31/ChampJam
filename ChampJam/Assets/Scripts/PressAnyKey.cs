using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKey : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "MainScene";

    public Animator animator;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            // Load the specified scene
            SoundManager.Instance.PlayMenuClick();
            animator.SetTrigger("Transition");
        }
    }
}