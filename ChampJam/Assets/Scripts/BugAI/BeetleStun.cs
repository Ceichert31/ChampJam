using UnityEngine;

public class BeetleStun : MonoBehaviour
{
    private AIAgent agent;

    private void Start()
    {
        agent = GetComponent<AIAgent>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            //Stop ai from moving 
            //Launch in direction
            agent.StunBug(collision.gameObject.transform.position);
            SoundManager.Instance.PlayRhinoHit();
        }

    }
}
