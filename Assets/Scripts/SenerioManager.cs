using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioManager : MonoBehaviour
{
    public TextMeshProUGUI scenarioText; // UI element for the scenario description.
    public Button[] choiceButtons; // Buttons for player choices.
    public TextMeshProUGUI feedbackText; // UI element for feedback.
    public TextMeshProUGUI scoreText; // UI element for the score.

    public Scenario[] scenarios; // Array of Scenario ScriptableObjects.
    private int currentScenarioIndex = 0; // Tracks the current scenario.

    [Header("Game Settings")]
    public int initialScore = 0; // Initial score at the start of the game.
    public int pointsToAdd = 5; // Points added for a correct choice.
    public float defaultSuspensionTime = 1.0f; // Default delay before loading the next scenario.

    private int score;

    void Start()
    {
        score = initialScore; // Initialize score.
        UpdateScoreDisplay();
        LoadScenario();
    }

    public void LoadScenario()
    {
        if (currentScenarioIndex < scenarios.Length)
        {
            Scenario currentScenario = scenarios[currentScenarioIndex];
            scenarioText.text = currentScenario.description;

            // Update buttons based on the current scenario choices.
            for (int i = 0; i < choiceButtons.Length; i++)
            {
                if (i < currentScenario.choices.Length)
                {
                    choiceButtons[i].gameObject.SetActive(true);

                    TextMeshProUGUI buttonText = choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                    if (buttonText != null)
                    {
                        buttonText.text = currentScenario.choices[i];
                    }
                    else
                    {
                        Debug.LogError($"Button {choiceButtons[i].name} is missing a TextMeshProUGUI component!");
                    }

                    choiceButtons[i].onClick.RemoveAllListeners(); // Clear old listeners

                    int index = i; // Store the current value of i in a local variable
                    choiceButtons[i].onClick.AddListener(() =>
                    {
                        Debug.Log("Button clicked: " + choiceButtons[index].name);
                        MakeChoice(index); // Use the local index variable
                    });
                }
                else
                {
                    choiceButtons[i].gameObject.SetActive(false); // Hide unused buttons
                }
            }

            feedbackText.text = ""; // Clear feedback.
        }
        else
        {
            EndGame();
        }
    }

    public void MakeChoice(int index)
    {
        Debug.Log($"Button {index} clicked");
        Scenario currentScenario = scenarios[currentScenarioIndex];

        if (index == currentScenario.correctChoiceIndex)
        {
            if (!string.IsNullOrEmpty(currentScenario.correctFeedback))
            {
                feedbackText.text = $"Feedback:\nCorrect! {currentScenario.correctFeedback}";
                score += pointsToAdd; // Add points for a correct answer.
                UpdateScoreDisplay();
                Debug.Log("Correct! Score: " + score);
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(currentScenario.falseFeedback))
            {
                feedbackText.text = $"Feedback:\nIncorrect. {currentScenario.falseFeedback}";
                Debug.Log("Incorrect. Score remains: " + score);
            }
        }

        // Use the suspension time from the scenario or default value.
        float suspensionTime = currentScenario.suspention > 0 ? currentScenario.suspention : defaultSuspensionTime;
        currentScenarioIndex++;
        Invoke("LoadScenario", suspensionTime); // Delay before the next scenario.
    }

    private void EndGame()
    {
        feedbackText.text = "Game Over! Thanks for playing.";
        // Optionally, transition to a results screen here.
    }

    public void UpdateScoreDisplay()
    {
        Debug.Log("Updating score display. Current score: " + score);
        scoreText.text = $"Score: {score}";
    }
}
