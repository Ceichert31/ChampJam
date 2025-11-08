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

    [SerializeField] float rating;
    public float getRating() { return rating; }
    public void setRating()
    {

        if (reputationScore <= ratingThresholds[9]) //5 STARS
        {
            rating = 5;
            ratingTF.text = ratingPhrases[9];
        }

        else if (reputationScore <= ratingThresholds[8]) //4.5 STARS
        {
            rating = 4.5f;
            ratingTF.text = ratingPhrases[8];
        }

        else if (reputationScore <= ratingThresholds[7]) //4 STARS
        {
            rating = 4;
            ratingTF.text = ratingPhrases[7];
        }

        else if (reputationScore <= ratingThresholds[6]) //3.5 STARS
        {
            rating = 3.5f;
            ratingTF.text = ratingPhrases[6];
        }

        else if (reputationScore <= ratingThresholds[5]) //3 STARS
        {
            rating = 3;
            ratingTF.text = ratingPhrases[5];
        }

        else if (reputationScore <= ratingThresholds[4]) //2.5 STARS
        {
            rating = 2.5f;
            ratingTF.text = ratingPhrases[4];
        }

        else if (reputationScore <= ratingThresholds[3]) //2 STARS
        {
            rating = 2;
            ratingTF.text = ratingPhrases[3];
        }

        else if (reputationScore <= ratingThresholds[2]) //1.5 STARS
        {
            rating = 1.5f;
            ratingTF.text = ratingPhrases[2];
        }

        else if (reputationScore <= ratingThresholds[1]) //1 STAR
        {
            rating = 1;
            ratingTF.text = ratingPhrases[1];
        }

        else if (reputationScore <= ratingThresholds[0]) //0.5 STARS
        {
            rating = 0.5f;
            ratingTF.text = ratingPhrases[0];
        }

        else //0 STAR
        {
            rating = 0;
        }

        for (int i = 0; i < ratingStars.Count; i++)
        {
            if (i == rating - 0.5)
            {
                Debug.Log("rating is " + rating + " and this star is " + i);
                ratingStars[i].sprite = ratingStarHalf;
            }

            else if (i <= rating - 1) //Light up Stars for Rating
            {
                //Debug.Log("setting white " + ratingStars[i]);
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
        reputationScore += 0.005f;
        setReputation(reputationScore);
    }
}
