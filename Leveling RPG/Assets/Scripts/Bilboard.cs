using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilboard : MonoBehaviour
{
    private Transform target;

    private void Start()
    {
        target = Camera.main.transform;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(-target.forward, target.up);
    }
}
