using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Potal : MonoBehaviour
{
    public GameObject teleportPoint;
    private string nowScene;

    private void Start()
    {
        nowScene = GameManager.instance.nowSceneName;

        if (GameManager.instance.beforeSceneName == "Battle")
            return;

        PlayerWarpSpot();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PotalMove());
        }
    }

    IEnumerator PotalMove()
    {
        SceneChanger.instance.SceneChangeStart();

        yield return new WaitForSeconds(0.5f);

        switch (nowScene)
        {
            case "Village":
                SceneManager.LoadScene("Field");
                break;
            case "Field":
                SceneManager.LoadScene("Village");
                break;
            default:
                break;
        }
    }

    private void PlayerWarpSpot()
    {
        GameObject player = GameManager.instance.player;
        player.transform.position = teleportPoint.transform.position;
    }
}
