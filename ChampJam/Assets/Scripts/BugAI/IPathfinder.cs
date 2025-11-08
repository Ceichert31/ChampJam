using UnityEngine;

//Goals for pathfinder
//Avoid spider
//Move towards goal

public interface IPathfinder
{
    Vector2 Goal { get; }
    Vector2 GetPathVelocity(Vector2 position);
}