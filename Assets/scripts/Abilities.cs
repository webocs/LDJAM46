using System.Collections;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    Vision vision;
    Food food;
    public GameObject visualLandmark;

    PlayerController playerController;
    float visionCycle = 2.0f;
    float visionTimer;
    int visionUpgrade = 1;
    int previousVisionAoe;
    int checkVision;
    private GameObject placedVisualLandmark;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        vision = GetComponent<Vision>();
        food = GetComponent<Food>();
        checkVision = 0;
    }

    public void flapWings()
    {
        if (food.currentFood > 1 && vision.visionAoe <= vision.MAX_VISION_AOE)
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
        IEnumerator co = smoothFlight(position);
        StartCoroutine(co);
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

    IEnumerator smoothFlight(Vector2 position)
    {
        float startime = Time.time;
        Vector2 startPosition = transform.position; //Starting position.
        Vector2 endPosition = position; //Ending position.
        Vector2 direction = endPosition - startPosition;
        direction.Normalize();

        while (Vector2.Distance(transform.position, endPosition) > 0.3f && ((Time.time - startime) * playerController.moveSpeed) < 1f)
        {
            Debug.Log(Vector2.Distance(startPosition, endPosition));
            float move = Mathf.Lerp(0, 1, (Time.time - startime) * playerController.moveSpeed);
            transform.position += (Vector3)(direction * move);
            yield return null;
        }
        transform.position = endPosition;
    }


}
