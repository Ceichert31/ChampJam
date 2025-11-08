using DG.Tweening;
using UnityEngine;

public class SpiderKillbox : MonoBehaviour, IDestroy
{
    [SerializeField]
    private Transform eatPoint;

    public void DestroyBug(GameObject bug)
    {
        bug.transform.DOMove(eatPoint.position, 0.3f).OnComplete(() => {  //Add points

            //minus points text
            GameManager.Instance.AddScore(-100);
            GameManager.Instance.RemoveBug(bug);
            Destroy(bug);
        });
        
    }
}

public interface IDestroy
{
    void DestroyBug(GameObject bug);
}
