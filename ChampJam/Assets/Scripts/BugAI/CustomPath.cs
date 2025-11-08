using UnityEngine;

public class CustomPath : MonoBehaviour, IPathfinder
{

    [SerializeField]
    private AnimationCurve agentPattern;

    [SerializeField]
    private float intensity = 1f;
    public Vector2 GetPathVelocity(Vector2 position)
    {
        var offset = new Vector2(agentPattern.Evaluate(Time.time) * intensity, agentPattern.Evaluate(Time.time) * intensity);

        return offset + position;
    }
}
