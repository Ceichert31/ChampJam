using UnityEngine;

public class SpiderAI : MonoBehaviour
{
    [SerializeField]
    private float agentSpeed = 1.0f;

    private Rigidbody2D rb;

    private Vector2 target;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        target = GameManager.Instance.GetNextTarget(transform.position).position;

        if (target == null)
            return;

        target = (target - new Vector2(transform.position.x, transform.position.y)).normalized;

        Debug.DrawRay(transform.position, target, Color.red);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = target * agentSpeed;
        transform.up = Vector2.Lerp(transform.up, rb.linearVelocity, Time.deltaTime);
    }
}
