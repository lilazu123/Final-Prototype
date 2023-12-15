using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    private string previousScene;

    private void Start()
    {
        // Initially, set the previous scene to the main menu
        previousScene = "MainMenu";
    }

    public void GoToScene(string sceneName)
    {
        // Load the requested scene
        SceneManager.LoadScene(sceneName);

        // Set the previous scene to the current scene
        previousScene = SceneManager.GetActiveScene().name;
    }

    public void GoToPreviousScene()
    {
        // Load the previous scene
        SceneManager.LoadScene(previousScene);
    }
}
