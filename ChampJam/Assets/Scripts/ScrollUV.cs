using UnityEngine;

public class ScrollUV : MonoBehaviour
{
    enum ScrollType { LINEAR, SINE, SHIFT, BOUNCE /*unimplemented*/ }

    [SerializeField] ScrollType scrollTypeX = ScrollType.LINEAR;
    [SerializeField] ScrollType scrollTypeY = ScrollType.LINEAR;
    [SerializeField] float scrollSpeedX = 1.0f;
    [SerializeField] float scrollSpeedY = 1.0f;

    Material material;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        material.mainTextureOffset += GetOffset();
    }

    Vector2 GetOffset()
    {
        return new Vector2(GetScroll(scrollTypeX, scrollSpeedX), GetScroll(scrollTypeY, scrollSpeedY));
    }

    float GetScroll(ScrollType scrollType, float scrollSpeed)
    {
        switch (scrollType)
        {
            case ScrollType.LINEAR:
                return LinearScroll(scrollSpeed);
            case ScrollType.SINE:
                return SineScroll(scrollSpeed);
            case ScrollType.SHIFT:
                return ShiftScroll(scrollSpeed);
            default:
                return 0;
        }
    }

    float LinearScroll(float scrollSpeed)
    {
        return scrollSpeed * Time.deltaTime;
    }

    float SineScroll(float scrollSpeed)
    {
        return scrollSpeedX * Mathf.Sin(Time.time) * Time.deltaTime;
    }

    float ShiftScroll(float scrollSpeed)
    {
        return scrollSpeedX * Mathf.Abs(Mathf.Sin(Time.time)) * Time.deltaTime;
    }
}
