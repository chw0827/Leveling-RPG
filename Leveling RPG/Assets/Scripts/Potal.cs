using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Potal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FieldPotalMove());
        }
    }

    IEnumerator FieldPotalMove()
    {
        string nowScene = SceneManager.GetActiveScene().name;
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
}
