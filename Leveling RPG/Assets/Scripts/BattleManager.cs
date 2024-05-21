using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum BattleState
{
    Start, Playerturn, Enemyturn, Win, Defeat
}

public class BattleManager : MonoBehaviour
{
    public BattleState battleState;
    bool enemyAlive;
    bool playerAlive;

    GameObject player;
    PlayerController pc;

    public GameObject bear;


    public Transform playerStand;
    public Transform enemyStand;

    public TMP_Text battleLog;

    void Awake()
    {
        battleState = BattleState.Start;
        StartCoroutine(BattleStart());
    }

    IEnumerator BattleStart()
    {
        UnitSetting();
        battleLog.text = "ÀÌ ³ªÅ¸³µ´Ù!";

        yield return new WaitForSeconds(2f);

    }

    void UnitSetting()
    {
        player = GameManager.instance.player;
        pc = GameManager.instance.pc;
        pc.playerstate = PlayerState.BattleIdle;
        pc.anim.SetBool("battle", true);
        player.transform.position = playerStand.position;
        player.transform.rotation = Quaternion.LookRotation(Vector3.back);

        Instantiate(bear).transform.position = enemyStand.position;
        bear.transform.rotation = Quaternion.LookRotation(Vector3.forward);
    }

    void UISetting()
    {

    }

    public void PlayerAttack()
    {
        if (battleState == BattleState.Enemyturn)
            return;


    }

    public void BattleRun()
    {
        GameManager.instance.pc.anim.SetBool("battle", false);
        SceneManager.LoadScene(GameManager.instance.beforeSceneName);
    }
}
