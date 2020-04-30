using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public static int MAX_FOOD= 15;
    public int currentFood;

    void Awake()
    {
        currentFood = MAX_FOOD;
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

}
