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

    public TMP_Text settingsSaveTxt;
    public TMP_Text settingsWarningTxt;

    public void GameStart()
    {
        if (titleAnim.GetBool("titleUp"))
        {
            StartCoroutine(SettingsWarningText());
            return;
        }

        SceneManager.LoadScene("Village");
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

    public void SaveSettingsBtn()
    {
        if (settingsSaveTxt.enabled)
            return;

        StartCoroutine(SettingsSaveText());
    }

    public void VolumeOnOff()
    {
        if (volumeCancleLine.enabled)
        {
            volumeCancleLine.enabled = false;
            volumeSld.value = 1f;
        }
        else if (!volumeCancleLine.enabled)
        {
            volumeCancleLine.enabled = true;
            volumeSld.value = 0f;
        }
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

    IEnumerator SettingsSaveText()
    {
        settingsSaveTxt.enabled = true;
        yield return new WaitForSeconds(2f);
        settingsSaveTxt.enabled = false;
    }

    IEnumerator SettingsWarningText()
    {
        settingsWarningTxt.enabled = true;
        yield return new WaitForSeconds(2f);
        settingsWarningTxt.enabled = false;
    }
}
