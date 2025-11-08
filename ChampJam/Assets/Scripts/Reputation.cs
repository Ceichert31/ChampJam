using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class Reputation : MonoBehaviour
{
    [SerializeField] float reputationScore;
    public float getReputation() { return reputationScore; }
    public void setReputation(float value)
    {
        reputationScore = value;
        foreach(Slider bar in reputationBars)
        {
            bar.value = value / 2.0f;
        }

        setRating();
    }

    [SerializeField] float reputationThreshold;
    public float getReputationThreshold() { return reputationThreshold; }
    public void setReputationThreshold(float value)
    {
        reputationThreshold = value;
        foreach(Slider bar in reputationBars)
        {
            bar.maxValue = value / 2.0f;
        }
    }

    [SerializeField] int rating;
    public int getRating() { return rating; }
    public void setRating()
    {

        if (reputationScore <= ratingThresholds[4])
        {
            rating = 5;
            ratingTF.text = ratingPhrases[4];
        }

        else if (reputationScore <= ratingThresholds[3])
        {
            rating = 4;
            ratingTF.text = ratingPhrases[3];
        }

        else if (reputationScore <= ratingThresholds[2])
        {
            rating = 3;
            ratingTF.text = ratingPhrases[2];
        }

        else if (reputationScore <= ratingThresholds[1])
        {
            rating = 2;
            ratingTF.text = ratingPhrases[1];
        }

        else if (reputationScore <= ratingThresholds[0])
        {
            rating = 1;
            ratingTF.text = ratingPhrases[0];
        }

        else
        {
            rating = 0;
        }

        for (int i = 0; i < ratingStars.Count; i++)
        {
            if(i <= rating - 1) //Light up Stars for Rating
            {
                Debug.Log("setting white " + ratingStars[i]);
                ratingStars[i].color = new Color(255, 255, 255, 255);
                ratingStars[i].sprite = ratingStarOn;
            }

            else //Toggle off missing rating stars
            {
                Debug.Log("toggle off " + ratingStars[i]);
                ratingStars[i].color = new Color(80, 80, 80, 255);
                ratingStars[i].sprite = ratingStarOff;
            }
        }
    }


    [SerializeField] List<Slider> reputationBars;
    [SerializeField] List<Image> ratingStars;
    [SerializeField] Sprite ratingStarOn;
    [SerializeField] Sprite ratingStarHalf;
    [SerializeField] Sprite ratingStarOff;
    [SerializeField] List<float> ratingThresholds;
    [SerializeField] TextMeshProUGUI ratingTF;
    [SerializeField] List<string> ratingPhrases;
    //[SerializeField] Color ratingOffColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        reputationScore += 0.01f;
        setReputation(reputationScore);
    }
}
