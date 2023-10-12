using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private PlayerController playerController;
    private BossHP bossHP;

    private Slider hpBar;

    private Image backStep;
    private Text backStep_text;

    private Image parry;
    private Text parry_text;

    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }

    private static GameManager m_instance;

    private void Awake()
    {
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        bossHP = GameObject.FindWithTag("boss").GetComponent<BossHP>();
        hpBar = GameObject.Find("Canvas").transform.Find("BossHp").GetComponent<Slider>();
        backStep = GameObject.Find("Canvas").transform.Find("BackStep_Image").GetComponent<Image>();
        parry = GameObject.Find("Canvas").transform.Find("Parry_Image").GetComponent<Image>();
        backStep_text = GameObject.Find("Canvas").transform.Find("BackStep_Image/BackStep_CoolDown").GetComponent<Text>();
        parry_text = GameObject.Find("Canvas").transform.Find("Parry_Image/Parry_CoolDown").GetComponent<Text>();
    }

    void Update()
    {
        BackStepCoolDown();
        ParryCoolDown();
        BossHpBar();
    }

    private void BackStepCoolDown()
    {
        if (playerController.backstepCooldown >= 3.0f)
        {
            backStep.color = Color.blue;
            backStep_text.GetComponent<Text>().text = "";
        }
        else
        {
            backStep.color = Color.black;
            backStep_text.GetComponent<Text>().text = ((int)playerController.backstepCooldown) + 1 + "";
        }
    }

    private void ParryCoolDown()
    {
        if (playerController.parryCooldown >= 3.0f)
        {
            parry.color = Color.green;
            parry_text.GetComponent<Text>().text = "";
        }
        else
        {
            parry.color = Color.black;
            parry_text.GetComponent<Text>().text = ((int)playerController.parryCooldown) + 1 + "";
        }
    }

    private void BossHpBar()
    {
        hpBar.value = bossHP.bossHP;

        if (hpBar.value <= 0)
        {
            GameObject.Find("Canvas").transform.Find("BossHp/Fill Area").gameObject.SetActive(false);
        }
        else
        {
            GameObject.Find("Canvas").transform.Find("BossHp/Fill Area").gameObject.SetActive(true); ;
        }
    }
}
