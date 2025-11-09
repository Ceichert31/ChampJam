using DG.Tweening;
using UnityEngine;

public class SpiderKillbox : MonoBehaviour, IDestroy
{
    [SerializeField]
    private Transform eatPoint;

    public bool CheckGoalType(BugType type)
    {
        return true;
    }

    public void DestroyBug(GameObject bug)
    {
        bug.transform.DOMove(eatPoint.position, 0.3f).OnComplete(() => {  //Add points

            //minus points text
            GameManager.Instance.AddScore(-50);
            DamageNumbers.Create(-50, transform.position, Color.red);
            GameManager.Instance.RemoveBug(bug);
            SoundManager.Instance.PlaySpiderBite();
            SoundManager.Instance.PlayBugDie();


            GameManager.Instance.RepLossOnDeath();

            Destroy(bug);
        });
        
    }
}

public interface IDestroy
{
    void DestroyBug(GameObject bug);
    bool CheckGoalType(BugType type);
}
