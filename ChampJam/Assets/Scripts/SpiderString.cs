using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class SpiderString : MonoBehaviour
{
    private LineRenderer lineRenderer;

    public Transform spider;
    public Transform spiderStringChild;

    private bool hasBeenDeployed;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (hasBeenDeployed)
        {
            lineRenderer.SetPosition(1, spiderStringChild.position);
            return;
        }
        lineRenderer.SetPosition(1, spider.position);
    }

    [Button("Deploy Spider")]
    public void EnableSpider()
    {
        spider.DOMoveY(0, 5f).SetEase(Ease.InOutBack).OnComplete(() =>
        {
            spider.GetComponent<SpiderAI>().enabled = true;
            spiderStringChild.parent = null;
            spiderStringChild.DOMoveY(10, 2f).SetEase(Ease.InOutBack);
            hasBeenDeployed = true;
        });
    }
}
