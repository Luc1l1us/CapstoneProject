using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossings : MonoBehaviour
{
    public float road1timer = 10f;
    public float road2timer = 10f;
    public float road3timer = 10f;
    public float road4timer = 10f;
    private float startTime;

    public GameObject road1;
    public GameObject road2;
    public GameObject road3;
    public GameObject road4;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TrafficLightCycle());
        startTime = Time.time;
    }

    // Update is called once per frame

    IEnumerator TrafficLightCycle()
    {
        while (true)
        {
            var Road1 = road1.GetComponent<smartRoad>();
            var Road2 = road2.GetComponent<smartRoad>();
            var Road3 = road3.GetComponent<smartRoad>();
            var Road4 = road4.GetComponent<smartRoad>();

            Road1.isGreen = true;
            Road2.isGreen = false;
            Road3.isGreen = false;
            Road4.isGreen = false;
            Debug.Log("Road1 is Green go go go");
            Debug.Log("Time elapsed: " + (Time.time - startTime).ToString("F2") + " seconds");
            yield return new WaitForSeconds(road1timer);

            Road1.isGreen = false;
            Road2.isGreen = true;
            Road3.isGreen = false;
            Road4.isGreen = false;
            Debug.Log("Road2 is Green go go go");
            Debug.Log("Time elapsed: " + (Time.time - startTime).ToString("F2") + " seconds");
            yield return new WaitForSeconds(road2timer);

            Road1.isGreen = false;
            Road2.isGreen = false;
            Road3.isGreen = true;
            Road4.isGreen = false;
            Debug.Log("Road3 is Green go go go");
            Debug.Log("Time elapsed: " + (Time.time - startTime).ToString("F2") + " seconds");
            yield return new WaitForSeconds(road3timer);

            Road1.isGreen = false;
            Road2.isGreen = false;
            Road3.isGreen = false;
            Road4.isGreen = true;
            Debug.Log("Road3 is Green go go go");
            Debug.Log("Time elapsed: " + (Time.time - startTime).ToString("F2") + " seconds");
            yield return new WaitForSeconds(road4timer);

            // Reset the currentCar when the light turns red
        }
    }

    void Update()
    {

    }

}