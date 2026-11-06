using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinCondition : MonoBehaviour
{

    public CanvasGroup[] uiElements;
    public GameObject winMenu;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI Timer;
    public TextMeshProUGUI timerElapsedText;
    public CanvasGroup WinCon;
    public static int score;
    private static bool isWin = false;
    

    // Start is called before the first frame update
    void Start()
    {
        winMenu.SetActive(true);
        WinCon.alpha = WinCon.alpha = 0; 
        WinCon.blocksRaycasts = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWin) {
            UpdateScore();
            //edit score for score completion and add timer condition if necessary
            if (score >= 150) {
                isWin = true;
                Win();
            }   
        }      
    }

    public void UpdateScore() {
        if (scoreText != null) {
            string svalue = scoreText.text;
            score = int.Parse(svalue);
        }
    }

   public void ElapsedTime() {
        //set Timer here
        int setTime = 900;
        string remTime = Timer.text;
        
        string[] splittime = remTime.Split(':');
        int min = int.Parse(splittime[0]);
        int sec = int.Parse(splittime[1]);

        float totsec = min * 60 + sec;
        float timeelap = setTime - totsec;

        string minutes = Mathf.Floor(timeelap / 60).ToString("00");
        string seconds = (timeelap % 60).ToString("00");
        timerElapsedText.text = $"{minutes}:{seconds}";
    } 

    public void Win() {

        isWin = true;

        ElapsedTime();

        WinCon.alpha = WinCon.alpha = 1; 
        WinCon.blocksRaycasts = true;
        Time.timeScale = 0;

        foreach (CanvasGroup uiElements in uiElements) {
            uiElements.alpha = 0f;
            uiElements.blocksRaycasts = false;
            Time.timeScale = 0;
        } 
    }
}
