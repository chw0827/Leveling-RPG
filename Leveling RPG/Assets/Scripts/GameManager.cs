using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public GameObject uI;

    public string nowSceneName;
    public string beforeSceneName;

    public TMP_Text moneyDisplay;
    public int moneyHave;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += NowSceneReport; 
    }

    private void Start()
    {
        FieldUIDisable();
        MoneyUpdate();
    }

    public void MoneyUpdate()
    {
        moneyDisplay.text = $"{moneyHave}";
        PlayerPrefs.SetInt("Money", moneyHave);
        PlayerPrefs.Save();
    }

    public void NowSceneReport(Scene scene, LoadSceneMode mode)
    {
        beforeSceneName = nowSceneName;
        nowSceneName = scene.name;
    }

    void FieldUIDisable()
    {
        if (nowSceneName == "Battle")
            uI.SetActive(false);
        else
            uI.SetActive(true);
    }
}
