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
        NowSceneCheck();
        Debug.Log(nowScene);
        Debug.Log(GameManager.instance.beforeSceneName);

        if (GameManager.instance.beforeSceneName == "Battle")
            return;

        PlayerWarpSpot();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.BeforeSceneReport();
            StartCoroutine(PotalMove());
        }
    }

    IEnumerator PotalMove()
    {
        GameManager.instance.SceneChangeEffect();

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
        GameObject player = GameObject.Find("Player");
        player.transform.position = teleportPoint.transform.position;
    }

    private void NowSceneCheck()
    {
        GameManager.instance.NowSceneReport();
        nowScene = GameManager.instance.nowSceneName;
    }
}
