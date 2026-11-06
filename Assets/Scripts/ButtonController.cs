using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    // Function to be called when the "Restart" button is clicked
    public void OnRestartButtonClick()
    {
        // Get the current scene index and reload the scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    // Function to be called when the "Next" button is clicked
    public void OnNextButtonClick()
    {
        // Load the main menu scene (assuming main menu is at build index 0)
        SceneManager.LoadScene(1);
    }
}
