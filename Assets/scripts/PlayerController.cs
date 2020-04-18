using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Abilities abilities;
    GameManager gameManager;
    const int UPPER_BOUNDARY = 10;
    const int RIGHT_BOUNDARY = 10;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        abilities = GetComponent<Abilities>();
    }

    // Update is called once per frame
    void Update()
    {
        checkMovement();
    }

    void checkMovement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            movePlayer(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            movePlayer(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            movePlayer(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            movePlayer(Vector2.right);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            abilities.flapWings();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            abilities.eat();
        } 
        if (Input.GetKeyDown(KeyCode.F))
        {
            abilities.leaveVisualLandmark();
        }  
        if (Input.GetKeyDown(KeyCode.Q))
        {
            abilities.recall();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameManager.restart();
        }

    }

    void movePlayer(Vector2 direction)
    {
        bool canMove = false;
        // Moving up or down
        if (direction.x == 0)
        {
            if(transform.position.y>=0 && transform.position.y < UPPER_BOUNDARY)
            {
                canMove = true;
            }
            else
            {
                canMove = (transform.position.y == 0 && direction == Vector2.up)
                    || (transform.position.y == UPPER_BOUNDARY && direction == Vector2.down);
            }
        }
        // Moving left or right
        else if (direction.y == 0)
        {
            if (transform.position.x > 0 && transform.position.x < RIGHT_BOUNDARY)
            {
                canMove = true;
            }
            else
            {
                canMove = (transform.position.x == 0 && direction == Vector2.right)
                    || (transform.position.x == RIGHT_BOUNDARY && direction == Vector2.left);
            }
        }

        if (canMove)
        {
            abilities.consumeFood(1);
            Tile targetTile = MapManager.tileAt((Vector2)transform.position + direction);
            Debug.Log(targetTile);
            if(targetTile && targetTile.isWalkable)
                transform.Translate(direction);
        }

    }
}
