using UnityEngine;

public class GoalBehavior : MonoBehaviour
{
    [SerializeField]
    private int bugLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if bug type add points and destroy bug
        if (collision.transform.gameObject.layer == bugLayer)
        {
            Destroy(collision.gameObject);

            //Add points
            GameManager.Instance.AddScore(100);
        }
    }
}
