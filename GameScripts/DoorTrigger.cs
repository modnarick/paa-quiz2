using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{

    private KeysController keyControl;

    void Start()
    {
        keyControl = GameObject.Find("KeysManager").GetComponent<KeysController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("PlayerTag") && keyControl.keyAcquired >= 3)
        {
            SceneManager.LoadSceneAsync(2);
        }
    }
}
