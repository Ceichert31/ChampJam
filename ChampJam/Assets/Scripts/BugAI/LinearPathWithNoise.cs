using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class LinearPathWithNoise : MonoBehaviour, IPathfinder
{
    public Vector2 Goal => GameManager.Instance.bugGoal.position;

    [SerializeField]
    private float intensity = 1.0f;

    public Vector2 GetPathVelocity(Vector2 position)
    {
        float x = Mathf.PerlinNoise(Random.Range(0, 999), Random.Range(0, 999));
        float y = Mathf.PerlinNoise(Random.Range(0, 999), Random.Range(0, 999));

        Vector2 noise = new Vector2(x, y);

        return position * noise * intensity;
    }
}
