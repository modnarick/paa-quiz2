using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PotionManager : MonoBehaviour
{
    [SerializeField] public int potionConfuse;
    [SerializeField] public int potionHoly;

    public GameObject[] uiConfuse;
    public GameObject[] uiHoly;

    private SpriteRenderer chestOpened;
    private HealthSystem health;
    private Transform ghost;

    //[SerializeField] private NewEnemyMovement aStarMovement;
    [SerializeField] private NewEnemyMovementBFS bfsMovement;
    [SerializeField] private NewEnemyMovementDFS dfsMovement;

    private bool potionActive = false;
    private AudioController audioController;

    int potionTime;

    private TimerTurn timerTurn;

    private bool canUseSkillConfuse;
    private bool canUseSkillHoly;

    void Awake()
    {
        canUseSkillConfuse = true;
        canUseSkillHoly = true;

        for (int i = 0; i < 3; i++)
        {
            uiHoly[i].SetActive(false);
            uiConfuse[i].SetActive(false);
        }

        //aStarMovement = GameObject.Find("Ghost").GetComponent<NewEnemyMovement>();
        bfsMovement = GameObject.Find("Ghost").GetComponent<NewEnemyMovementBFS>();
        dfsMovement = GameObject.Find("Ghost").GetComponent<NewEnemyMovementDFS>();
        health = GameObject.Find("HealthManager").GetComponent<HealthSystem>();
        ghost = GameObject.Find("Ghost").transform;
        timerTurn = GameObject.Find("PlayerEnemyTurn").GetComponent<TimerTurn>();
        audioController = GameObject.Find("AudioManager").GetComponent<AudioController>();
    }

    void Update()
    {
        if (timerTurn.timer % 2 == 1 && health.life > 0 && potionHoly > 0 && Input.GetKey(KeyCode.K) && canUseSkillHoly)
        {
            //aStarMovement.enabled = false;
            //bfsMovement.enabled = true;
            //dfsMovement.enabled = false;
            StartCoroutine(CooldownSkillK());
            potionHoly -= 1;
            ghost.localPosition = new Vector3(-25.29f, -28.8f, 0f);
            potionTime = timerTurn.timer + 2;
            //dfsMovement.isDisabled = true;
            //bfsMovement.isDisabled = false;
            //aStarMovement.isDisabled = true;
            potionActive = true;
            audioController.PlaySFX(audioController.usePotion);
        }

        for (int i = 0; i < uiHoly.Length; i++)
        {
            if (i < potionHoly)
            {
                uiHoly[i].SetActive(true);
            }
            else uiHoly[i].SetActive(false);
        }

        if (timerTurn.timer % 2 == 1 && health.life > 0 && potionConfuse > 0 && Input.GetKey(KeyCode.J) && canUseSkillConfuse)
        {
            //aStarMovement.enabled = false;
            bfsMovement.enabled = false;
            dfsMovement.enabled = true;
            StartCoroutine(CooldownSkillJ());
            potionConfuse -= 1;
            potionTime = timerTurn.timer + 40;
            dfsMovement.isDisabled = false;
            bfsMovement.isDisabled = true;
            //aStarMovement.isDisabled = true;
            potionActive = true;
            audioController.PlaySFX(audioController.usePotion);
        }

        for (int i = 0; i < uiConfuse.Length; i++)
        {
            if (i < potionConfuse)
            {
                uiConfuse[i].SetActive(true);
            }
            else uiConfuse[i].SetActive(false);
        }

        if (potionActive && potionTime == timerTurn.timer)
        {
            //aStarMovement.enabled = true;
            bfsMovement.enabled = true;
            dfsMovement.enabled = false;
            //aStarMovement.isDisabled = false;
            dfsMovement.isDisabled = true;
            bfsMovement.isDisabled = false;
            potionTime = 0;
            potionActive = false;
            //aStarMovement.RemotePath();
            audioController.PlaySFX(audioController.potionEnd);
        }
    }

    private IEnumerator CooldownSkillJ()
    {
        canUseSkillConfuse = false;
        yield return new WaitForSeconds(2.0f);
        canUseSkillConfuse = true;
    }

    private IEnumerator CooldownSkillK()
    {
        canUseSkillHoly = false;
        yield return new WaitForSeconds(2.0f);
        canUseSkillHoly = true;
    }
}