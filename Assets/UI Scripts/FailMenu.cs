using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FailMenu : MonoBehaviour
{
    public CanvasGroup[] uiElements;
    public CanvasGroup FailMen;
    public GameObject failMenu;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI Timer;
    //edit timer here
    public float remainTime = 900;
    public static bool isFail;
    public int score;
    
    void Awake()
    {
        failMenu.SetActive(true);
        FailMen.alpha = FailMen.alpha = 0; 
        FailMen.blocksRaycasts = false;
    }

    // Update is called once per frame
    void Update()
    {
        //add timer condition
        if (!isFail) {
            UpdateScore();
            CheckFail();
            Countdown();
        }
    }

    void UpdateScore() {
        if (scoreText != null) {
            score = int.Parse(scoreText.text);
        }
    }

    public void CheckFail() {
        if (score < -1000) {
            isFail = true;
            Fail();
        }
    }

    void Countdown() {
        if (remainTime > 0) {
            remainTime -= Time.deltaTime;
        } else {
            remainTime = 0;

            if (remainTime == 0) {
                Fail();
            }
        }
    }

    public void Fail() {

        FailMen.alpha = FailMen.alpha = 1; 
        FailMen.blocksRaycasts = true;

        foreach (CanvasGroup uiElements in uiElements) {
            uiElements.alpha = 0f;
            uiElements.blocksRaycasts = false;
            Time.timeScale = 0;
        } 
    }
}
