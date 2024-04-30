using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public GameObject target;
    public float speed;
    private float distance;

    private Vector3 pos;


    void Start()
    {
        distance = target.transform.position.z - transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CameraFollow();
    }

    void CameraFollow()
    {
        pos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z - distance);
        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
    }
}
