using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.NowSceneReport();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.BeforeSceneReport();
            SceneManager.LoadScene("Field");
        }
    }
}
