using System;
using UnityEngine;

public class BugColliderDetector : MonoBehaviour
{
    [SerializeField]
    private int bugLayer;

    private IDestroy destroyLogic;

    public EventHandler bugEaten;

    private GoalKillbox goal;

    private void Start()
    {
        destroyLogic = GetComponent<IDestroy>();
        goal = GetComponent<GoalKillbox>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if bug type add points and destroy bug
        if ((collision.transform.gameObject.layer == bugLayer) && (collision.transform.gameObject.GetComponent<FlyAgent>().bugType == goal.goalType))
        {
            destroyLogic.DestroyBug(collision.gameObject);
            bugEaten?.Invoke(gameObject, null);
        }
    }
}
