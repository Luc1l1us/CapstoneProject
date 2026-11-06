using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public Image targetImage;

    private void Start()
    {
        if (targetImage == null)
        {
            Debug.LogError("Target Image not assigned! Please assign it in the Inspector.");
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("NEWest");
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    public void ControlsShow()
    {
        if (targetImage != null)
        {
            targetImage.enabled = !targetImage.enabled;
        }
    }
}
