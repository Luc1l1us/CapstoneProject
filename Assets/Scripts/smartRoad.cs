using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smartRoad : MonoBehaviour
{
    [SerializeField] Queue<Aicontroller> trafficQueue = new Queue<Aicontroller>();
    public Aicontroller currentCar;

    public bool isGreen;

    void Start()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AICar") || other.CompareTag("Astar"))
        {
            var car = other.GetComponentInParent<Aicontroller>();
           

            // Ensure that the 'car' and 'currentCar' are not null before comparing them.
            if (car != null && (currentCar == null || car != currentCar))
            {
                
                trafficQueue.Enqueue(car);
                
                if (isGreen == false)
                {
                    car.shouldStop = true;
                }
                
            }
            else
            {
                Debug.Log("Aicontroller not found on " + car.name);
            }
        }
        if(other.CompareTag("Player"))
        {
            var player = other.GetComponentInParent<Driver>();

            if(isGreen == false)
            {
                player.trafficViolation();
            }
        }
    }
    private void Update()
    {
        if (currentCar == null )
        {
            if (trafficQueue.Count > 0)
            {
                if(isGreen == true)
                {
                    currentCar = trafficQueue.Dequeue();
                   
                    currentCar.shouldStop = false;
                }

            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AICar") || other.CompareTag("Astar"))
        {
            var car = other.GetComponentInParent<Aicontroller>();
            if (car != null)
            {
                RemoveCar(car);
            }
        }
    }

    private void RemoveCar(Aicontroller car)
    {
        if (car == currentCar)
        {
            currentCar = null;
        }
    }
}
