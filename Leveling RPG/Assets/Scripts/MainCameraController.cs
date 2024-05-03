using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public GameObject target;
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
        playerX = target.transform.position.x;
        playerY = target.transform.position.y + height;
        playerZ = target.transform.position.z - zDistance;

        pos = new Vector3(playerX, playerY, playerZ);
    }
}
