using UnityEngine;

public class SineWavePath : MonoBehaviour, IPathfinder
{
    private Vector2 GoalPosition => GameManager.Instance.bugGoal.position;

    [SerializeField]
    private float frequency = 2.0f;

    [SerializeField]
    private float amplitude = 2.0f;

    public Vector2 GetPathVelocity(Vector2 position)
    {
        var target = (GoalPosition - position).normalized;

        float x = Mathf.Sin(Time.time * frequency) * amplitude;
        float y = Mathf.Sin(Time.time * frequency) * amplitude;

        target += new Vector2(x, y);

        return target;
    }
}
