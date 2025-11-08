using TMPro;
using UnityEngine;

public class DamageNumberDisplay : MonoBehaviour
{
    private TextMeshProUGUI tmpText;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private float timer;
    private Vector3 originalScale;
    private Vector3 startPosition;
    private float totalDuration;

    private float popDuration;
    private float riseDuration;
    private float fadeDuration;
    private float riseSpeed;
    private float popScale;

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
        startPosition = transform.position;
    }

    public void Initialize(string text, Color color, float pop, float rise, float fade, float speed, float scale)
    {
        tmpText.text = text;
        tmpText.color = color;
        timer = 0f;

        popDuration = pop;
        riseDuration = rise;
        fadeDuration = fade;
        riseSpeed = speed;
        popScale = scale;

        totalDuration = popDuration + riseDuration;
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
            //rectTransform.anchoredPosition = (Vector2)startPosition + Vector2.up * yOffset;

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