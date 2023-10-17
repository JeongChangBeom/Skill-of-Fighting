using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerController playerController;
    private BossHP bossHP;

    private Slider hpBar;

    private Image backStep;
    private Text backStep_text;

    private Image parry;
    private Text parry_text;

    private PlayerHp playerHp;
    private BossHP bossHp;

    private GameObject PauseText;
    public bool isPause = false;
   

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
        playerHp = GameObject.FindWithTag("Player").GetComponent<PlayerHp>();
        bossHp = GameObject.FindWithTag("boss").GetComponent<BossHP>();
        PauseText = GameObject.Find("Canvas").transform.Find("PauseText").gameObject;

        Time.timeScale = 1;
        playerHp.isDead = false;
    }

    void Update()
    {
        BossHpBar();

        if (!playerHp.isDead && bossHp.bossHP > 0)
        {
            Pause();
        }

        if (!playerHp.isDead && bossHp.bossHP > 0 && !isPause)
        {
            BackStepCoolDown();
            ParryCoolDown();
        }
        else
        {
            GameOver();
        }
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPause)
        {
            isPause = true;
            Time.timeScale = 0;
            PauseText.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPause)
        {
            isPause = false;
            Time.timeScale = 1;
            PauseText.SetActive(false);
        }
    }

    private void GameOver()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene("MainMenu");
        }
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
