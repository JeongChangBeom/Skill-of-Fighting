using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource BGM;

    public AudioSource ButtonMouseOn_Audio;
    public AudioClip ButtonMouseOn;
    public static SoundManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<SoundManager>();
            }
            return m_instance;
        }
    }

    private static SoundManager m_instance;

    private void Awake()
    {
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }


    public void ButtonMouseOn_Sound()
    {
        ButtonMouseOn_Audio.PlayOneShot(ButtonMouseOn);
    }
}
