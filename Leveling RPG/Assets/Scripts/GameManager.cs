using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;

    public string nowSceneName;
    public string beforeSceneName;

    public TMP_Text moneyDisplay;
    public int moneyHave;

    public Animator anim;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        MoneyUpdate();
    }

    public void SceneChangeEffect()
    {
        anim.SetTrigger("sceneMove");
    }

    public void MoneyUpdate()
    {
        moneyDisplay.text = $"{moneyHave}";
        PlayerPrefs.SetInt("Money", moneyHave);
        PlayerPrefs.Save();
    }

    public void NowSceneReport()
    {
        nowSceneName = SceneManager.GetActiveScene().name;
    }

    public void BeforeSceneReport()
    {
        beforeSceneName = SceneManager.GetActiveScene().name;
    }
}
