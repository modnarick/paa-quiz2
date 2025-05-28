using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidFountain : MonoBehaviour
{

    private HealthSystem healthSystem;

    // Start is called before the first frame update
    void Start()
    {
        healthSystem = GameObject.Find("HealthManager").GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerTag"))
        {
            healthSystem.TakeDamage(1);
        }
        
    }
}
