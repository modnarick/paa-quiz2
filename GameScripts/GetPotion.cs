using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPotion : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer chestOpened;
    private PotionManager potionStorage;
    private int num;

    private AudioController audioController;

    void Start()
    {

        chestOpened.gameObject.SetActive(false);
        audioController = GameObject.Find("AudioManager").GetComponent<AudioController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerTag"))
        {
            audioController.PlaySFX(audioController.openChest);
            potionStorage = GameObject.Find("PotionParent").GetComponent<PotionManager>();
            num = UnityEngine.Random.Range(0, 100);
            if (num % 2 == 0)
            {
                if (potionStorage.potionHoly < 3)
                {
                    potionStorage.potionHoly += 1;
                }
                else potionStorage.potionConfuse += 1;
            }
            else
            {
                if (potionStorage.potionConfuse < 3)
            {
                potionStorage.potionConfuse += 1;
            }
            else potionStorage.potionHoly += 1;
        }


        chestOpened.gameObject.SetActive(true);

            Destroy(gameObject);
        }
    }
}