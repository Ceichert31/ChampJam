using UnityEngine;
using TMPro;

public class DamageNumbers : MonoBehaviour
{
    [Header("Animation Settings")]

    [SerializeField] private float popDuration = 0.2f;
    [SerializeField] private float riseDuration = 1.0f;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float riseSpeed = 2f;
    [SerializeField] private float popScale = 1.2f;

    [Header("References")]

    private TextMeshProUGUI tmpText;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private float timer;
    private Vector3 originalScale;
    private Vector3 startPosition;
    private float totalDuration;

    private void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        originalScale = transform.localScale;
        //startPosition = rectTransform.anchoredPosition;
        totalDuration = popDuration + riseDuration;

        Initialize(100, Color.green);
    }

    public void Initialize(int damage, Color color)
    {
        tmpText.text = damage.ToString();
        tmpText.color = color;
        timer = 0f;
    }

    public void Initialize(string text, Color color)
    {
        tmpText.text = text;
        tmpText.color = color;
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // pop in
        if (timer < popDuration)
        {
            float popProgress = timer / popDuration;
            float scale = Mathf.Lerp(0f, popScale, EaseOutElastic(popProgress));
            transform.localScale = originalScale * scale;
        }
        // rise up
        else if (timer < totalDuration)
        {
            if (transform.localScale.x > originalScale.x)
            {
                transform.localScale = originalScale;
            }

            float riseProgress = (timer - popDuration) / riseDuration;
            float yOffset = riseProgress * riseSpeed * 100f;
            rectTransform.anchoredPosition = (Vector2)startPosition + Vector2.up * yOffset;

            // fade out
            float fadeStartTime = totalDuration - fadeDuration;
            if (timer > fadeStartTime)
            {
                float fadeProgress = (timer - fadeStartTime) / fadeDuration;
                canvasGroup.alpha = 1f - fadeProgress;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // elastic bounce
    private float EaseOutElastic(float t)
    {
        float c4 = (2f * Mathf.PI) / 3f;
        return t == 0f ? 0f : t == 1f ? 1f :
            Mathf.Pow(2f, -10f * t) * Mathf.Sin((t * 10f - 0.75f) * c4) + 1f;
    }
}