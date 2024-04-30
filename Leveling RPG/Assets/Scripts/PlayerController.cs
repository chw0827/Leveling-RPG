using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Wait,
    Move,
    BattleIdle,
    Attack,
    Hit,
    Dead
}

public class PlayerController : MonoBehaviour
{
    public PlayerState playerstate;
    public float speed;

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if (playerstate != PlayerState.Wait)
        {
            if (x != 0 || z != 0)
            {
                transform.Translate(new Vector3(x, 0, z) * speed * Time.deltaTime);
                playerstate = PlayerState.Move;
            }
            else
                playerstate = PlayerState.Idle;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Potal"))
            StartCoroutine(PotalWait());
    }

    IEnumerator PotalWait()
    {
        playerstate = PlayerState.Wait;

        yield return new WaitForSeconds(1f);

        playerstate = PlayerState.Idle;
    }
}
