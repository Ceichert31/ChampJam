using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField]
    List<GameObject> tutorialList;

    private int index;

    public bool firstIgnite;

    private void Awake()
    {
        Instance = this;    
    }
    private void Start()
    {
        if (tutorialList.Count <= 0)
        {
            return;
        }
        Invoke(nameof(StartTutorial), 0.5f);
    }

    [Button("Advance Tutorial")]
    public void AdvanceTutorial()
    {
        if (tutorialList.Count <= 0)
        {
            return;
        }

        DOTween.CompleteAll();
        //Spawn spider

        if (index > tutorialList.Count) return;

        if (index == tutorialList.Count - 1)
        {
            //Spawn spider

        }

        tutorialList[index].transform.DOScaleY(0, 0.3f).SetEase(Ease.InBounce).OnComplete(() =>
        {
            index++;

            if (index >= tutorialList.Count) return;

            tutorialList[index].transform.DOScaleY(1, 0.3f).SetEase(Ease.InBounce);
        });
    }

    private void StartTutorial()
    {
        tutorialList[0].transform.DOScaleY(1, 0.3f).SetEase(Ease.InBounce);
    }
}
