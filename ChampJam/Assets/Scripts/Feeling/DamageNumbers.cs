using UnityEngine;
using TMPro;

public class DamageNumbers : MonoBehaviour
{
    public static DamageNumbers instance;

    [Header("Animation Settings")]
    [SerializeField] private float popDuration = 0.2f;
    [SerializeField] private float riseDuration = 1.0f;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float riseSpeed = 2f;
    [SerializeField] private float popScale = 1.2f;

    [Header("Setup")]
    [SerializeField] private GameObject damageNumberPrefab;
    [SerializeField] private Transform canvasTransform;

    private void Awake()
    {
        instance = this;
    }

    // call this from any script
    public static void Create(int damage, Vector3 worldPosition)
    {
        Create(damage.ToString(), worldPosition, Color.red);
    }

    public static void Create(int damage, Vector3 worldPosition, Color color)
    {
        Create(damage.ToString(), worldPosition, color);
    }

    public static void Create(string text, Vector3 worldPosition, Color color)
    {
        if (instance == null) return;

        GameObject obj = Instantiate(instance.damageNumberPrefab, instance.canvasTransform);
        RectTransform rt = obj.GetComponent<RectTransform>();

        // world to screen position
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPosition);

        // convert screen position to canvas space
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            instance.canvasTransform.GetComponent<RectTransform>(),
            screenPos,
            null,
            out Vector2 localPoint);

        rt.anchoredPosition = localPoint;

        obj.GetComponent<DamageNumberDisplay>().Initialize(text, color,
            instance.popDuration, instance.riseDuration, instance.fadeDuration,
            instance.riseSpeed, instance.popScale);
    }
}
