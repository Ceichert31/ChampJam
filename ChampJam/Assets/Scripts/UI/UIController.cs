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

    public bool firstMoth;
    public bool firstFlea;
    public bool firstBeetle;

    public SpiderString spiderDropDown;

    public BugSpawner bugSpawner;

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
        //Invoke(nameof(StartTutorial), 0.5f);
        StartTutorial();
        bugSpawner.dontSpawn = true;
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
            bugSpawner.dontSpawn = false;
            spiderDropDown.EnableSpider();
            tutorialList[tutorialList.Count - 1].transform.DOMoveX(-700, 0.3f).SetEase(Ease.InOutBack);
            return;
        }

        switch (index)
        {
            case 0:
                bugSpawner.SpawnMoth();
                break;
            case 1:
                bugSpawner.SpawnFlea();
                break;
            case 2:
                bugSpawner.SpawnBeetle();
                break;
        }

        tutorialList[index].transform.DOMoveX(-700, 0.3f).SetEase(Ease.InOutBack).OnComplete(() =>
        {
            index++;

            if (index >= tutorialList.Count) return;

            tutorialList[index].transform.DOMoveX(315, 0.3f).SetEase(Ease.InOutBack);
        });
    }

    private void StartTutorial()
    {
        tutorialList[0].transform.DOMoveX(315, 0.3f).SetEase(Ease.InOutBack);
    }
}
