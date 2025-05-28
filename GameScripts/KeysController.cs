using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysController : MonoBehaviour
{
    public int keyAcquired;
    public GameObject[] groupKeys;

    private int num;

    // Start is called before the first frame update
    void Start()
    {
        groupKeys[0].gameObject.SetActive(false);
        groupKeys[1].gameObject.SetActive(false);

        num = UnityEngine.Random.Range(0, 100);
        if (num % 2 == 0)
        {
            groupKeys[0].gameObject.SetActive(true);
        }
        else
        {
            groupKeys[1].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
