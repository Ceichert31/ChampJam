using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform BugGoal;

    public Transform SpiderPos;

    private int score = 0;
    private void Awake()
    {
        Instance = this;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
}
