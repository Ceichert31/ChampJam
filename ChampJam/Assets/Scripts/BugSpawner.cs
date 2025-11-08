using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BugSpawner : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve spawnRate;
    [SerializeField]
    private List<GameObject> spawnList;
    [SerializeField]
    private List<Transform> spawnPointList;
    [SerializeField]
    private Transform bugParent;
    [SerializeField]
    private int maxSpawnAmount = 5;

    public bool dontSpawn;

    private float spawnTime;
    private List<GameObject> activeBugs = new List<GameObject>();

    private void Start()
    {
        spawnTime = spawnRate.Evaluate(Time.time) + Time.time;
        //SpawnBugs();
    }

    public void Update()
    {
        if (dontSpawn) return;
        if (spawnTime < Time.time && GameManager.Instance.notEnded)
        {
            spawnTime = spawnRate.Evaluate(Time.time) + Time.time;
            SpawnBugs();
        }
    }

    private void SpawnBugs()
    {
        int bugNum = Random.Range(0, maxSpawnAmount);
        for (int i = 0; i < bugNum; i++)
        {
            int spawnIndex = Random.Range(0, spawnList.Count);
            int spawnPointIndex = Random.Range(0, spawnPointList.Count);

            GameObject bug = Instantiate(spawnList[spawnIndex], spawnPointList[spawnPointIndex].position, Quaternion.identity, bugParent);
            activeBugs.Add(bug);
            GameManager.Instance.AddBug(bug);
        }
    }

    public void SpawnMoth()
    {
        GameObject bug = Instantiate(spawnList[1], spawnPointList[0].position, Quaternion.identity, bugParent);
        activeBugs.Add(bug);
        GameManager.Instance.AddBug(bug);
    }

    public void SpawnFlea()
    {
        GameObject bug = Instantiate(spawnList[0], spawnPointList[0].position, Quaternion.identity, bugParent);
        activeBugs.Add(bug);
        GameManager.Instance.AddBug(bug);
    }

    public void SpawnBeetle()
    {
        GameObject bug = Instantiate(spawnList[2], spawnPointList[0].position, Quaternion.identity, bugParent);
        activeBugs.Add(bug);
        GameManager.Instance.AddBug(bug);
    }

    [Button("Spawn Bugs")]
    public void DebugSpawnBug()
    {
        SpawnBugs();
    }

    [Button("Destroy All Bugs")]
    public void DestroyAllBugs()
    {
        activeBugs.RemoveAll(bug => bug == null);

        // destroy all active bugs
        foreach (GameObject bug in activeBugs)
        {
            if (bug != null)
            {
                GameManager.Instance.RemoveBug(bug);
                Destroy(bug);
            }
        }

        activeBugs.Clear();
    }

 
    public void OnBugDestroyed(GameObject bug)
    {
        activeBugs.Remove(bug);
    }

    // get current bug count
    public int GetActiveBugCount()
    {
        activeBugs.RemoveAll(bug => bug == null);
        return activeBugs.Count;
    }
}