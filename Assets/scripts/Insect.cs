using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insect : Edible
{
    public float movementDelay;
    public float reRollDelay;

    private float reRollTimer;
    private float movementTimer;
    // Start is called before the first frame update
    void Start()
    {
        movementTimer = movementDelay;
    }

    // Update is called once per frame
    void Update()
    {
        calculateVisibility();
        movementTimer -= Time.deltaTime;
        if (movementTimer < 0)
        {
            move();
            movementTimer = movementDelay;
        }
        rollForNoise();
    }

    private void move()
    {
        List<Tile> suroudingTiles = MapManager.getSurounding((Vector2)transform.position, true);
        Tile targetTile = suroudingTiles[Random.Range(0, suroudingTiles.Count)];
        transform.position = targetTile.transform.position;
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


}
