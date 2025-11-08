using Unity.VisualScripting;
using UnityEngine;

public class FlyAgent : MonoBehaviour
{
    [SerializeField]
    private float agentSpeed = 1.0f;

    [SerializeField]
    private float fleeDistance = 0.5f;

    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float viewRange = 0.7f;

    private IPathfinder pathfinder;

    private Rigidbody2D rb;

    private Vector3 SpiderPos => GameManager.Instance.SpiderPos.position;

    private void Start()
    {
        pathfinder = GetComponent<IPathfinder>();

        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = pathfinder.GetPathVelocity(CalculateTarget()) * agentSpeed;
        transform.up = Vector2.Lerp(transform.up, rb.linearVelocity, Time.deltaTime);
    }

    Vector2 CalculateTarget()
    {
        var target = (GameManager.Instance.BugGoal.position - transform.position).normalized;

        var toSpider = (SpiderPos - transform.position).normalized;

        Debug.DrawRay(transform.position, toSpider, Color.red);
        Debug.DrawRay(transform.position, target, Color.blue);

        if (Vector3.Dot(toSpider, target) > viewRange && Vector3.Distance(transform.position, SpiderPos) < fleeDistance)
        {
            return target -toSpider;
        }

        return target;
    }
}
