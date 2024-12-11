using UnityEngine;

[CreateAssetMenu(fileName = "NewScenario", menuName = "Game/Scenario", order = 1)]
public class Scenario : ScriptableObject
{
    [Header("Scenario Details")]
    [TextArea] public string description; // The scenario's text.
    public string[] choices; // Possible responses to the scenario.
    public int correctChoiceIndex; // Index of the correct response.

    [Header("Feedback")]
    [TextArea] public string correctFeedback; // Feedback after selecting the correct option.
    [TextArea] public string falseFeedback; // Feedback after selecting an incorrect option.

    [Header("Game Settings")]
    [Tooltip("Time delay (in seconds) before the next scenario loads.")]
    [Range(0.1f, 10f)] public float suspention = 3f; // Suspension time before the next scenario.
}
