using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour

{
    public GameObject FadePannel;
    void Awake()
    {
        StartCoroutine(FadeInStart());
    }

    public IEnumerator FadeInStart()
    {
        for (float f = 1f; f > 0; f -= 0.009f)
        {
            Color c = FadePannel.GetComponent<Image>().color;
            c.a = f;
            FadePannel.GetComponent<Image>().color = c;
            yield return null;
        }
        yield return new WaitForSeconds(0.33f);
        
        FadePannel.SetActive(false);
    }

    public IEnumerator FadeOutStart()
    {
        for (float f = 0f; f < 1; f += 0.027f)
        {
            Color c = FadePannel.GetComponent<Image>().color;
            c.a = f;
            FadePannel.GetComponent<Image>().color = c;
            yield return null;
        }
        yield return new WaitForSeconds(0.11f);
    }
}