using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] GameObject screen;
    [SerializeField] CanvasGroup canvasGroup;

    [SerializeField] GameObject bugsLead;
    public GameObject getBugsLead() {  return bugsLead; }
    [SerializeField] TextMeshProUGUI bugsLeadScoreTF;
    public TextMeshProUGUI getBugsLeadTF() { return bugsLeadScoreTF; }
    public void setBugsLeadTF(int value) { bugsLeadScoreTF.text = value.ToString(); }
    [SerializeField] TextMeshProUGUI bugsFreedScoreTF;
    [SerializeField] GameObject bugsFreed;
    public GameObject getBugsFreed() {  return bugsFreed; } 
    public TextMeshProUGUI getBugsFreedTF() { return bugsFreedScoreTF; }
    public void setBugsFreedTF(int value) { bugsFreedScoreTF.text = value.ToString(); }
    [SerializeField] TextMeshProUGUI bugsBattedScoreTF;
    [SerializeField] GameObject bugsBatted;
    [SerializeField] GameObject getBugsBatted() { return bugsBatted; }
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

        //Score Shake
        Transform bugLead = bugsLead.transform;
        Transform bugFree = bugsFreed.transform;
        Transform bugBat = bugsBatted.transform;
        Transform total = totalScoreTF.gameObject.transform;
        openSequence.Append(bugLead.DOPunchPosition(new Vector3(15.0f, 0.0f, 0.0f), .5f));
        openSequence.Append(bugFree.DOPunchPosition(new Vector3(15.0f, 0.0f, 0.0f), .5f));
        openSequence.Append(bugBat.DOPunchPosition(new Vector3(15.0f, 0.0f, 0.0f), .5f));
        openSequence.Append(total.DOPunchScale(new Vector3(0.5f, 0.5f, 0.0f), .5f));

    }

    /*public void ShakeInScores()
    {
        DG.Tweening.Sequence bugLeadSequence = DOTween.Sequence();
        Transform bugLead = bugsLeadScoreTF.gameObject.transform;
        bugLeadSequence.Append(bugLead.DOPunchPosition(new Vector3(1.0f, 0.0f, 0.0f), .3f));

        DG.Tweening.Sequence bugFreeSequence = DOTween.Sequence();
        Transform bugFree = bugsFreedScoreTF.gameObject.transform;

        DG.Tweening.Sequence bugBatSequence = DOTween.Sequence();
        Transform bugBat = bugsBattedScoreTF.gameObject.transform;
    }
    */



}
