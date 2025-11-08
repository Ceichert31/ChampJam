using Unity.VisualScripting;
using UnityEngine;

public class FlyAgent : MonoBehaviour
{
    [SerializeField]
    private float agentSpeed = 1.0f;

    [SerializeField]
    private float fleeDistance = 0.5f;

    [SerializeField]
    private float targetChangeTimer = 5f;

    private float changeTimer;

    private IPathfinder pathfinder;

    private Rigidbody2D rb;

    private Vector2 target;

    private Vector3 SpiderPos => GameManager.Instance.SpiderPos.position;

    private void Start()
    {
        pathfinder = GetComponent<IPathfinder>();

        rb = GetComponent<Rigidbody2D>();

        target = GameManager.Instance.GetRandomPointInBounds();

        changeTimer = Time.time + targetChangeTimer;
    }

    private void Update()
    {
        CalculateTarget();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = pathfinder.GetPathVelocity((target - new Vector2(transform.position.x, transform.position.y)).normalized) * agentSpeed;
        transform.up = Vector2.Lerp(transform.up, rb.linearVelocity, Time.deltaTime);
    }

    void CalculateTarget()
    {
        if (Vector3.Distance(transform.position, target) < 0.1f || changeTimer < Time.time)
        {
            changeTimer = Time.time + targetChangeTimer;
            target = GameManager.Instance.GetRandomPointInBounds();
        }

        var toSpider = (SpiderPos - transform.position).normalized;

        Debug.DrawRay(transform.position, toSpider, Color.red);
        //Debug.DrawRay(transform.position, target, Color.blue);
        Debug.DrawLine(transform.position, target, Color.blue);

        if (Vector3.Distance(transform.position, SpiderPos) < fleeDistance)
        {
            target = -toSpider;
        }
    }
}
