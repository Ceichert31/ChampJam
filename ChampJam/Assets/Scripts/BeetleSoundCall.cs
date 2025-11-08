using UnityEngine;

public class BeetleSoundCall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            SoundManager.Instance.PlayRhinoHit();
        }
    }
}
