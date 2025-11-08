using Unity.VisualScripting;
using UnityEngine;
public class FlyAgent : MonoBehaviour
{
    [System.Serializable]
    public enum BugType
    {
        MOTH,
        FLY,
        DEFAULT
    }

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
    private Rigidbody2D rb;
    private Vector2 target;

    private Vector3 SpiderPos => GameManager.Instance.SpiderPos.position;

    private void Start()
    {
        latestLightPos = transform;
        pathfinder = GetComponent<IPathfinder>();
        rb = GetComponent<Rigidbody2D>();
        target = GameManager.Instance.GetRandomPointInBounds();

        changeTimer = Time.time + targetChangeTimer;

        // temporary 50/50 for bug decisions
        if (Random.value > 0.5f)
        {
            bugType = BugType.MOTH;
        }
        else
        {
            bugType = BugType.FLY;
        }
    }

    private void Update()
    {
        CalculateTarget();
        Debug.Log("inRadius: " + inRadius);
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

        switch (bugType)
        {
            case BugType.MOTH:
                MothLightPathing();
                Debug.Log("moth pathfding");
                break;

            case BugType.FLY:
                FlyLightPathing();
                Debug.Log("fly pathfding");
                break;

            case BugType.DEFAULT:
                Debug.Log("this shouldn't happen");
                break;
        }
    }

    private void MothLightPathing()
    {
        // move towards light
        Vector2 toLightDir = ((Vector2)latestLightPos.position - (Vector2)transform.position).normalized;
        rb.linearVelocity = pathfinder.GetPathVelocity(toLightDir) * agentSpeed;
        transform.up = Vector2.Lerp(transform.up, rb.linearVelocity, Time.fixedDeltaTime * 3f);
    }

    private void FlyLightPathing()
    {
        // flee from light
        Vector2 awayFromLightDir = ((Vector2)transform.position - (Vector2)latestLightPos.position).normalized;
        rb.linearVelocity = pathfinder.GetPathVelocity(awayFromLightDir) * agentSpeed;
        transform.up = Vector2.Lerp(transform.up, rb.linearVelocity, Time.fixedDeltaTime * 3f);
    }

    private void CalculateTarget()
    {
        if (Vector2.Distance(transform.position, target) < 0.1f || changeTimer < Time.time)
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