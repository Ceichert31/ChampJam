using UnityEngine;

//Goals for pathfinder
//Avoid spider
//Move towards goal

public interface IPathfinder
{
    Vector3 getPathVelocity(Vector3 position);
}