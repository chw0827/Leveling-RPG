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

    public GameObject enemySource;
    GameObject enemy;
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

        battleLog.text = $"{eS.unitName}��(��) ��Ÿ����!";

        yield return new WaitForSeconds(2.5f);
        PlayersTurnNow();
    }

    void UnitSetting()
    {
        player = GameManager.instance.player;
        pC = GameManager.instance.pc;

        player.transform.position = playerStand.position;
        player.transform.rotation = Quaternion.LookRotation(Vector3.back);

        enemy = Instantiate(enemySource, enemyStand.position, Quaternion.LookRotation(Vector3.forward));
        eS = enemy.GetComponent<EnemyState>();

        playerAlive = true;
        enemyAlive = true; 
    }

    void UIUpdate()
    {
        PlayerHpBar.value = pC.hp / pC.maxHp;
        playerLv.text = $"Lv {pC.level}";
        playerHpCount.text = $"HP : {pC.hp}";
        playerName.text = $"�̸� : {pC.characterName}";
        playerAttackP.text = $"���ݷ� : {pC.attackP}";
    }

    void EnemyTurnAttack()
    {
        StartCoroutine(EnemyAttack());
    }

    IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(1f);

        eS.Attack(out damage, out hitTiming);
        battleLog.text = $"{eS.unitName}�� ����!\n";

        yield return new WaitForSeconds(hitTiming);
        pC.GetHit(damage, out playerAlive);
        battleLog.text += $"{damage}�� �����!";

        yield return new WaitForSeconds(1f);
        if (!playerAlive)
        {
            battleLog.text = $"{pC.characterName}��(��) ��������...\n" +
                $"{pC.characterName}��(��) �������� �й��ߴ�...";
            battleState = BattleState.Defeat;
        }
        else if (playerAlive)
        {
            PlayersTurnNow();
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
        battleLog.text = $"{pC.characterName}�� ����!\n";

        yield return new WaitForSeconds(hitTiming);
        eS.GetHit(damage, out enemyAlive);
        battleLog.text += $"{damage}�� �����!";

        yield return new WaitForSeconds(1f);
        if (!enemyAlive)
        {
            battleLog.text = $"{eS.unitName}��(��) ���� ���ߴ�.\n" +
                $"{pC.characterName}�� �¸�!";
            battleState = BattleState.Win;
        }
        else if (enemyAlive)
        {
            EnemyTurnAttack();
        }
    }

    public void BattleRun()
    {
        if (battleState == BattleState.Enemyturn || !playerAlive)
            return;

        StartCoroutine(PantsRun());
    }

    IEnumerator PantsRun()
    {
        battleLog.text = "������ �����ƴ�.";

        yield return new WaitForSeconds(1f);
        SceneChanger.instance.SceneChangeStart();

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(GameManager.instance.beforeSceneName);
    }

    void PlayersTurnNow()
    {
        if (battleState != BattleState.Playerturn)
        {
            battleState = BattleState.Playerturn;
            battleLog.text = $"{pC.characterName}�� ����.\n������ �ұ�?";
        }
    }
}
