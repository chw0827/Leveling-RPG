using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleEncounter : MonoBehaviour
{
    GameObject player;

    public int encoutercount;
    private Vector3 nowPlayerPosition;
    private Vector3 transPlayerPosition;
    private float playerMoveDistance;
    private bool playerOutCheck;

    private void Start()
    {
        player = GameManager.instance.player;

        if (GameManager.instance.beforeSceneName != "Battle")
            return;

        player.transform.position = GameManager.instance.LastPlayerPositionLoad();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(RandomEncounter());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            playerOutCheck = true;
    }

    IEnumerator RandomEncounter()
    {
        encoutercount = 80;
        playerOutCheck = false;

        while (!playerOutCheck)
        {
            nowPlayerPosition = player.transform.position;

            yield return new WaitForSeconds(0.1f);

            transPlayerPosition = player.transform.position;
            playerMoveDistance = Vector3.Distance(nowPlayerPosition, transPlayerPosition);

            if (playerMoveDistance == 0)
                continue;

            nowPlayerPosition = transPlayerPosition;

            if (Random.Range(0, encoutercount) == 0)
            {
                SceneChanger.instance.SceneChangeStart();
                GameManager.instance.LastPlayerPositionSave();

                yield return new WaitForSeconds(0.5f);

                playerOutCheck = true;
                SceneManager.LoadScene("Battle");
            }
            encoutercount--;
        }
    }
}
