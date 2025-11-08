using Unity.VisualScripting;
using UnityEngine;

public class FlyAgent : MonoBehaviour
{
    [SerializeField]
    private float agentSpeed = 1.0f;

    private IPathfinder pathfinder;

    private Rigidbody2D rb;

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

        var toSpider = (GameManager.Instance.SpiderPos.position - transform.position).normalized;

        Debug.DrawRay(transform.position, toSpider, Color.red);
        Debug.DrawRay(transform.position, target, Color.blue);

        if (Vector2.Dot(target, toSpider) > 0.6f)
        {
            return target + toSpider;
        }

        return target - toSpider;
    }
}
