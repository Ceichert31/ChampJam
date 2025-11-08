using System;
using UnityEngine;

public class BugColliderDetector : MonoBehaviour
{
    [SerializeField]
    private int bugLayer;

    private IDestroy destroyLogic;

    public EventHandler bugEaten;

    private void Start()
    {
        destroyLogic = GetComponent<IDestroy>();
        goal = GetComponent<GoalKillbox>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if bug type add points and destroy bug
        if ((collision.transform.gameObject.layer == bugLayer) && (collision.transform.gameObject.TryGetComponent(out AIAgent agent)))
        {
            if (!destroyLogic.CheckGoalType(agent.bugType))
                return;
                
            destroyLogic.DestroyBug(collision.gameObject);
            bugEaten?.Invoke(gameObject, null);
        }
    }
}
