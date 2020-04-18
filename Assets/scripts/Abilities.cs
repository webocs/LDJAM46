using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    Vision vision;
    Food food;
    public GameObject visualLandmark;
    

    float visionCycle = 2.0f;
    float visionTimer;

    int visionUpgrade = 1;
    int previousVisionAoe;
    int checkVision;
    private GameObject placedVisualLandmark;

    private void Awake()
    {
        vision = GetComponent<Vision>();
        food = GetComponent<Food>();
        checkVision = 0;
    }

    public void flapWings()
    {
        if (food.currentFood > 1)
        {
            previousVisionAoe = vision.visionAoe;
            vision.visionAoe += visionUpgrade;
            food.consume(1);
            visionTimer = visionCycle;
            checkVision +=1;
        }
    }
    public void consumeFood(int qty)
    {
        food.consume(qty);
    }
    public void eat()
    {
        Edible e= MapManager.getEdibleAt(transform.position);
        if (e) food.restore(e.eat());
    }

    public void leaveVisualLandmark()
    {
        GameObject.Destroy(placedVisualLandmark);
        placedVisualLandmark = GameObject.Instantiate(visualLandmark, transform.position, Quaternion.identity);
    }

    public void recall()
    {
        if (placedVisualLandmark)
        {
            flightTo(placedVisualLandmark.transform.position);
            GameObject.Destroy(placedVisualLandmark);
            placedVisualLandmark = null;
        }
    }

    public void flightTo(Vector2 position)
    {
        transform.position = position;
    }

    private void Update()
    {
        if(checkVision>0)
            if (visionTimer < 0)
            {
                vision.visionAoe = previousVisionAoe;
                checkVision -=1;
                previousVisionAoe -= 1;
                visionTimer = visionCycle;
            }
            else
            {
                visionTimer -= Time.deltaTime;
            }    
    }

   
}
