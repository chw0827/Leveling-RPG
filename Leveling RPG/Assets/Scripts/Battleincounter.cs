using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battleincounter : MonoBehaviour
{
    public GameObject player;

    private Vector3 nowPlayerPosition;
    public float playerMoveDistance;
    public float minMoveDistance;

    private void OnTriggerEnter(Collider other)
    {
        if (other == player)
        {
            nowPlayerPosition = player.transform.position;
        } 
    }
    private void OnTriggerStay(Collider other)
    {
        if (other == player)
        {
            playerMoveDistance = Vector3.Distance(nowPlayerPosition, nowPlayerPosition);
        }
    }
}
