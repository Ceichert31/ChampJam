using UnityEngine;

public class FlyMovement : MonoBehaviour, IDirection
{
    [SerializeField]
    private float agentInLightMult = 1.5f;
    public Vector2 GetTargetDirection(Vector2 position, Vector2 lastLightPos)
    {
        // move towards light
        Vector2 awayFromLightDir = ((Vector2)transform.position - lastLightPos).normalized;
        return awayFromLightDir * agentInLightMult;
    }
}
