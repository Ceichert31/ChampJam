using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
    private void LateUpdate()
    {
        GetComponent<Transform>().eulerAngles = Vector3.zero;
    }
}
