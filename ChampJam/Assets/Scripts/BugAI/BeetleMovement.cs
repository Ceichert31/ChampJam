using UnityEngine;

public class BeetleMovement : MonoBehaviour, IDirection
{
    public Vector2 GetTargetDirection(Vector2 position, Vector2 lastLightPos)
    {
        return position / 2;
    }
}
