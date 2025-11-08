using UnityEngine;

public class ObjectShake : MonoBehaviour
{
    [Header("Shake Settings")]
    [SerializeField] private float intensity = 15f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float duration = 1f;

    private Quaternion originalRotation;
    private bool isShaking = false;
    private float shakeTime = 0f;
    private float elapsedTime = 0f;

    private void Start()
    {
        originalRotation = transform.localRotation;
    }

    private void Update()
    {
        if (isShaking)
        {
            elapsedTime += Time.deltaTime;

            // diminishing
            float diminish = 1f - (elapsedTime / duration);

            // stop when duration reached
            if (elapsedTime >= duration)
            {
                StopShake();
                return;
            }

            shakeTime += Time.deltaTime * speed;
            float z = Mathf.Sin(shakeTime) * intensity * diminish;
            transform.localRotation = originalRotation * Quaternion.Euler(0f, 0f, z);
        }
    }

    public void StartShake()
    {
        // reset to original if already shaking
        if (isShaking)
        {
            transform.localRotation = originalRotation;
        }

        isShaking = true;
        shakeTime = 0f;
        elapsedTime = 0f;
    }

    private void StopShake()
    {
        isShaking = false;
        transform.localRotation = originalRotation;
    }
}