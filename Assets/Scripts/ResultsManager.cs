using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI resultsText; // UI element to display results.

    [Header("Game Settings")]
    [Tooltip("Points added per scenario. Used for calculating total score.")]
    public int pointsPerScenario = 1; // Points added per scenario. Set in the Inspector.

    [Header("Messages")]
    public string resultMessageFormat = "You scored {0}/{1}.\nReflect and try again for better understanding!"; // Result message format.

    [Header("PlayerPrefs Keys")]
    public string scoreKey = "Score"; // Key used for storing the score in PlayerPrefs.
    public string totalScenariosKey = "TotalScenarios"; // Key used for storing the total scenarios in PlayerPrefs.

    [Header("Other Settings")]
    public int defaultScore = 0; // Default score value if no score is saved.
    public int defaultTotalScenarios = 5; // Default value for total scenarios.

    void Start()
    {
        // Retrieve score and total scenarios from PlayerPrefs, with default values.
        int score = PlayerPrefs.GetInt(scoreKey, defaultScore);
        int totalScenarios = PlayerPrefs.GetInt(totalScenariosKey, defaultTotalScenarios);

        // Display the result message.
        resultsText.text = string.Format(resultMessageFormat, score, totalScenarios);
    }

    public void RestartGame()
    {
        // Restart the game by loading the main menu.
        SceneManager.LoadScene("MainMenu");
    }
}
