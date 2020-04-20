using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public static int MAX_FOOD= 15;
    private float autoFoodConsumeCycle = 10.0f;
    public int currentFood;

    private float autoConsumeTimer;
    void Awake()
    {
        currentFood = MAX_FOOD;
        autoConsumeTimer = autoFoodConsumeCycle;
    }

    public void restore(int qty)
    {
        currentFood += qty;
    }

    public void consume(int qty)
    {
        if (currentFood > 0)
        {
            currentFood -= qty;
        }
        if(currentFood == 0)
        {
            GameObject.FindObjectOfType<GameManager>().gameOver();
        }
    }

    // Update is called once per frame
    void Update()
    { 
        if (autoConsumeTimer < 0)
        {
            consume(1);
            autoConsumeTimer = autoFoodConsumeCycle;
        }
        else
        {
            autoConsumeTimer -= Time.deltaTime;
        }
    }
}
