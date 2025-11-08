using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    List<BugHotel> bugHotels = new();

    public Transform SpiderPos;

    private BoxCollider2D bounds;

    private List<GameObject> bugList = new();

    [SerializeField]
    private float switchBugHotelTime = 15f;

    private float switchBugHotelTimer;


    private int score = 0;

    private LanternControls lantern;

    [SerializeField] private Reputation repMeter;
    [SerializeField] private GameOverScreen gameOver;
    [SerializeField] private BugSpawner bugSpawner;
    [SerializeField] private float repGainOnDeath = 12f;

    public bool notEnded = true;

    private int totalBugsSaved = 0;
    private void Awake()
    {
        Instance = this;

        bounds = GetComponent<BoxCollider2D>();

        ChangeActiveHotels();
    }

    private void Start()
    {
        // im sorry
        lantern = GameObject.Find("LanternPrefab(Clone)").transform.GetChild(1).GetComponent<LanternControls>();

        if (lantern == null)
        {
            Debug.LogError("Bruh moment!");
        }
    }

    private void Update()
    {
        if (Time.time > switchBugHotelTimer)
        {
            ChangeActiveHotels();
        }

        if (repMeter != null)
        {
            if (repMeter.reputationScore >= 60f && notEnded)
            {
                if (gameOver != null)
                {
                    gameOver.OpenGameOverMenu();
                }
                
                if (bugSpawner != null)
                {
                    bugSpawner.DestroyAllBugs();
                }

                notEnded = false;
            }
        }
        
    }

    private void ChangeActiveHotels()
    {
        switchBugHotelTimer = Time.time + switchBugHotelTime;

        if (bugHotels.Count <= 0)
            return;

        //Disable all hotels
        foreach (var hotel in bugHotels)
        {
            hotel.DeactivateHotel();
        }
        int index = Random.Range(0, bugHotels.Count);
        //Randomly enable one hotel

        bugHotels[index].ActivateHotel();
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

        //If no bugs are on screen, return random
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

    public bool GetLightState()
    {
        return lantern.isLightOn;
    }

    public bool HasFlyOnScreen()
    {
        foreach (GameObject bug in bugList)
        {
            if (IsInBounds(bug.transform.position) && bug.TryGetComponent(out AIAgent agent))
            {
                if (agent.bugType == BugType.FLY)
                    return true;
            }
        }
        return false;
    }
    public bool HasMothOnScreen()
    {
        foreach (GameObject bug in bugList)
        {
            if (IsInBounds(bug.transform.position) && bug.TryGetComponent(out AIAgent agent))
            {
                if (agent.bugType == BugType.MOTH)
                    return true;
            }
        }
        return false;
    }

    public bool HasBeetleOnScreen()
    {
        foreach (GameObject bug in bugList)
        {
            if (IsInBounds(bug.transform.position) && bug.TryGetComponent(out AIAgent agent))
            {
                if (agent.bugType == BugType.DEFAULT)
                    return true;
            }
        }
        return false;
    }

    public void RepLossOnDeath()
    {
        if (repMeter != null)
            repMeter.reputationScore += repGainOnDeath;
    }

    public void IncrementBugsSaved()
    {
        totalBugsSaved++;
        gameOver.setTotalScoreTF(totalBugsSaved * 100);
        gameOver.setBugsLeadTF(totalBugsSaved);
    }
}
