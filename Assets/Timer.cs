using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    [SerializeField] GameObject winConditionUI; // Reference to the win condition UI GameObject
    public GameObject gameCanvas;
    public GameObject gameCanvas2;
    public GameObject gameCanvas3;
    public GameObject gameCanvas4;
    public GameObject gameCanvas5;
    public GameObject gameCanvas6;

    void Awake()
    {
        timerText.outlineWidth = 0.1f;
        timerText.outlineColor = new Color32(0, 0, 0, 255);

        // Ensure the win condition UI is initially inactive
        if (winConditionUI != null)
        {
            winConditionUI.SetActive(false);
        }
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else
        {
            remainingTime = 0;
            // MethodForFailure
            timerText.color = Color.red;

            // Check if remaining time is exactly 0, then enable the win condition UI
            if (remainingTime == 0)
            {
                EnableWinConditionUI();
            }
        }

        string minutes = Mathf.Floor(remainingTime / 60).ToString("00");
        string seconds = (remainingTime % 60).ToString("00");
        timerText.text = $"{minutes}:{seconds}";
    }

    void EnableWinConditionUI()
    {
        // Enable the win condition UI here
        if (winConditionUI != null)
        {
            winConditionUI.SetActive(true);
            gameCanvas.SetActive(false);
            gameCanvas2.SetActive(false);
            gameCanvas3.SetActive(false);
            gameCanvas4.SetActive(false);
            gameCanvas5.SetActive(false);
            gameCanvas6.SetActive(false);

        }
    }
}
