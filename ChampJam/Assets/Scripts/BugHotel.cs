using DG.Tweening;
using UnityEngine;

public class BugHotel : MonoBehaviour
{
    public void ActivateHotel()
    {
        transform.DOScaleY(1, 0.3f);
    }

    public void DeactivateHotel()
    {
        transform.DOScaleY(0, 0.3f);
    }
}
