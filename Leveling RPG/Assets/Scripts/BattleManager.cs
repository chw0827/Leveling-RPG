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
    Stat bearStat;

    public Transform playerStand;
    public Transform enemyStand;

    public Slider PlayerHpBar;

    public TMP_Text battleLog;
    public TMP_Text playerLv;
    public TMP_Text playerHpCount;
    public TMP_Text playerName;
    public TMP_Text playerAttackP;

    void Awake()
    {
        battleState = BattleState.Start;
        StartCoroutine(BattleStart());
    }

    IEnumerator BattleStart()
    {
        UnitSetting();
        UIUpdate();

        battleLog.text = $"{bearStat.unitName}이(가) 나타났다!";

        yield return new WaitForSeconds(2f);
        WhosTurnNow();
    }

    void UnitSetting()
    {
        player = GameManager.instance.player;
        pc = GameManager.instance.pc;
        pc.playerstate = PlayerState.BattleIdle;
        pc.anim.SetBool("battle", true);
        player.transform.position = playerStand.position;
        player.transform.rotation = Quaternion.LookRotation(Vector3.back);

        bearStat = bear.GetComponent<Stat>();
        Instantiate(bear).transform.position = enemyStand.position;
        bear.transform.rotation = Quaternion.LookRotation(Vector3.forward);
    }

    void UIUpdate()
    {
        PlayerHpBar.value = pc.hp / pc.maxHp;
        playerLv.text = $"Lv {pc.level}";
        playerHpCount.text = $"{pc.hp}";
        playerName.text = $"이름 : {pc.playerName}";
        playerAttackP.text = $"공격력 : {pc.attackP}";
    }

    public void PlayerAttack()
    {
        if (battleState == BattleState.Enemyturn)
            return;
        
    }

    public void BattleRun()
    {
        pc.anim.SetBool("battle", false);
        SceneManager.LoadScene(GameManager.instance.beforeSceneName);
    }

    void WhosTurnNow()
    {
        if (battleState != BattleState.Playerturn)
        {
            battleState = BattleState.Playerturn;
            battleLog.text = $"{pc.playerName}의 차례. 무엇을 할까?";
            return;
        }
        battleState = BattleState.Enemyturn;
        battleLog.text = $"{bearStat.unitName}의 차례.";
    }
}
