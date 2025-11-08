using UnityEngine;

public class GoalKillbox : MonoBehaviour, IDestroy
{
    public void DestroyBug(GameObject bug)
    {
        //Add points
        //Play particles
        //Play sfx
        GameManager.Instance.AddScore(100);
        GameManager.Instance.RemoveBug(bug);
        DestroyBug(bug);
    }
}
