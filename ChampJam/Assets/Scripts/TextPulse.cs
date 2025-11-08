using UnityEngine;
using TMPro;

public class TextAnimation : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI textComponent;

    [Header("Scale Settings")]
    public float minScale = 0.5f;
    public float maxScale = 1.5f;
    public float scaleSpeed = 2f;

    [Header("Fade Settings")]
    public float minAlpha = 0f;
    public float maxAlpha = 1f;
    public float fadeSpeed = 1.5f;

    private float scaleTime;
    private float fadeTime;
    private Vector3 originalScale;

    private void Start()
    {
        // Get TextMeshProUGUI component if not assigned
        if (textComponent == null)
        {
            textComponent = GetComponent<TextMeshProUGUI>();
        }

        originalScale = transform.localScale;
    }

    private void Update()
    {
        AnimateScale();
        AnimateFade();
    }

    private void AnimateScale()
    {
        scaleTime += Time.deltaTime * scaleSpeed;
        float scale = Mathf.Lerp(minScale, maxScale, (Mathf.Sin(scaleTime) + 1f) / 2f);
        transform.localScale = originalScale * scale;
    }

    private void AnimateFade()
    {
        fadeTime += Time.deltaTime * fadeSpeed;
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, (Mathf.Sin(fadeTime) + 1f) / 2f);

        Color color = textComponent.color;
        color.a = alpha;
        textComponent.color = color;
    }
}