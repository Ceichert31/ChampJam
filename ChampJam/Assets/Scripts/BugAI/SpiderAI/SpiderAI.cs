using UnityEngine;

public class SpiderAI : MonoBehaviour
{
    [SerializeField]
    private float agentSpeed = 1.0f;

    private Rigidbody2D rb;

    private Vector2 target;

    [SerializeField]
    private float wanderDelay = 5f;
    private float wanderTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        target = GameManager.Instance.GetRandomPointInBounds();
        wanderTimer = wanderDelay;
    }

    //Spider logic 
    //Pause after catching bug
    //Get random target when no bugs 

    private Vector2 CalculateTarget()
    {
        //If there are valid bugs, get bug target
        if (GameManager.Instance.HasNextTarget(transform.position))
        {
            target = GameManager.Instance.GetNextTarget(transform.position);
        }

        //If reached destination, get next point
        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            //get new target
            target = GameManager.Instance.GetNextTarget(transform.position);
        }

        //Count down timer 
        wanderTimer -= Time.deltaTime;
        if (wanderTimer <= 0)
        {
            wanderTimer = wanderDelay;
            target = GameManager.Instance.GetNextTarget(transform.position);
        }

        Debug.DrawLine(transform.position, target, Color.red);

        return (target - new Vector2(transform.position.x, transform.position.y)).normalized;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = CalculateTarget() * agentSpeed;
        transform.up = Vector2.Lerp(transform.up, rb.linearVelocity, Time.deltaTime);
    }
}
