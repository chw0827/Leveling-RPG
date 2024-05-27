using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum BattleState
{
    Start, Playerturn, Enemyturn, Win, Defeat, 
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
    int playerBehaveCount;

    public GameObject enemySource;
    GameObject enemy;
    EnemyState eS;
    int totalEnemyCount;
    int enemyBehaveCount;
    int totalPriceMoney;
    int totalPlusExp;

    public Button[] playerActBtn;
    bool playerActBtnSetActive = true;

    public Transform playerStand;
    public Transform enemyStand;

    public Slider PlayerHpBar;
    public Animator victoryAnim;
    public Animator defeatAnim;

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

    IEnumerator BattleStart()
    {
        UnitSetting();
        UIUpdate();

        battleLog.text = $"{eS.unitName}이(가) 나타났다!";

        yield return new WaitForSeconds(2f);
        WhosTurnNow();
    }

    void UnitSetting()
    {
        player = GameManager.instance.player;
        pC = GameManager.instance.pc;

        player.transform.position = playerStand.position;
        player.transform.rotation = Quaternion.LookRotation(Vector3.back);

        enemy = Instantiate(enemySource, enemyStand.position, Quaternion.LookRotation(Vector3.forward));
        totalEnemyCount += 1;
        eS = enemy.GetComponent<EnemyState>();

        playerAlive = true;
        enemyAlive = true; 
    }

    void UIUpdate()
    {
        PlayerHpBar.value = pC.hp / pC.maxHp;
        playerLv.text = $"Lv {pC.Level}";
        playerHpCount.text = $"HP : {pC.hp}";
        playerName.text = $"이름 : {pC.characterName}";
        playerAttackP.text = $"공격력 : {pC.attackP}";
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        enemyBehaveCount--;
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
            if (enemyBehaveCount >= 1)
                StartCoroutine(EnemyTurn());
            else if (enemyBehaveCount <= 0)
            {
                WhosTurnNow();
                PlayerActBtnSetActive();
            }
        }
    }

    public void PlayerAttackBtn()
    {
        if (battleState != BattleState.Playerturn)
            return;

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        PlayerActBtnSetActive();
        playerBehaveCount--;
        pC.Attack(out damage, out hitTiming);
        battleLog.text = $"{pC.characterName}의 공격!\n";

        yield return new WaitForSeconds(hitTiming);
        eS.GetHit(damage, out enemyAlive);
        battleLog.text += $"{damage}의 대미지!";
       
        yield return new WaitForSeconds(1f);
        if (!enemyAlive)
        {
            totalPlusExp += eS.plusExp;
            totalPriceMoney += eS.price;
            battleLog.text = $"{eS.unitName}은(는) 힘이 다했다.";
            totalEnemyCount -= 1;
            yield return new WaitForSeconds(1.5f);

            if (totalEnemyCount <= 0)
                PlayerWin();
        }
        else if (enemyAlive)
        {
            if (playerBehaveCount >= 1)
                PlayerActBtnSetActive();
            else if (playerBehaveCount <= 0)
            {
                WhosTurnNow();
                StartCoroutine(EnemyTurn());
            }
        }
    }

    public void BattleRun()
    {
        if (battleState != BattleState.Playerturn || !playerAlive)
            return;

        StartCoroutine(PantsRun());
    }

    IEnumerator PantsRun()
    {
        PlayerActBtnSetActive();
        battleLog.text = "무사히 도망쳤다.";

        yield return new WaitForSeconds(1f);
        SceneChanger.instance.SceneChangeStart();

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(GameManager.instance.beforeSceneName);
    }

    public void VictoryBattleOutBtn()
    {
        StartCoroutine(VictoryBattleOut());
    }

    IEnumerator VictoryBattleOut()
    {
        SceneChanger.instance.SceneChangeStart();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(GameManager.instance.beforeSceneName);
    }

    void WhosTurnNow()
    {
        if (battleState != BattleState.Playerturn)
        {
            playerBehaveCount = 1;
            battleState = BattleState.Playerturn;
            battleLog.text = $"{pC.characterName}의 차례.\n무엇을 할까?";
            return;
        }

        enemyBehaveCount = 1;
        battleState = BattleState.Enemyturn;
        battleLog.text = $"{eS.unitName}의 차례.";
    }

    void PlayerActBtnSetActive()
    {
        if (playerActBtnSetActive)
        {
            for (int i = 0; i < playerActBtn.Length; i++)
            {
                playerActBtn[i].enabled = false;
            }
            playerActBtnSetActive = false;
            return;
        }

        for (int i = 0; i < playerActBtn.Length; i++)
        {
            playerActBtn[i].enabled = true;
        }
        playerActBtnSetActive = true;
    }

    void PlayerWin()
    {
        battleState = BattleState.Win;
        GameManager.instance.Money += totalPriceMoney;
        pC.Exp += totalPlusExp;
        battleLog.text = $"{pC.characterName}의 승리!\n " +
            $"{pC.characterName}은 {totalPriceMoney} 골드를 획득했다!\n" +
            $"추가로 {totalPlusExp}의 경험치를 획득했다!";
        victoryAnim.SetBool("victory", true);
    }
}
