using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightcontrol : MonoBehaviour
{
    public Light greenLight;
    public Light redLight;

    public bool greenLightE;
    public bool redLightE;


    // Start is called before the first frame update
    void Start()
    {
        greenLight.enabled = false;
        redLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(greenLightE == true)
        {
            greenLight.enabled = true;
            redLight.enabled = false;
        }
        else
        {
            greenLight.enabled = false;
            redLight.enabled = true;
        }
        
        
    }
}
