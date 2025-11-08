using UnityEngine;

public class SpiderAnimatorMethods : MonoBehaviour
{
    public void PlayEatSound()
    {
        SoundManager.Instance.PlayEat();
    }
    public void PlayChewSound()
    {
        SoundManager.Instance.PlayChew();
    }
}
