using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    private TimerTurn timerTurn;

    [SerializeField]
    private Transform gateItself;

    [SerializeField]
    private Transform gateShadow;

    // Start is called before the first frame update
    void Start()
    {
        timerTurn = GameObject.Find("PlayerEnemyTurn").GetComponent<TimerTurn>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        timerTurn.timer++;
        gateItself.localScale = new Vector3(2, 2, 1);
        gateShadow.localScale = new Vector3(2, 2, 1);

        Destroy(gameObject);
    }
}
