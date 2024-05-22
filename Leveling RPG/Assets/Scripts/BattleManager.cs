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
    int damage;
    float hitTiming;

    GameObject player;
    PlayerController pC;

    public GameObject enemy;
    EnemyState eS;

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

        battleLog.text = $"{eS.unitName}이(가) 나타났다!";

        yield return new WaitForSeconds(2.5f);
        WhosTurnNow();
    }

    void UnitSetting()
    {
        player = GameManager.instance.player;
        pC = GameManager.instance.pc;

        player.transform.position = playerStand.position;
        player.transform.rotation = Quaternion.LookRotation(Vector3.back);

        eS = enemy.GetComponent<EnemyState>();
        Instantiate(enemy).transform.position = enemyStand.position;
        enemy.transform.rotation = Quaternion.LookRotation(Vector3.forward);
    }

    void UIUpdate()
    {
        PlayerHpBar.value = pC.hp / pC.maxHp;
        playerLv.text = $"Lv {pC.level}";
        playerHpCount.text = $"HP : {pC.hp}";
        playerName.text = $"이름 : {pC.characterName}";
        playerAttackP.text = $"공격력 : {pC.attackP}";
    }

    void EnemyTurnAttack()
    {
        if (battleState == BattleState.Playerturn)
            return;
        StartCoroutine(EnemyAttack());
    }

    IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(1f);

        eS.Attack(out damage, out hitTiming);
        battleLog.text = $"{eS.unitName}의 공격!\n";

        yield return new WaitForSeconds(hitTiming);
        pC.GetHit(damage, out playerAlive);
        battleLog.text += $"{damage}의 대미지!";

        yield return new WaitForSeconds(1f);
        if (!playerAlive)
        {
            battleLog.text = $"{pC.characterName}은(는) 쓰러졌다...\n" +
                $"{pC.characterName}은(는) 전투에서 패배했다...";
            battleState = BattleState.Defeat;
        }
        else if (playerAlive)
        {
            battleState = BattleState.Playerturn;
            WhosTurnNow();
        }
    }

    public void PlayerAttackBtn()
    {
        if (battleState == BattleState.Enemyturn)
            return;
        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        battleState = BattleState.Enemyturn;
        pC.Attack(out damage, out hitTiming);
        battleLog.text = $"{pC.characterName}의 공격!\n";

        yield return new WaitForSeconds(hitTiming);
        eS.GetHit(damage, out enemyAlive);
        battleLog.text += $"{damage}의 대미지!";

        yield return new WaitForSeconds(1f);
        if (!enemyAlive)
        {
            battleLog.text = $"{eS.unitName}은(는) 힘이 다했다.\n" +
                $"{pC.characterName}의 승리!";
            battleState = BattleState.Win;
        }
        else if (enemyAlive)
        {
            WhosTurnNow();
            EnemyTurnAttack();
        }
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
            battleLog.text = $"{pC.characterName}의 차례.\n무엇을 할까?";
            return;
        }
        battleState = BattleState.Enemyturn;
        battleLog.text = $"{eS.unitName}의 차례.";
    }
}
