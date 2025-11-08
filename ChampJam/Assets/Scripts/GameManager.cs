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

    private Vector2 noBugsPos = new Vector2(-99, -99);
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

    public bool IsInBounds(Vector2 pos)
    {
        return bounds.bounds.Contains(pos);
    }

    public void AddBug(GameObject bug)
    {
        bugList.Add(bug);
    }

    public void RemoveBug(GameObject bug)
    {
        bugList.Remove(bug);
    }

    /// <summary>
    /// Returns the next target for the spider to eat
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public Vector2 GetNextTarget(Vector2 pos)
    {
        //If no bugs are nearby or on-screen, wander
        if (bugList.Count <= 0)
        {
            return GetRandomPointInBounds();
        }

        float dist = 100;
        GameObject closest = null;
        foreach(GameObject bug in bugList)
        {
            if (!IsInBounds(bug.transform.position))
            {
                continue;
            }

            //Calculate value of bug too

            //Check for closest bug
            if (Vector2.Distance(pos, (Vector2)bug.transform.position) < dist)
            {
                dist = Vector2.Distance(pos, (Vector2)bug.transform.position);
                closest = bug;
            } 
        }

        if (closest == null)
        {
            return GetRandomPointInBounds();
        }

        return closest.transform.position;
    }

    /// <summary>
    /// Checks if the spider has the next target
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public bool HasNextTarget(Vector2 pos)
    {
        //If no bugs are nearby or on-screen, wander
        if (bugList.Count <= 0)
        {
            return false;
        }

        float dist = 100;
        GameObject closest = null;
        foreach (GameObject bug in bugList)
        {
            if (!IsInBounds(bug.transform.position))
            {
                continue;
            }

            //Calculate value of bug too

            //Check for closest bug
            if (Vector2.Distance(pos, (Vector2)bug.transform.position) < dist)
            {
                dist = Vector2.Distance(pos, (Vector2)bug.transform.position);
                closest = bug;
            }
        }

        if (closest == null)
        {
            return false;
        }

        return true;
    }
}
