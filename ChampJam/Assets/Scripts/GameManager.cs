using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform BugGoal;

    public Transform SpiderPos;

    private BoxCollider2D bounds;

    private int score = 0;
    private void Awake()
    {
        Instance = this;

        bounds = GetComponent<BoxCollider2D>();
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    /// <summary>
    /// Returns a random point within the screen
    /// </summary>
    /// <returns></returns>
    public Vector2 GetRandomPointInBounds()
    {
        Vector2 extents = bounds.size / 2f;

        Vector2 localPoint = new Vector2(
            Random.Range(-extents.x, extents.x),
             Random.Range(-extents.y, extents.y)
            );

        return bounds.transform.TransformPoint(localPoint);
    }
}
