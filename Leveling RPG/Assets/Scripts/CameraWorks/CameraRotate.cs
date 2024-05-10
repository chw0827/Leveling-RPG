using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private void Update()
    {
        Rotate();
    }

    IEnumerator Rotate()
    {
        while (true)
        {
            transform.Rotate(new Vector3(0, 1 * Time.deltaTime, 0));
            yield return new WaitForSeconds(10f);
        }
    }
}
