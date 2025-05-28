using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimplePF2D;

public class NewEnemyMovementBFS : MonoBehaviour
{
    private BFSAlgorithm bfsAlgorithm;
    private SimplePF2D.Path path;
    private Rigidbody2D rb;
    private Vector3 nextPoint;
    public bool isStationary = true;

    [SerializeField] Transform player;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private TimerTurn timerTurn;
    [SerializeField] private GameObject debugMarkerPrefab;

    public bool isDisabled = true;
    public float secondsMovement = 0.4f;

    private Coroutine movementCoroutine;

    void Start()
    {
        Initialize();
    }

    void OnEnable()
    {
        Initialize();
    }

    void Initialize()
    {
        timerTurn = GameObject.Find("PlayerEnemyTurn").GetComponent<TimerTurn>();
        SimplePathFinding2D pathfinding = GameObject.Find("GridPathfinding").GetComponent<SimplePathFinding2D>();
        bfsAlgorithm = new BFSAlgorithm(pathfinding, debugMarkerPrefab);
        rb = GetComponent<Rigidbody2D>();
        nextPoint = Vector3.zero;
        isStationary = true;
        isDisabled = false;
    }

    void Update()
    {
        if (!isDisabled)
        {
            if (timerTurn.timer % 2 == 1 && timerTurn.timer != 0)
            {
                NavNode startNode = bfsAlgorithm.GetPathFinding().GetNode(bfsAlgorithm.GetPathFinding().WorldToNav(transform.position));
                NavNode endNode = bfsAlgorithm.GetPathFinding().GetNode(bfsAlgorithm.GetPathFinding().WorldToNav(player.position));
                if (bfsAlgorithm.CreatePath(startNode, endNode))
                {
                    path = new SimplePF2D.Path(bfsAlgorithm.GetPathFinding());
                    path.GetPathPointList().AddRange(bfsAlgorithm.GetPathPoints());
         
                }
            }

            if (path != null && path.IsGenerated())
            {
                if (isStationary && timerTurn.timer % 2 == 0)
                {
                    if (path.GetNextPoint(ref nextPoint))
                    {
                        Vector3 direction = (nextPoint - transform.position).normalized;
                        rb.velocity = direction * speed;
                        if (movementCoroutine == null)
                        {
                            movementCoroutine = StartCoroutine(MoveToNextPoint());
                        }
                        isStationary = false;
                    }
                    else
                    {
                        rb.velocity = Vector3.zero;
                        isStationary = true;
                    }
                }
            }
            else
            {
                rb.velocity = Vector3.zero;
                isStationary = true;
            }
        }
    }

    private IEnumerator MoveToNextPoint()
    {
        Vector3 startPoint = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < secondsMovement)
        {
            rb.MovePosition(Vector3.Lerp(startPoint, nextPoint, elapsedTime / secondsMovement));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.MovePosition(nextPoint);
        rb.velocity = Vector3.zero;
        isStationary = true;
        timerTurn.timer++;
        movementCoroutine = null;
        bfsAlgorithm.RemoveSingleDebugMarker(nextPoint);
    }

    void OnDisable()
    {
        bfsAlgorithm.ClearDebugMarkers();
        isStationary = false;
        isDisabled = true;

        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
            movementCoroutine = null;
        }
    }

    public void RemotePath()
    {
        path.CreatePath(transform.position, player.position);
        if (path.GetNextPoint(ref nextPoint))
        {
            if (movementCoroutine == null)
            {
                movementCoroutine = StartCoroutine(MoveToNextPoint());
            }
        }
    }
}
