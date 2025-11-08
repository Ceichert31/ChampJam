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

    public GameObject spawner;
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
        spawner.SetActive(false);
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
            spawner.SetActive(true);
            spiderDropDown.EnableSpider();
        }

        switch (index)
        {
            case 1:
                bugSpawner.SpawnMoth();
                break;
            case 2:
                bugSpawner.SpawnFlea();
                break;
            case 3:
                bugSpawner.SpawnBeetle();
                break;
        }

        tutorialList[index].transform.DOMoveX(-230, 0.3f).SetEase(Ease.InOutBack).OnComplete(() =>
        {
            index++;

            if (index >= tutorialList.Count) return;

            tutorialList[index].transform.DOMoveX(275, 0.3f).SetEase(Ease.InOutBack);
        });
    }

    private void StartTutorial()
    {
        tutorialList[0].transform.DOMoveX(275, 0.3f).SetEase(Ease.InOutBack);
    }
}
