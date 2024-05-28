using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle, Wait, Move, BattleIdle, Attack, Hit, Dead
}

public class PlayerController : MonoBehaviour
{
    public PlayerState playerstate;
    public string characterName;
    public float speed;
    public Animator anim;

    private Vector3 look;

    public GameObject sword;
    private int level = 1;
    public int Level { get { return level; } set { LevelUpStat(value); } }
    int expBar = 100;
    private int exp;
    public int Exp { get { return exp; } set { ExpCalc(value); } }
    public int attackP;
    public float maxHp;
    public float hp;

    private void FixedUpdate()
    {
        if (GameManager.instance.nowSceneName == "Battle")
            return;

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

        transform.rotation = Quaternion.LookRotation(Vector3.forward);

        yield return new WaitForSeconds(0.4f);

        playerstate = PlayerState.Idle;
    }
    
    public void BattleModeSet(string SceneName)
    {
        if (SceneName == "Battle")
        {
            playerstate = PlayerState.BattleIdle;
            anim.SetBool("battle", true);
            sword.SetActive(true);
            return;
        }

        playerstate = PlayerState.Idle;
        anim.SetBool("battle", false);
        sword.SetActive(false);
    }

    public void Attack(out int attackP, out float hitTiming)
    {
        attackP = this.attackP;
        hitTiming = 1f;
        playerstate = PlayerState.Attack;
        anim.SetTrigger("attack");
    }

    public void GetHit(int damage, out bool alive)
    {
        playerstate = PlayerState.Hit;
        anim.SetTrigger("getHit");
        hp -= damage;
        alive = true;

        if (hp <= 0)
        {
            alive = false;   
        }
    }

    public void Death()
    {
        playerstate = PlayerState.Dead;
        anim.SetBool("death", true);
    }

    void LevelUpStat(int levelUp)
    {
        level += levelUp;
        attackP += levelUp * 2;
        maxHp += levelUp * 6;
        hp = maxHp;
    }

    public void ExpCalc(int plusExp)
    {
        exp = plusExp;
        int totalLvUp = 0;

        if (exp >= expBar)
        {
            do
            {
                exp -= expBar;
                totalLvUp += 1;
                expBar += expBar / 2;
            } while ((exp - expBar) >= 0);

            Level = totalLvUp;
        }
    }
}
