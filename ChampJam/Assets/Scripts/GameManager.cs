using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform BugGoal;

    public Transform SpiderPos;

    private BoxCollider2D bounds;

    private List<GameObject> bugList = new();

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

    public void AddBug(GameObject bug)
    {
        bugList.Add(bug);
    }

    public void RemoveBug(GameObject bug)
    {
        bugList.Remove(bug);
    }

    public Transform GetNextTarget(Vector2 pos)
    {
        if (bugList.Count <= 0)
        {
            return null;
        }

        float dist = 100;
        GameObject closest = null;
        foreach(GameObject bug in bugList)
        {
            if (Vector2.Distance(pos, (Vector2)bug.transform.position) < dist)
            {
                dist = Vector2.Distance(pos, (Vector2)bug.transform.position);
                closest = bug;
            } 
        }

        return closest.transform;
    }
}
