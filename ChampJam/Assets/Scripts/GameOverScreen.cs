using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] GameObject screen;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI bugsLeadScoreTF;
    public TextMeshProUGUI getBugsLeadTF() { return bugsLeadScoreTF; }
    public void setBugsLeadTF(int value) { bugsLeadScoreTF.text = value.ToString(); }
    [SerializeField] TextMeshProUGUI bugsFreedScoreTF;
    public TextMeshProUGUI getBugsFreedTF() { return bugsFreedScoreTF; }
    public void setBugsFreedTF(int value) { bugsFreedScoreTF.text = value.ToString(); }
    [SerializeField] TextMeshProUGUI bugsBattedScoreTF;
    public TextMeshProUGUI getBugsBattedTF() { return bugsBattedScoreTF; }
    public void setBugsBattedTF(int value) { bugsBattedScoreTF.text = value.ToString(); }
    [SerializeField] TextMeshProUGUI totalScoreTF;
    public TextMeshProUGUI getTotalScoreTF() {  return totalScoreTF; }
    public void setTotalScoreTF(int value) {  totalScoreTF.text = value.ToString(); }
    void Start()
    {
        OpenGameOverMenu();

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenGameOverMenu()
    {
        //Opens Vertically on the Y 
        DG.Tweening.Sequence openSequence = DOTween.Sequence();
        openSequence.Append(screen.transform.DOScale(new Vector3(1, 1f, 1), 0.7f));
        openSequence.Append(screen.transform.DOPunchScale(new Vector3(0.0f, 0.05f, 0f), .3f));
        //openSequence.Insert(0,canvasGroup.DOFade(1f, .75f));
    }

    public void ShakeInScores()
    {
        DG.Tweening.Sequence scoreSequence = DOTween.Sequence();
    }



}
