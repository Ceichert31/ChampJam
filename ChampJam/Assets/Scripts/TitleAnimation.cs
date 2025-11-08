using UnityEngine;
using TMPro;

public class TitleAnimation : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationAngle = 15f;
    public float rotationSpeed = 1f;

    [Header("Pulse Settings")]
    public float minScale = 0.9f;
    public float maxScale = 1.1f;
    public float pulseSpeed = 2f;

    private float rotationTime;
    private float pulseTime;
    private Vector3 originalScale;
    private Quaternion originalRotation;

    private void Start()
    {
        originalScale = transform.localScale;
        originalRotation = transform.localRotation;
    }

    private void Update()
    {
        AnimateRotation();
        AnimatePulse();
    }

    private void AnimateRotation()
    {
        rotationTime += Time.deltaTime * rotationSpeed;
        float angle = Mathf.Sin(rotationTime) * rotationAngle;
        transform.localRotation = originalRotation * Quaternion.Euler(0, 0, angle);
    }

    private void AnimatePulse()
    {
        pulseTime += Time.deltaTime * pulseSpeed;
        float scale = Mathf.Lerp(minScale, maxScale, (Mathf.Sin(pulseTime) + 1f) / 2f);
        transform.localScale = originalScale * scale;
    }
}