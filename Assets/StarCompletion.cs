using System.Collections;
using UnityEngine;
using TMPro;

public class StarCompletion : MonoBehaviour
{
    [SerializeField] Star[] Stars;
    [SerializeField] TMP_Text scoreText;

    [SerializeField] float EnlargeScale = 1.5f;
    [SerializeField] float ShrinkScale = 1f;
    [SerializeField] float EnlargeDuration = 0.25f;
    [SerializeField] float ShrinkDuration = 0.25f;

    public Driver driver;
    void Start()
    {
        // Example: Find Driver script at runtime
    }

    private void Update()
    {
        driver = FindObjectOfType<Driver>();

        if (driver == null)
        {
            Debug.LogError("Driver script not found in the scene!");
            return;
        }

        // Example: Get the score from the Driver script
        int score = driver.score;

        // Example: Find TextMeshPro component at runtime
        scoreText = GetComponentInChildren<TMP_Text>();

        if (scoreText != null)
        {
            // Update the TextMeshPro text with the player's score
            scoreText.text = score.ToString();
            Debug.Log("Score text updated: " + scoreText.text);
        }
        else
        {
            Debug.LogError("No TextMeshPro component found in children!");
        }

        // Example: Call ShowStars with the retrieved score
        ShowStars(score);
    }

    public void ShowStars(int score)
    {
        int numberOfStars = CalculateStars(score);
        StartCoroutine(ShowStarsRoutine(numberOfStars));
    }

    private int CalculateStars(int score)
    {
        if (score >= 100)
        {
            return 3;
        }
        else if (score >= 50)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    private IEnumerator ShowStarsRoutine(int numberOfStars)
    {
        foreach (Star star in Stars)
        {
            star.YellowStar.transform.localScale = Vector3.zero;
        }

        for (int i = 0; i < numberOfStars; i++)
        {
            yield return StartCoroutine(EnlargeAndShrinkStar(Stars[i]));
        }
    }

    private IEnumerator EnlargeAndShrinkStar(Star star)
    {
        yield return StartCoroutine(ChangeStarScale(star, EnlargeScale, EnlargeDuration));
        yield return StartCoroutine(ChangeStarScale(star, ShrinkScale, ShrinkDuration));
    }

    private IEnumerator ChangeStarScale(Star star, float targetScale, float duration)
    {
        Vector3 initialScale = star.YellowStar.transform.localScale;
        Vector3 finalScale = new Vector3(targetScale, targetScale, targetScale);

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            star.YellowStar.transform.localScale = Vector3.Lerp(initialScale, finalScale, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        star.YellowStar.transform.localScale = finalScale * 6;
    }
}
