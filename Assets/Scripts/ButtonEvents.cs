using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvents : MonoBehaviour {

    [SerializeField]
    private Animator settingPanel;
    [SerializeField]
    private Animator startButton;
    [SerializeField]
    private Animator settingButton;
    [SerializeField]
    private Animator bigAssText;
    [SerializeField]
    private Animator creditsPanel;   

    public void OpenSettingsPanel()
    {
        settingPanel.SetBool("isHidden", true);
        startButton.SetBool("isHidden", true);
        settingButton.SetBool("isHidden", true);
        bigAssText.SetBool("isHidden", true);
    }

    public void CloseSettingsPanel()
    {
        settingPanel.SetBool("isHidden", false);        
        startButton.SetBool("isHidden", false);
        settingButton.SetBool("isHidden", false);
        bigAssText.SetBool("isHidden", false);
    }

    public void OpenCreditsPanel()
    {
        creditsPanel.SetBool("isHidden", true);
        startButton.SetBool("isHidden", true);
        settingButton.SetBool("isHidden", true);
        bigAssText.SetBool("isHidden", true);
    }

    public void CloseCreditsPanel()
    {
        creditsPanel.SetBool("isHidden", false);
        startButton.SetBool("isHidden", false);
        settingButton.SetBool("isHidden", false);
        bigAssText.SetBool("isHidden", false);
    }
}
