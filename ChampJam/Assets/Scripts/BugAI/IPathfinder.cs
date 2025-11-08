using UnityEngine;

//Goals for pathfinder
//Avoid spider
//Move towards goal

public interface IPathfinder
{
    Vector2 GetPathVelocity(Vector2 position);
}

public interface IDirection
{
    Vector2 GetTargetDirection(Vector2 position, Vector2 lastLightPos);
}