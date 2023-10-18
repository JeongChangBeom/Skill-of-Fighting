using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionOption : MonoBehaviour
{
    private Dropdown resolutionDropdown;
    private List<Resolution> resolutions = new List<Resolution>();
    private int resolutionNum;
    void Start()
    {
        resolutionDropdown = GameObject.Find("Canvas").transform.Find("Option/Resolution").GetComponent<Dropdown>();

        InitUI();
    }

    private void InitUI()
    {
        for(int i=0;i<Screen.resolutions.Length;i++)
        {
            if(Mathf.Ceil(Screen.resolutions[i].refreshRate) == 60 && (Screen.resolutions[i].width * 9 == Screen.resolutions[i].height * 16) && Screen.resolutions[i].width >= 1280)
            {
                resolutions.Add(Screen.resolutions[i]);
            }
        }

        resolutionDropdown.options.Clear();

        int optionNum = 0;

        foreach (Resolution item in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();

            option.text = item.width + " x " + item.height + " " + Mathf.Ceil(item.refreshRate) + "hz";
            resolutionDropdown.options.Add(option);

            if (item.width == Screen.width && item.height == Screen.height)
            {
                resolutionDropdown.value = optionNum;
            }
            optionNum++;
        }
        resolutionDropdown.RefreshShownValue();
    }

    public void DropboxOptionChange(int x)
    {
        resolutionNum = x;
    }


    public void OkBtnClick()
    {
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, false);
    }


}
