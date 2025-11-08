using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform bugGoal;

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
