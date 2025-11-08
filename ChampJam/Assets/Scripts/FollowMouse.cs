using UnityEngine;

public class FollowMouse2D : MonoBehaviour
{
    void Update()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;


        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);


        transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, transform.position.z);
    }
}