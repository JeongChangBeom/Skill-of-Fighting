using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerController playerController;

    private Image backStep;
    private Text backStep_text;

    private Image parry;
    private Text parry_text;

    private PlayerHp playerHp;
    private BossStatus bossStatus;

    private GameObject Option;
    public bool isPause = false;

    public bool GameStart;

    public static bool Dotutorial = false;

    public bool restartPress = false;

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
        GameStart = false;

        Time.timeScale = 1;

        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        backStep = GameObject.Find("Canvas").transform.Find("BackStep_Image").GetComponent<Image>();
        parry = GameObject.Find("Canvas").transform.Find("Parry_Image").GetComponent<Image>();
        backStep_text = GameObject.Find("Canvas").transform.Find("BackStep_Image/BackStep_CoolDown").GetComponent<Text>();
        parry_text = GameObject.Find("Canvas").transform.Find("Parry_Image/Parry_CoolDown").GetComponent<Text>();
        playerHp = GameObject.FindWithTag("Player").GetComponent<PlayerHp>();
        bossStatus = GameObject.FindWithTag("boss").GetComponent<BossStatus>();
        Option = GameObject.Find("Canvas").transform.Find("Option").gameObject;

        playerHp.isDead = false;

        Invoke("StartGame", 2f);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Stage03")
        {
            if (GameObject.FindWithTag("boss").GetComponent<ImmortalStatus>().mainHP > 0)
            {
                Pause();
            }
        }
        else
        {
            if (!playerHp.isDead && bossStatus.bossHP > 0)
            {
                Pause();
            }
        }

        if (SceneManager.GetActiveScene().name == "Stage03")
        {
            if (!playerHp.isDead && GameObject.FindWithTag("boss").GetComponent<ImmortalStatus>().mainHP > 0 && !isPause)
            {
                BackStepCoolDown();
                ParryCoolDown();
            }
            else if (playerHp.isDead && GameObject.FindWithTag("boss").GetComponent<ImmortalStatus>().mainHP > 0 && !isPause)
            {
                GameOver();
            }
        }
        else
        {
            if (!playerHp.isDead && bossStatus.bossHP > 0 && !isPause)
            {
                BackStepCoolDown();
                ParryCoolDown();
            }
            else if (playerHp.isDead && bossStatus.bossHP > 0 && !isPause)
            {
                GameOver();
            }
        }
    }

    private void StartGame()
    {
        GameStart = true;
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPause)
        {
            isPause = true;
            Time.timeScale = 0;
            Option.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPause)
        {
            isPause = false;
            Time.timeScale = 1;
            Option.SetActive(false);
        }
    }

    private void GameOver()
    {
        if (restartPress)
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
    }

    private void BackStepCoolDown()
    {
        if (playerController.backstepCooldown >= 3.0f)
        {
            backStep.color = Color.white;
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
            parry.color = Color.white;
            parry_text.GetComponent<Text>().text = "";
        }
        else
        {
            parry.color = Color.black;
            parry_text.GetComponent<Text>().text = ((int)playerController.parryCooldown) + 1 + "";
        }
    }
}
