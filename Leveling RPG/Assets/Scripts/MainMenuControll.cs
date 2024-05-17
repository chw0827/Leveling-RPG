using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuControll : MonoBehaviour
{
    public Animator titleAnim;
    public Animator settingsAnim;

    public Button settingsBtn;
    public Slider volumeSld;
    public Image volumeCancleLine;

    public TMP_Text settingsWarningTxt;
    public TMP_Dropdown dropDown;

    public void GameStart()
    {
        if (titleAnim.GetBool("titleUp"))
        {
            StartCoroutine(SettingsWarningText());
            return;
        }

        StartCoroutine(StartGame());
    }

    public void InputSettingsBtn()
    {
        if (!titleAnim.GetBool("titleUp"))
        {
            StartCoroutine(InputSettings());
        }
        else if (titleAnim.GetBool("titleUp"))
        {
            StartCoroutine(CloseSettings());
        }
    }

    public void CloseSettingsBtn()
    {
        StartCoroutine(CloseSettings());
    }

    public void VolumeOnOff()
    {
        if (volumeCancleLine.enabled)
        {
            volumeCancleLine.enabled = false;
            volumeSld.value = 0.1f;
        }
        else if (!volumeCancleLine.enabled)
        {
            volumeCancleLine.enabled = true;
            volumeSld.value = 0f;
        }
    }

    public void VolumeControll()
    {
        if (volumeSld.value == 0f)
            volumeCancleLine.enabled = true;
        else
            volumeCancleLine.enabled = false;

        PlayerPrefs.SetFloat("Volume", volumeSld.value);
    }

    public void DropDownResolution()
    {
        int value = dropDown.value;
        if (value == 0)
            Screen.fullScreen = true;
        else if (value == 1)
            Screen.fullScreen = false;
    }

    IEnumerator StartGame()
    {
        SceneChanger.instance.SceneChangeStart();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Village");
    }
    IEnumerator InputSettings()
    {
        settingsBtn.interactable = false;
        titleAnim.SetBool("titleUp", true);
        yield return new WaitForSeconds(1f);
        settingsAnim.SetBool("settingsDown", true);

        yield return new WaitForSeconds(1f);
        settingsBtn.interactable = true;
    }

    IEnumerator CloseSettings()
    {
        settingsBtn.interactable = false;
        settingsAnim.SetBool("settingsDown", false);
        yield return new WaitForSeconds(1f);
        titleAnim.SetBool("titleUp", false);

        yield return new WaitForSeconds(1f);
        settingsBtn.interactable = true;
    }

    IEnumerator SettingsWarningText()
    {
        settingsWarningTxt.enabled = true;
        yield return new WaitForSeconds(2f);
        settingsWarningTxt.enabled = false;
    }
}
