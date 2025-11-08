using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public enum BugType
{
    MOTH,
    FLY,
    DEFAULT
}


public class AIAgent : MonoBehaviour
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
    private float targetChangeTimer = 5f;
    private float changeTimer;

    [SerializeField]
    private float rotationSpeed = 5f;

    private IPathfinder pathfinder;
    private IDirection direction;
    private Rigidbody2D rb;
    private Vector2 target;

    private Animator animator;

    private void Start()
    {
        latestLightPos = transform;
        pathfinder = GetComponent<IPathfinder>();
        direction = GetComponent<IDirection>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
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
        float animSpeed = (agentSpeed * rb.linearVelocity.magnitude);
        animSpeed = Mathf.Clamp(animSpeed, 0.5f, 1.5f);
        animator.speed = animSpeed;

        if (!inRadius || !GameManager.Instance.GetLightState())
        {
            rb.linearVelocity = pathfinder.GetPathVelocity((target - new Vector2(transform.position.x, transform.position.y)).normalized) * agentSpeed;
            transform.up = Vector2.Lerp(transform.up, rb.linearVelocity, Time.fixedDeltaTime * rotationSpeed);
            //end early
            return;
        }

        // move towards light
        rb.linearVelocity = pathfinder.GetPathVelocity(direction.GetTargetDirection(transform.position, latestLightPos.position)) * agentSpeed;
        transform.up = Vector2.Lerp(transform.up, rb.linearVelocity, Time.fixedDeltaTime * rotationSpeed);
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