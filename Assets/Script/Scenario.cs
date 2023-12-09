using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Scenario : MonoBehaviour
{
    public VideoPlayer video;

    void Start()
    {
        video.loopPointReached += CheckOver;
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        Invoke("Next",1.5f);
    }

    void Next()
    {
        SceneManager.LoadScene("Stage01_Before");
    }
}
