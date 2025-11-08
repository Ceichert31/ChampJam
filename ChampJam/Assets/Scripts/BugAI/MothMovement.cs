using UnityEngine;

public class MothMovement : MonoBehaviour, IDirection
{
    [SerializeField]
    private float agentInLightMult = 1.5f;
    public Vector2 GetTargetDirection(Vector2 position, Vector2 lastLightPos)
    {
        Vector2 toLightDir = (lastLightPos - (Vector2)transform.position).normalized;
        return toLightDir * agentInLightMult;
    }
}
