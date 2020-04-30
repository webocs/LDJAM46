using System.Collections;
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
    private int MOVE_EVERY_TURNS = 2;
    private int movementStepper;

    // Start is called before the first frame update
    void Start()
    {
        movementTimer = movementDelay;
        movementStepper = MOVE_EVERY_TURNS; 
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        ignoreVisibilityCheck = isMoving;
        calculateVisibility();
        if (!isMoving)
        {           
            rollForNoise();
        }
    }

    override public void move()
    {
        movementStepper -= 1;
        if (movementStepper <= 0)
        {
            List<Vector2> availableMoves = MapManager.getSurounding((Vector2)transform.position, true);
            Vector2 targetMove = availableMoves[Random.Range(0, availableMoves.Count)];
            IEnumerator co = smoothTranslate(targetMove);
            StartCoroutine(co);
            MakeNoise();
            movementStepper = MOVE_EVERY_TURNS;
        } 
    }

    private void rollForNoise()
    {
        if (reRollTimer < 0)
        {
            reRollTimer = reRollDelay;
            int dice = Random.Range(0, 20);            
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
