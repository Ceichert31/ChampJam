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

    private bool dontGetRandomTarget;

    private bool canMove = true;

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
        if (!canMove) return;
        CalculateTarget();
    }

    private void FixedUpdate()
    {
        if (!canMove) return;
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
            //end 
            return;
        }

        dontGetRandomTarget = true;

        switch (bugType)
        {
            case BugType.MOTH:
                // Light movement logic
                target = direction.GetTargetDirection(transform.position, latestLightPos.position);
                rb.linearVelocity = pathfinder.GetPathVelocity(target) * agentSpeed;
                transform.up = Vector2.Lerp(transform.up, rb.linearVelocity, Time.fixedDeltaTime * rotationSpeed);
                break;

            case BugType.FLY:
                // Light movement logic

                target = direction.GetTargetDirection(transform.position, latestLightPos.position);
                rb.linearVelocity = pathfinder.GetPathVelocity(target) * agentSpeed;
                transform.up = Vector2.Lerp(transform.up, rb.linearVelocity, Time.fixedDeltaTime * rotationSpeed);
                break;

            case BugType.DEFAULT: // Light movement logic
                rb.linearVelocity = pathfinder.GetPathVelocity((target - new Vector2(transform.position.x, transform.position.y)).normalized) * agentSpeed;
                transform.up = Vector2.Lerp(transform.up, rb.linearVelocity, Time.fixedDeltaTime * rotationSpeed);
                break;
        }

        Invoke(nameof(ResetRandom), 1f);

        Debug.DrawLine(transform.position, target, Color.blue);
    }

    void ResetRandom() => dontGetRandomTarget = false;
    private void CalculateTarget()
    {
        if (dontGetRandomTarget) return;

        if (Vector2.Distance(transform.position, target) < 0.1f || changeTimer < Time.time)
        {
            changeTimer = Time.time + targetChangeTimer;
            target = GameManager.Instance.GetRandomPointInBounds();
        }
    }

    public void StunBug(Vector2 pos)
    {
        canMove = false;
        rb.linearVelocity = ((Vector2)transform.position - pos).normalized * agentSpeed;
        Invoke(nameof(ResetStun), 0.8f);
    }

    private void ResetStun() => canMove = true;
}