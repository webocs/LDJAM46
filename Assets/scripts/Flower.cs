﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : Edible
{
    float reRollDelay = 1.0f;
    float reRollTimer = 1.0f;


    private void Update()
    {
        calculateVisibility();
        if (reRollTimer < 0)
        {
            reRollTimer = reRollDelay;
            int dice = Random.Range(0, 20);
            if (dice > 5 && dice < 7) MakeNoise();
        }
        else
        {
            reRollTimer -= Time.deltaTime;
        }
    }

}
