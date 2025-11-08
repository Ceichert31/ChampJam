using UnityEngine;

public class SpiderAnimatorMethods : MonoBehaviour
{
    public void PlayEatSound()
    {
        SoundManager.Instance.PlaySpiderBite();
    }
    public void PlayChewSound()
    {
        SoundManager.Instance.PlaySpiderEat();
    }
}
