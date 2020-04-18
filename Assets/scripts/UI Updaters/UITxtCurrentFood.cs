using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITxtCurrentFood : MonoBehaviour
{
    Text text;
    void Awake()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    public void UpdateFood(int value)
    {
        text.text = string.Format("Current food: {0}", value);
    }
}
