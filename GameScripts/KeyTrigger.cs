using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTrigger : MonoBehaviour
{
    private KeysController keyControl;

    // Start is called before the first frame update
    void Start()
    {
        keyControl = GameObject.Find("KeysManager").GetComponent<KeysController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerTag"))
        {
            keyControl.keyAcquired++;
            Destroy(gameObject);
        }
    }
}
