using UnityEngine;

public class SpiderKillbox : MonoBehaviour, IDestroy
{
    public void DestroyBug(GameObject bug)
    {
        //Add points
        GameManager.Instance.AddScore(-100);
        GameManager.Instance.RemoveBug(bug);
        Destroy(bug);
    }
}

public interface IDestroy
{
    void DestroyBug(GameObject bug);
}
