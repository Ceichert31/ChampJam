using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public enum BugType
{
    MOTH,
    FLY,
    DEFAULT
}


public class FlyAgent : MonoBehaviour
{
    [Header("Fly Data")]
    // improtant
    public BugType bugType = BugType.DEFAULT;
    public bool inRadius = false;
    public bool litWhenInRadius = false;
    public Transform latestLightPos;
    // end of important

    [Header("Fly Settings")]

    [SerializeField]
    private float agentSpeed = 1.0f;
    [SerializeField]
    private float fleeDistance = 0.5f;
    [SerializeField]

    private float targetChangeTimer = 5f;
    private float changeTimer;

    private IPathfinder pathfinder;
    private IDirection direction;
    private Rigidbody2D rb;
    private Vector2 target;

    private void Start()
    {
        latestLightPos = transform;
        pathfinder = GetComponent<IPathfinder>();
        direction = GetComponent<IDirection>();
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
        PerformPathing();
    }

    private void PerformPathing()
    {
        if (!inRadius || !GameManager.Instance.GetLightState())
        {
            Debug.Log("normal pathfding");
            rb.linearVelocity = pathfinder.GetPathVelocity((target - new Vector2(transform.position.x, transform.position.y)).normalized) * agentSpeed;
            transform.up = Vector2.Lerp(transform.up, rb.linearVelocity, Time.deltaTime);
            //end early
            return;
        }

        // move towards light
        rb.linearVelocity = pathfinder.GetPathVelocity(direction.GetTargetDirection(transform.position, latestLightPos.position)) * agentSpeed;
        transform.up = Vector2.Lerp(transform.up, rb.linearVelocity, Time.fixedDeltaTime * 3f);
    }
    private void CalculateTarget()
    {
        if (Vector2.Distance(transform.position, target) < 0.1f || changeTimer < Time.time)
        {
            changeTimer = Time.time + targetChangeTimer;
            target = GameManager.Instance.GetRandomPointInBounds();
        }
        Debug.DrawLine(transform.position, target, Color.blue);
    }
}