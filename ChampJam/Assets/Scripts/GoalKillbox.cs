using UnityEngine;


public class GoalKillbox : MonoBehaviour, IDestroy
{
    [SerializeField] public BugType goalType;

    [SerializeField] private GameObject damageNumber;

    private ObjectShake shake;

    private void Awake()
    {
        shake = GetComponent<ObjectShake>();

        // temp for visualizatoin
        switch (goalType)
        {
            case BugType.MOTH:
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.blue;
                break;

            case BugType.FLY:
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
                break;

            default:
                break;
        }
    }

    public void DestroyBug(GameObject bug)
    {
        //Add points
        //Play particles
        //Play sfx
        shake.StartShake();

        Instantiate(damageNumber, transform.position, Quaternion.identity);
        shake.StartShake();
        DamageNumbers.Create(100, transform.position, Color.yellow);

    GameManager.Instance.AddScore(100);
        GameManager.Instance.RemoveBug(bug);

        Destroy(bug);
    }

    public bool CheckGoalType(BugType type)
    {
        if (goalType == type) return true;
        return false;
    }
}
