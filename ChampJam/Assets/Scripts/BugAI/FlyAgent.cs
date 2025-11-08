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
        return (GameManager.Instance.bugGoal.position - transform.position).normalized;
    }
}
