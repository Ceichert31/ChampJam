using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class SpiderString : MonoBehaviour
{
    private LineRenderer lineRenderer;

    public Transform spider;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lineRenderer.SetPosition(1, spider.position);
    }

    [Button("Deploy Spider")]
    public void EnableSpider()
    {
        spider.DOMoveY(0, 5f).SetEase(Ease.InOutBack).OnComplete(() =>
        {
            spider.GetComponent<SpiderAI>().enabled = true;
        });
    }
}
