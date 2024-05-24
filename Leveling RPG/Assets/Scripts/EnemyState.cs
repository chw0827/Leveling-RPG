using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyState : MonoBehaviour
{
    public Animator anim;
    public TMP_Text levelTxt;
    public TMP_Text unitNameTxt;
    public Slider unitHpSdr;

    public string unitName;
    public int level;
    public int attackP;
    public float maxHp;
    public float hp;
    public int plusExp;
    public int price;

    private void Update()
    {
        StateUpdate();
    }

    void StateUpdate()
    {
        levelTxt.text = $"LV {level}";
        unitNameTxt.text = $"{unitName}";
        unitHpSdr.value = hp / maxHp;
    }

    public void Attack(out int attackP, out float hitTiming)
    {
        attackP = this.attackP;
        hitTiming = 0.9f;
        anim.SetTrigger("attack");
    }

    public void GetHit(int damage, out bool alive)
    {
        anim.SetTrigger("getHit");
        hp -= damage;
        alive = true;

        if (hp <= 0)
        {
            alive = false;
        }
    }

    public void Death(out int price, out int exp)
    {
        price = this.price;
        exp = this.plusExp;
        anim.SetBool("death", true);
    }
}
