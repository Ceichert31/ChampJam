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

    private float spawnTime;

    private void Start()
    {
        spawnTime = spawnRate.Evaluate(Time.time) + Time.time;
        SpawnBugs();
    }

    public void Update()
    {
        if (spawnTime < Time.time)
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
            //Spawn
            int spawnIndex = Random.Range(0, spawnList.Count);

            int spawnPointIndex = Random.Range(0, spawnPointList.Count);

            GameManager.Instance.AddBug(Instantiate(spawnList[spawnIndex], spawnPointList[spawnPointIndex].position, Quaternion.identity, bugParent));
        }
    }

    [Button("Spawn Bugs")]
    public void DebugSpawnBug()
    {
        SpawnBugs();
    }
}
