using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battleincounter : MonoBehaviour
{
    private Vector3 nowPlayerPosition;
    public float playerMoveDistance;
    public float minMoveDistance;
    public int moveCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nowPlayerPosition = other.transform.position;
        } 
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            if (other.transform.position != nowPlayerPosition)
        }
    }
}
