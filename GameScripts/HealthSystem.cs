using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{

    public GameObject[] hearts;
    public int life;
    private AudioController audioController;

    // Start is called before the first frame update
    void Start()
    {
        life = 3;
        audioController = GameObject.Find("AudioManager").GetComponent<AudioController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (life < 1)
        {
            Destroy(hearts[0].gameObject);
            SceneManager.LoadSceneAsync(3);

        }
        else if (life < 2)
        {
            Destroy(hearts[1].gameObject);
        }
        else if (life < 3)
        {
            Destroy(hearts[2].gameObject);
        }
    }

    public void TakeDamage(int damage)
    {

        life -= damage;
        audioController.PlaySFX(audioController.takeDamage);
    }
}
