using UnityEngine;


public class GoalKillbox : MonoBehaviour, IDestroy
{
    //[SerializeField] public BugType goalType;

    [SerializeField] private GameObject damageNumber;

    private ObjectShake shake;

    private void Awake()
    {
        shake = GetComponent<ObjectShake>();

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

        GameManager.Instance.IncrementBugsSaved();

        GameManager.Instance.AddScore(100);
        GameManager.Instance.RemoveBug(bug);

        Destroy(bug);
    }

    public bool CheckGoalType(BugType type)
    {
        /* if (goalType == type) return true;
         return false;*/

        switch (type)
        {
            case BugType.MOTH:
                UIController.Instance.firstMoth = true;
                UIController.Instance.AdvanceTutorial();
                break;
            case BugType.FLY:
                UIController.Instance.firstFlea = true;
                UIController.Instance.AdvanceTutorial();
                break;

            case BugType.DEFAULT:
                UIController.Instance.firstBeetle = true;
                UIController.Instance.AdvanceTutorial();
                break;
        }

        return true;
    }
}
