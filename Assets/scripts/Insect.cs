﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insect : Edible
{
    public float movementDelay;
    public float reRollDelay;
    public float moveSpeed;

    private float reRollTimer;
    private float movementTimer;
    private bool isMoving;
    
    // Start is called before the first frame update
    void Start()
    {
        movementTimer = movementDelay;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        ignoreVisibilityCheck = isMoving;
        calculateVisibility();
        if (!isMoving)
        {
            movementTimer -= Time.deltaTime;
            if (movementTimer < 0)
            {
                move();
                movementTimer = movementDelay;
            }
            rollForNoise();
        }
    }

    private void move()
    {
        List<Vector2> availableMoves = MapManager.getSurounding((Vector2)transform.position, true);
        Vector2 targetMove = availableMoves[Random.Range(0, availableMoves.Count)];
        IEnumerator co = smoothTranslate(targetMove);
        StartCoroutine(co);
    }

    private void rollForNoise()
    {
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

    IEnumerator smoothTranslate(Vector2 direction)
    {
        float startime = Time.time;
        Vector2 startPostion = transform.position; //Starting position.
        Vector2 endPosition = (Vector2)transform.position + direction; //Ending position.
        isMoving = true;
        while (Vector2.Distance(transform.position, endPosition) > 0.1f && ((Time.time - startime) * moveSpeed) < 1f)
        {
            Debug.Log(Vector2.Distance(startPostion, endPosition));
            float move = Mathf.Lerp(0, 1, (Time.time - startime) * moveSpeed);
            transform.position += (Vector3)(direction * move);
            yield return null;
        }
        transform.position = endPosition;
        isMoving = false;
    }


}
