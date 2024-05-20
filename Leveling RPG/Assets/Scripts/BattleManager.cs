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
    public bool enemyAlive;
    public bool playerAlive;

    public GameObject bear;

    public Transform playerStand;
    public Transform enemyStand;

    public TMP_Text battleLog;

    private void Awake()
    {
        BattleStart();
    }

    void BattleStart()
    {
        battleState = BattleState.Start;

        Instantiate(bear).transform.position = enemyStand.position;
        bear.transform.rotation = Quaternion.LookRotation(Vector3.forward);

        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = playerStand.position;
        player.transform.rotation = Quaternion.LookRotation(Vector3.back);
    }

    public void BattleRun()
    {
        SceneManager.LoadScene(GameManager.instance.beforeSceneName);
    }
}
