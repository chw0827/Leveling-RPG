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
    public Animator anim;

    private Vector3 look;

    private void FixedUpdate()
    {
        PlayerMove();   
    }

    private void PlayerMove()
    {
        if (playerstate != PlayerState.Wait)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            if (x != 0 || z != 0)
            {
                look = (Vector3.right * x) + (Vector3.forward * z);
                transform.rotation = Quaternion.LookRotation(look);

                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                playerstate = PlayerState.Move;
                anim.SetBool("move", true);
            }
            else
            {
                playerstate = PlayerState.Idle;
                anim.SetBool("move", false);
            }
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
        anim.SetBool("move", false);

        yield return new WaitForSeconds(0.6f);

        transform.rotation = Quaternion.LookRotation(Vector3.zero + Vector3.forward);

        yield return new WaitForSeconds(1f);

        playerstate = PlayerState.Idle;
    }
}
