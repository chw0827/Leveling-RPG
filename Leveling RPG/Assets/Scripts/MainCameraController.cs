using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public float speed;
    public float height;
    public float zDistance;

    private float playerX;
    private float playerY;
    private float playerZ;

    private Vector3 pos;

    private void Start()
    {
        DistanceCheck();
        transform.position = pos;
    }
    void FixedUpdate()
    {
        DistanceCheck();
        CameraFollow();
    }

    void CameraFollow()
    {
        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
    }

    void DistanceCheck()
    {
        GameObject player = GameManager.instance.player;

        playerX = player.transform.position.x;
        playerY = player.transform.position.y + height;
        playerZ = player.transform.position.z - zDistance;

        pos = new Vector3(playerX, playerY, playerZ);
    }
}
