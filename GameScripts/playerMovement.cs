using SimplePF2D;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private LayerMask obstacleLayer;
    private Rigidbody2D body;

    private Vector2 targetPosition; 
    private bool isMoving = false;

    [SerializeField]
    private TimerTurn timerTurn;

    public Animator animator;

    AudioController audioController;

    private NewEnemyMovement aStarEnemyMoving;
    private NewEnemyMovementDFS dfsEnemyMoving;
    private NewEnemyMovementBFS bfsEnemyMoving;
    private bool isSpam = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        targetPosition = body.position;
        timerTurn = GameObject.Find("PlayerEnemyTurn").GetComponent<TimerTurn>();
        aStarEnemyMoving = GameObject.Find("Ghost").GetComponent<NewEnemyMovement>();
        bfsEnemyMoving = GameObject.Find("Ghost").GetComponent<NewEnemyMovementBFS>();
        dfsEnemyMoving = GameObject.Find("Ghost").GetComponent<NewEnemyMovementDFS>();
        audioController = GameObject.Find("AudioManager").GetComponent<AudioController>();
    }

    private void Update()
    {
        if (!isMoving)
        {
            animator.SetFloat("Speed", 0);
        }

        if ((aStarEnemyMoving.isStationary || bfsEnemyMoving.isStationary || dfsEnemyMoving.isStationary) && (!isMoving && (timerTurn.timer % 2 == 1 || timerTurn.timer == 0)) && !isSpam)
        {
            float horizontalInput = 0f;
            float verticalInput = 0f;

            if (Input.GetKeyDown(KeyCode.W))
            {
                verticalInput = 1;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                verticalInput = -1;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                horizontalInput = -1;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                horizontalInput = 1;
            }

            // If movement input is detected
            if ((horizontalInput != 0 || verticalInput != 0) && !isMoving)
            {
                Vector2 newPosition = body.position;

                if (horizontalInput != 0)
                {
                    animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
                    newPosition = new Vector2(body.position.x + 2 * Mathf.Sign(horizontalInput), body.position.y);

                    if (timerTurn.timer == 0 && !IsObstacleInDirection(newPosition))
                    {
                        audioController.PlaySFX(audioController.dash);
                    }
                    else if (IsObstacleInDirection(newPosition) && timerTurn.timer > 0)
                    {
                        audioController.PlaySFX(audioController.hitWall);
                    }
                    else if (timerTurn.timer > 0)
                    {
                        audioController.PlaySFX(audioController.dash);
                    }
                }
                else if (verticalInput != 0)
                {
                    animator.SetFloat("Speed", Mathf.Abs(verticalInput));
                    newPosition = new Vector2(body.position.x, body.position.y + 2 * Mathf.Sign(verticalInput));

                    if (timerTurn.timer == 0 && !IsObstacleInDirection(newPosition))
                    {
                        audioController.PlaySFX(audioController.dash);
                    }
                    else if (IsObstacleInDirection(newPosition) && timerTurn.timer > 0)
                    {
                        audioController.PlaySFX(audioController.hitWall);
                    }
                    else if (timerTurn.timer > 0)
                    {
                        audioController.PlaySFX(audioController.dash);
                    }
                }

                if (!IsObstacleInDirection(newPosition))
                {
                    targetPosition = newPosition;
                    if (timerTurn.timer != 0)
                    {
                        timerTurn.timer++;
                        StartCoroutine(TimerAdd());
                    }
                    isMoving = true;
                }
                else if (timerTurn.timer != 0)
                {
                    timerTurn.timer++;
                    StartCoroutine(TimerAdd());
                }

                if (horizontalInput > 0.01f)
                    transform.localScale = new Vector3(2, 2, 1);
                else if (horizontalInput < -0.01f)
                    transform.localScale = new Vector3(-2, 2, 1);
            }
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            body.position = Vector2.MoveTowards(body.position, targetPosition, moveSpeed * Time.fixedDeltaTime);

            if (Vector2.Distance(body.position, targetPosition) < 0.01f)
            {
                body.position = targetPosition;
                isMoving = false;
            }
        }
    }

    private bool IsObstacleInDirection(Vector2 targetPos)
    {
        Collider2D hit = Physics2D.OverlapCircle(targetPos, 0.1f, obstacleLayer);
        return hit != null;
    }

    private IEnumerator TimerAdd()
    {
        yield return new WaitForSeconds(0.4f);
        isMoving = false;
    }

    private IEnumerator SpamDetect()
    {
        yield return new WaitForSeconds(0.8f);
        isSpam = false;
    }
}
