using System.Collections.Generic;
using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve spawnRate;

    [SerializeField]
    private List<GameObject> spawnList;

    [SerializeField]
    private Transform bugParent;

    private float spawnTime;

    private void Start()
    {
        spawnTime = spawnRate.Evaluate(Time.time) + Time.time;
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
        int bugNum = Random.Range(0, 3);

        for (int i = 0; i < bugNum; i++)
        {
            //Spawn
            int spawnIndex = Random.Range(0, spawnList.Count - 1);

            Instantiate(spawnList[spawnIndex], bugParent);
        }
    }
}
