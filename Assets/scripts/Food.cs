using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    const int MAX_FOOD= 15;
    private float autoFoodConsumeCycle = 3.0f;
    public int currentFood;
    public UITxtCurrentFood txtFood;

    private float autoConsumeTimer;
    void Awake()
    {
        currentFood = MAX_FOOD;
        autoConsumeTimer = autoFoodConsumeCycle;
        txtFood = FindObjectOfType<UITxtCurrentFood>();
    }

    public void restore(int qty)
    {
        currentFood += qty;
        txtFood.UpdateFood(currentFood);
    }

    public void consume(int qty)
    {
        if (currentFood > 0)
        {
            currentFood -= qty;
            txtFood.UpdateFood(currentFood);
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
