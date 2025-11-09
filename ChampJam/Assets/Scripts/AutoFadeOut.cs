using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AutoFadeOut : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(DoThing), 0.1f);
    }

    void DoThing()
    {
        GetComponent<Image>().DOColor(Color.clear, 1);
    }
}
