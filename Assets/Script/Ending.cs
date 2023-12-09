using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject BackGroundPanel;
    public Text Senario_Text;
    public Text End_Text;
    public Text Credit_Text;

    private bool senarioStart = false;
    private bool endStart = false;
    private bool creditStart = false;
    private bool gomenuOn = false;

    int max = 0;

    string senario = "가디언은 이모탈을 무찔렀고\n\n\n현대 무기들에게 걸려있던 이모탈의 세뇌는 전부 풀리게 되었다." +
                  "\n\n\n그리하여 세상은 현대 무기 전통 무기 할 것 없이 평화로운 세상이 되었다.";

    void Start()
    {
        GameObject.Find("Canvas").GetComponent<Animator>().SetBool("FadeIn", true);
        Senario_Text.text = senario;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            senarioStart = true;
        }

        if (senarioStart)
        {
            BackGroundPanel.SetActive(true);

            if (Input.anyKey)
            {
                Senario_Text.transform.localPosition = Vector3.MoveTowards(Senario_Text.transform.localPosition, new Vector3(Senario_Text.transform.localPosition.x, 1060f, Senario_Text.transform.localPosition.z), 600 * Time.deltaTime);
            }
            else
            {
                Senario_Text.transform.localPosition = Vector3.MoveTowards(Senario_Text.transform.localPosition, new Vector3(Senario_Text.transform.localPosition.x, 1060f, Senario_Text.transform.localPosition.z), 100 * Time.deltaTime);
            }
            print(Senario_Text.transform.localPosition.y);
        }

        if (Senario_Text.transform.localPosition.y == 1060f)
        {
            endStart = true;
        }

        if (endStart)
        {
            if (Input.anyKey)
            {
                End_Text.transform.localPosition = Vector3.MoveTowards(End_Text.transform.localPosition, new Vector3(End_Text.transform.localPosition.x, 660f, End_Text.transform.localPosition.z), 600 * Time.deltaTime);
            }
            else
            {
                End_Text.transform.localPosition = Vector3.MoveTowards(End_Text.transform.localPosition, new Vector3(End_Text.transform.localPosition.x, 660f, End_Text.transform.localPosition.z), 100 * Time.deltaTime);
            }
        }

        if (End_Text.transform.localPosition.y == 660f)
        {
            creditStart = true;
        }

        if (creditStart)
        {
            Credit_Text.gameObject.SetActive(true);
            max += 1;

            if (max < 250)
            {
                Credit_Text.gameObject.transform.localScale += new Vector3(0.002f, 0.002f, 0.002f);
            }
        }

        if (Credit_Text.gameObject.transform.localScale.x > 0.4f)
        {
            Invoke("GomenuOn", 3.0f);
        }

        if (gomenuOn)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    private void GomenuOn()
    {
        gomenuOn = true;
    }
}
