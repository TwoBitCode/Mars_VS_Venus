using UnityEngine;

public class PulseEffect : MonoBehaviour
{
    [Header("Pulse Effect Settings")]
    [Tooltip("How much the object contracts in percentage.")]
    [Range(0f, 100f)] public float contractionPercentage = 40f; // Contraction percentage, adjustable in the inspector.

    [Tooltip("How much the object expands in percentage.")]
    [Range(0f, 100f)] public float expansionPercentage = 20f; // Expansion percentage, adjustable in the inspector.

    [Tooltip("Speed of the pulsing effect.")]
    [Range(0.1f, 10f)] public float pulseSpeed = 3.0f; // Speed of the pulse, adjustable in the inspector.

    // Constants to control scale behavior
    private const float MIN_SCALE_LIMIT = 0f;  // Minimum scale for contraction percentage (0%).
    private const float MAX_SCALE_LIMIT = 100f; // Maximum scale for expansion percentage (100%).

    // Constants for sine wave calculation
    private const float SINE_OFFSET = 1.0f;  // Offset to ensure the sine wave starts from a neutral state (no negative values).
    private const float SINE_NORMALIZER = 2.0f;  // Normalizer to scale the sine wave between 0 and 1.

    private float timer = 0.0f; // Time variable for pulsing.

    private Vector3 initialScale; // Initial object scale
    private Vector3 minScale; // Minimum scale the object can reach
    private Vector3 maxScale; // Maximum scale the object can reach

    private void Start()
    {
        initialScale = transform.localScale; // Save initial scale
    }

    void Update()
    {
        // Calculate the minimum and maximum scales based on the contraction/expansion percentages
        minScale = initialScale * (1 - contractionPercentage / MAX_SCALE_LIMIT); // Minimum scale based on contraction
        maxScale = initialScale * (1 + expansionPercentage / MAX_SCALE_LIMIT); // Maximum scale based on expansion

        // Increment the timer based on time and pulse speed
        timer += Time.deltaTime * pulseSpeed;

        // Calculate the scale factor using a sine wave, normalized to 0..1
        float sineWaveValue = Mathf.Sin(timer);  // Sine wave value between -1 and 1
        float normalizedScale = (sineWaveValue + SINE_OFFSET) / SINE_NORMALIZER;  // Normalize to 0..1

        // Interpolate between the minScale and maxScale (compared to initialScale)
        transform.localScale = Vector3.Lerp(minScale, maxScale, normalizedScale);
    }
}
