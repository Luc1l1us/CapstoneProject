using UnityEngine;
using TMPro;

public class Driver : MonoBehaviour
{
    public string driverName;
    public int score;
    public TextMeshProUGUI scoreText;
    public int maxSpeed; // Example speed limit


    private float timeWithoutViolations = 0f;
    private float violationThreshold = 10f;

    private bool isTogglingRight = false;
    private bool isTogglingLeft = false;
    public GameObject rightSpotlight;
    public GameObject leftSpotlight;


    // Update is called once per frame
    void Update()
    {
        timeWithoutViolations += Time.deltaTime;
        // Check for overspeeding
        if (GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
        {
            speedViolation();
        }
        if(timeWithoutViolations >= violationThreshold)
        {
            goodBehavior();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleSpotlightPeriodically(rightSpotlight, ref isTogglingRight);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleSpotlightPeriodically(leftSpotlight, ref isTogglingLeft);
        }
        UpdateScoreText();

    }
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
    void ToggleSpotlight(GameObject spotlight)
    {
        // Toggle the spotlight state
        spotlight.SetActive(!spotlight.activeSelf);
    }

    void ToggleSpotlightPeriodically(GameObject spotlight, ref bool isToggling)
    {
        isToggling = !isToggling;

        if (isToggling)
        {
            InvokeRepeating("ToggleSpotlightHelper", 0f, 1f);
        }
        else
        {
            CancelInvoke("ToggleSpotlightHelper");
            spotlight.SetActive(false);
        }
    }

    // Helper method for InvokeRepeating
    void ToggleSpotlightHelper()
    {
        ToggleSpotlight(isTogglingRight ? rightSpotlight : leftSpotlight);
    }

    void RecordViolation(string violation)
    {
        Debug.Log(driverName + " violated: " + violation);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "AICar")
        {
            score -= 20;
            RecordViolation("Colliding with other cars");
            resetTimer();
        }
    }

    public void speedViolation()
    {
        score -= 10;
        RecordViolation("Overspeeding");
        resetTimer();
    }
    public void trafficViolation()
    {
        score -= 15;
        RecordViolation("Ignoring traffic lights");
        resetTimer();
    }

    private void goodBehavior()
    {
        score += 30;
        resetTimer();

    }
    private void resetTimer()
    {
        timeWithoutViolations = 0f;
    }

}
