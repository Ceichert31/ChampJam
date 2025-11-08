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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if bug type add points and destroy bug
        if (collision.transform.gameObject.layer == bugLayer)
        {
            destroyLogic.DestroyBug(collision.gameObject);
            bugEaten?.Invoke(gameObject, null);
        }
    }
}
