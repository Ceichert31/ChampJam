using UnityEngine;

public class SineWavePath : MonoBehaviour, IPathfinder
{

    [SerializeField]
    private float frequency = 2.0f;

    [SerializeField]
    private float amplitude = 2.0f;

    public Vector2 GetPathVelocity(Vector2 position)
    {
        float x = Mathf.Sin(Time.time * frequency) * amplitude;
        float y = Mathf.Sin(Time.time * frequency) * amplitude;

        position += new Vector2(x, y);

        return position;
    }
}
