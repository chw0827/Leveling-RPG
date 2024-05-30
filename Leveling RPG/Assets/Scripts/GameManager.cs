using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public PlayerController pc;
    public GameObject uI;
    public GameObject goToTitleUI;
    public Button goTitleBtn;

    public string nowSceneName;
    public string beforeSceneName;

    public TMP_Text moneyDisplay;
    private int money;
    public int Money { get { return money; } set { MoneyUpdate(value); } }

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
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");
        pc = player.GetComponent<PlayerController>();
        moneyDisplay.text = $"{PlayerPrefs.GetInt("Money")}";
        SceneManager.sceneLoaded += BattleSceneReady;
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
            GoToTitleUI();
    }

    void MoneyUpdate(int price)
    {
        money = price; 
        moneyDisplay.text = $"{money}";
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.Save();
    }

    void NowSceneReport(Scene scene, LoadSceneMode mode)
    {
        beforeSceneName = nowSceneName;
        nowSceneName = scene.name;
    }

    void BattleSceneReady(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Battle" || scene.name == "Main")
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

    void GoToTitleUI()
    {
        goToTitleUI.SetActive(true);
        goTitleBtn.enabled = true;
        pc.playerstate = PlayerState.Wait;
    }

    public void GoTitleBtn()
    {
        StartCoroutine(GoTitle());
    }

    IEnumerator GoTitle()
    {
        pc.PlayerStatSave();
        SceneChanger.instance.SceneChangeStart();
        goTitleBtn.enabled = false;
        goToTitleUI.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Main");
        pc.playerstate = PlayerState.Idle;
    }
    
    public void CancleBtn()
    {
        goToTitleUI.SetActive(false);
        pc.playerstate = PlayerState.Idle;
    }

    public void LastPlayerPositionSave()
    {
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", player.transform.position.z);
        PlayerPrefs.Save();
    }

    public Vector3 LastPlayerPositionLoad()
    {
        Vector3 lastPlayerPosotion;

        lastPlayerPosotion.x = PlayerPrefs.GetFloat("PlayerX");
        lastPlayerPosotion.y = PlayerPrefs.GetFloat("PlayerY");
        lastPlayerPosotion.z = PlayerPrefs.GetFloat("PlayerZ");

        return lastPlayerPosotion;
    }
}
