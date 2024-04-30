using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public GameObject target;
    public float speed;
    private float yDistance;
    private float zDistance;

    private Vector3 pos;

    private void Start()
    {
        yDistance = transform.position.y - target.transform.position.y;
        zDistance = target.transform.position.z - transform.position.z;
    }

    void FixedUpdate()
    {
        CameraFollow();
    }

    void CameraFollow()
    {
        pos = new Vector3(target.transform.position.x, target.transform.position.y + yDistance, target.transform.position.z - zDistance);
        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
    }
}
