using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToFieldPotal : MonoBehaviour
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
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("Field");
    }
}
