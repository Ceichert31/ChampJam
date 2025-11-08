using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class LinearPathWithNoise : MonoBehaviour, IPathfinder
{
    private Vector2 GoalPosition => GameManager.Instance.bugGoal.position;

    public Vector2 GetPathVelocity(Vector2 position)
    {
        Vector2 target = GoalPosition - position;
        target.Normalize();

        float x = Mathf.PerlinNoise(Random.Range(0, 999), Random.Range(0, 999));
        float y = Mathf.PerlinNoise(Random.Range(0, 999), Random.Range(0, 999));

        Vector2 noise = new Vector2(x, y);

        return target + noise;
    }
}
