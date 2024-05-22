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
    EnemyState bearStat;

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

    private void Update()
    {
        UIUpdate();
    }

    public IEnumerator BattleStart()
    {
        UnitSetting();
        UIUpdate();

        battleLog.text = $"{bearStat.unitName}이(가) 나타났다!";

        yield return new WaitForSeconds(2.5f);
        WhosTurnNow();
    }

    void UnitSetting()
    {
        player = GameManager.instance.player;
        pc = GameManager.instance.pc;
        player.transform.position = playerStand.position;
        player.transform.rotation = Quaternion.LookRotation(Vector3.back);

        bearStat = bear.GetComponent<EnemyState>();
        Instantiate(bear).transform.position = enemyStand.position;
        bear.transform.rotation = Quaternion.LookRotation(Vector3.forward);
    }

    void UIUpdate()
    {
        PlayerHpBar.value = pc.hp / pc.maxHp;
        playerLv.text = $"Lv {pc.level}";
        playerHpCount.text = $"HP : {pc.hp}";
        playerName.text = $"이름 : {pc.characterName}";
        playerAttackP.text = $"공격력 : {pc.attackP}";
    }

    public void PlayerAttack()
    {
        if (battleState == BattleState.Enemyturn)
            return;
        
    }

    public void BattleRun()
    {
        StartCoroutine(PantsRun());
    }

    IEnumerator PantsRun()
    {
        battleLog.text = "무사히 도망쳤다.";

        yield return new WaitForSeconds(1f);
        SceneChanger.instance.SceneChangeStart();

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(GameManager.instance.beforeSceneName);
    }

    void WhosTurnNow()
    {
        if (battleState != BattleState.Playerturn)
        {
            battleState = BattleState.Playerturn;
            battleLog.text = $"{pc.characterName}의 차례.\n무엇을 할까?";
            return;
        }
        battleState = BattleState.Enemyturn;
        battleLog.text = $"{bearStat.unitName}의 차례.";
    }
}
