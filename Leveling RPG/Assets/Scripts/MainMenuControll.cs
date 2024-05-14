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

    public Button settingsButton;

    public TMP_Text settingsSaveTxt;

    public void GameStart()
    {
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

    IEnumerator InputSettings()
    {
        settingsButton.interactable = false;
        titleAnim.SetBool("titleUp", true);
        yield return new WaitForSeconds(1f);
        settingsAnim.SetBool("settingsDown", true);

        yield return new WaitForSeconds(1f);
        settingsButton.interactable = true;
    }

    IEnumerator CloseSettings()
    {
        settingsButton.interactable = false;
        settingsAnim.SetBool("settingsDown", false);
        yield return new WaitForSeconds(1f);
        titleAnim.SetBool("titleUp", false);

        yield return new WaitForSeconds(1f);
        settingsButton.interactable = true;
    }

    IEnumerator SettingsSaveText()
    {
        settingsSaveTxt.enabled = true;
        yield return new WaitForSeconds(2f);
        settingsSaveTxt.enabled = false;
    }
}
