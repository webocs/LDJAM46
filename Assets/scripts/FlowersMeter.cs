using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowersMeter : MonoBehaviour
{
    void Update()
    {
        Text txt = GetComponentInChildren<Text>();
        if (txt) txt.text = MapManager.getRemaining<Flower>()+"";
    }
}
