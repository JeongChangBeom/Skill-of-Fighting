using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    private void Start()
    {
        if (this.gameObject.name == "BGMSlider")
        {
            GameObject.Find("Canvas").transform.Find("Option/BGMSlider").GetComponent<Slider>().value = SoundManager.BGMvolume;
        }

        if (this.gameObject.name == "SFXSlider")
        {
            GameObject.Find("Canvas").transform.Find("Option/SFXSlider").GetComponent<Slider>().value = SoundManager.BGMvolume;
        }
    }
    public void SetLevel(float sliderVal)
    {
        mixer.SetFloat("Volume", Mathf.Log10(sliderVal) * 20);

        if (this.gameObject.name == "BGMSlider")
        {
            SoundManager.BGMvolume = sliderVal;
        }

        if (this.gameObject.name == "SFXSlider")
        {
            SoundManager.SFXvolume = sliderVal;
        }
    }

}
