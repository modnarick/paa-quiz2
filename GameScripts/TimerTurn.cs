using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTurn : MonoBehaviour
{
    [SerializeField]
    private Transform ObstaclesOne;

    [SerializeField]
    private Transform ObstaclesTwo;

    [SerializeField]
    private Transform ObstaclesThree;

    public int timer;
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer != 0) {

            if (timer % 9 == 0 || timer % 9 == 1 || timer % 9 == 2)
            { ObstaclesOne.localScale = new Vector3(1, 1, 1); }
            else { ObstaclesOne.localScale = new Vector3(0, 0, 1); }

            if (timer % 9 == 3 || timer % 9 == 4 || timer % 9 == 5)
            { ObstaclesTwo.localScale = new Vector3(1, 1, 1); }
            else { ObstaclesTwo.localScale = new Vector3(0, 0, 1); }

            if (timer % 9 == 6 || timer % 9 == 7 || timer % 9 == 8)
            { ObstaclesThree.localScale = new Vector3(1, 1, 1); }
            else { ObstaclesThree.localScale = new Vector3(0, 0, 1); }
        }

    }

}
