using UnityEngine;

public class ScrollUV : MonoBehaviour
{
    [SerializeField] float scrollSpeedX = 1.0f;
    [SerializeField] float scrollSpeedY = 1.0f;

    [SerializeField] Material material;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        float offsetX = scrollSpeedX * Time.deltaTime;
        float offsetY = scrollSpeedY * Time.deltaTime;

        material.mainTextureOffset += new Vector2(offsetX, offsetY);
    }
}
