using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    public GameObject player;

    private float playerX;
    private float playerZ;

    public float positionY;

    private Vector3 pos;

    private void Start()
    {
        CameraFollow();
    }

    private void LateUpdate()
    {
        CameraFollow();
    }

    void CameraFollow()
    {
        playerX = player.transform.position.x;
        positionY = transform.position.y;
        playerZ = player.transform.position.z;

        pos = new Vector3(playerX, positionY, playerZ);
        transform.position = pos;
    }
}
