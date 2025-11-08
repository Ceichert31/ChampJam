using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform bugGoal;
    private void Awake()
    {
        Instance = this;
    }
}
