using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    private Image im;

    public Sprite empty;
    public Sprite full;
    public Sprite one_three;
    public Sprite two_three;
    public Sprite one_two;

    private BossStatus bossStatus;
    private ImmortalStatus immortalStatus;

    void Start()
    {
        im = GetComponent<Image>();
        if (SceneManager.GetActiveScene().name == "Stage03")
        {
            immortalStatus = GameObject.FindWithTag("boss").GetComponent<ImmortalStatus>();
        }
        else
        {
            bossStatus = GameObject.FindWithTag("boss").GetComponent<BossStatus>();
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Stage03")
        {
            if(this.gameObject.name == "LeftArmHp")
            {
                switch (immortalStatus.leftarmHP)
                {
                    case 0:
                        this.gameObject.SetActive(false);
                        break;
                    case 1:
                        im.sprite = one_two;
                        break;
                    case 2:
                        im.sprite = full;
                        break;
                }
            }
            else if(this.gameObject.name == "RightArmHp")
            {
                switch (immortalStatus.rightarmHP)
                {
                    case 0:
                        this.gameObject.SetActive(false);
                        break;
                    case 1:
                        im.sprite = one_two;
                        break;
                    case 2:
                        im.sprite = full;
                        break;
                }
            }
            else if (this.gameObject.name == "BossHP")
            {
                switch (immortalStatus.mainHP)
                {
                    case 0:
                        im.sprite = empty;
                        break;
                    case 1:
                        im.sprite = one_three;
                        break;
                    case 2:
                        im.sprite = two_three;
                        break;
                    case 3:
                        im.sprite = full;
                        break;
                }
            }

            if(immortalStatus.leftarmHP == 0 && immortalStatus.rightarmHP == 0)
            {
                GameObject.Find("Canvas").transform.Find("BossHP").gameObject.SetActive(true);
            }
        }
        else
        {
            switch (bossStatus.bossHP)
            {
                case 0:
                    im.sprite = empty;
                    break;
                case 1:
                    im.sprite = one_three;
                    break;
                case 2:
                    im.sprite = two_three;
                    break;
                case 3:
                    im.sprite = full;
                    break;
            }
        }
    }
}
