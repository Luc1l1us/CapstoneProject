using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIqueing : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var carGameObject = other.gameObject;
        if (other.CompareTag("AICar"))
        {
            Debug.Log("Should be stopping");
            if (CompareTag("AICar"))
            {
                Debug.Log("Should be stopping");
            }
        }
    }
}
