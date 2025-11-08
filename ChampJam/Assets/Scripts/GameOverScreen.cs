using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] RectTransform screen;
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
        //Make open vertically on the y

    }
    // Update is called once per frame
    void Update()
    {
        
    }


    
}
