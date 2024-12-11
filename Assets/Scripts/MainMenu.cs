using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Constants for the selected sides and scenes
    private const string VENUS_SIDE = "Venus";
    private const string MARS_SIDE = "Mars";
    private const string VENUS_SCENE = "GameSceneVenus";
    private const string MARS_SCENE = "GameSceneMars";

    public void SelectSide(string side)
    {
        PlayerPrefs.SetString("SelectedSide", side);

        // Use constants for scene loading
        if (side == VENUS_SIDE)
        {
            SceneManager.LoadScene(VENUS_SCENE);
        }
        if (side == MARS_SIDE)
        {
            SceneManager.LoadScene(MARS_SCENE);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
