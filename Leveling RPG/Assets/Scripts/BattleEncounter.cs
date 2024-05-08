using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleEncounter : MonoBehaviour
{
    public GameObject player;
    public int encouterPercent;
    private Vector3 nowPlayerPosition;
    private Vector3 transPlayerPosition;
    private float playerMoveDistance;
    private bool playerOutCheck; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("In");
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
        encouterPercent = 20;
        playerOutCheck = false;

        while (!playerOutCheck)
        {
            nowPlayerPosition = player.transform.position;

            yield return new WaitForSeconds(0.5f);

            transPlayerPosition = player.transform.position;
            playerMoveDistance = Vector3.Distance(nowPlayerPosition, transPlayerPosition);
            
            if (playerMoveDistance == 0)
                continue;

            nowPlayerPosition = transPlayerPosition;

            if (Random.Range(0, encouterPercent) == 0)
            {
                GameManager.instance.SceneChangeEffect();

                yield return new WaitForSeconds(0.5f);

                playerOutCheck = true;
                SceneManager.LoadScene("Battle");
            }
            encouterPercent--;
        }
    }
}
