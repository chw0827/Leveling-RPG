using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public PlayerController pc;
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
        pc = player.GetComponent<PlayerController>();
        moneyDisplay.text = $"{moneyHave}";
        SceneManager.sceneLoaded += BattleSceneReady;
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

    void BattleSceneReady(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Battle")
        {
            uI.SetActive(false);
            pc.BattleModeSet(scene.name);
        }
        else
        {
            uI.SetActive(true);
            pc.BattleModeSet(scene.name);
        }
    }
}
