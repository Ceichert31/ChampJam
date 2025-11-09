using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AutoFadeOut : MonoBehaviour
{
    void Start()
    {
        GetComponent<Image>().DOColor(Color.clear, 1);
    }
}
