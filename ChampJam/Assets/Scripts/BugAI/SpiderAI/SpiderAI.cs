using System;
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

    [SerializeField]
    private float eatDelay = 5f;

    private bool canMove = true;

    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        target = GameManager.Instance.GetRandomPointInBounds();
        wanderTimer = wanderDelay;

        GetComponent<BugColliderDetector>().bugEaten += FreezeMovement;

        animator = GetComponent<Animator>();
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

    /// <summary>
    /// Pauses spider movement for a certain time duration
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FreezeMovement(object sender, EventArgs e)
    {
        canMove = false;
        animator.SetTrigger("Eat");
        animator.SetBool("IsChewing", true);
        Invoke(nameof(ResetMovement), eatDelay);
    }
    private void ResetMovement()
    {
        canMove = true;
        animator.SetBool("IsChewing", false);
    }
    private void FixedUpdate()
    {
        if (!canMove)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = CalculateTarget() * agentSpeed;
    }
}
