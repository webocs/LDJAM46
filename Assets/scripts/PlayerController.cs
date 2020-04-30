using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip moveSound;

    Abilities abilities;
    GameManager gameManager;
    public float moveSpeed;
    const int UPPER_BOUNDARY = 15;
    const int RIGHT_BOUNDARY = 15;
    private bool isMoving;
    private Animator animator;
    private bool goingRight;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        abilities = GetComponent<Abilities>();
        animator = GetComponent<Animator>();
        goingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        checkMovement();
    }

    void checkMovement()
    {
        if (!isMoving)
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
                if (goingRight)
                {
                    animator.SetTrigger("goLeft");
                    goingRight = false;
                }

            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                movePlayer(Vector2.right);
                if (!goingRight)
                {
                    animator.SetTrigger("goRight");
                    goingRight = true;
                }
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
            Tile targetTile = MapManager.tileAt((Vector2)transform.position + direction);
            if (targetTile && targetTile.isWalkable)
            {
                IEnumerator co = smoothTranslate(direction);
                GameObject.Find("SoundPlayer").GetComponent<AudioSource>().clip = moveSound;
                GameObject.Find("SoundPlayer").GetComponent<AudioSource>().Play();
                StartCoroutine(co);
                abilities.consumeFood(1);
                gameManager.tick();

            }
        }

    }

    IEnumerator smoothTranslate(Vector2 direction)
    {
        isMoving = true;
        float startime = Time.time;
        Vector2 startPostion = transform.position; //Starting position.
        Vector2 endPosition = (Vector2)transform.position + direction; //Ending position.

        while (Vector2.Distance(transform.position,endPosition)> 0.3f && ((Time.time - startime) * moveSpeed) < 1f)
        {
            float move = Mathf.Lerp(0, 1, (Time.time - startime) * moveSpeed);
            transform.position += (Vector3)(direction * move);
            yield return null;
        }
        isMoving = false;
        transform.position = endPosition;
    }
}
