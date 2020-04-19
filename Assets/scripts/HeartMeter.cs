using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartMeter : MonoBehaviour
{
    Food food;
    public Sprite fullHealth;
    public Sprite halfHealth;
    public Sprite lowHealth;
    Image healthIcon;
    Text healthText;

    private void Awake()
    {
        food = FindObjectOfType<Food>();
        healthIcon = GetComponentInChildren<Image>();
        healthText = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        
        if (food.currentFood > (Food.MAX_FOOD / 2))
        {
            healthIcon.sprite = fullHealth;
        } else if(food.currentFood > (Food.MAX_FOOD / 7))
        {
            healthIcon.sprite = halfHealth;
        }
        else
        {
            healthIcon.sprite = lowHealth;
        }
        healthText.text = food.currentFood+"";

    }

}
