using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyState : MonoBehaviour
{
    public Animator anim;
    public Canvas enemyCanvas;
    public TMP_Text levelTxt;
    public TMP_Text unitNameTxt;
    public Slider unitHpSdr;

    public string unitName;
    public int level;
    public int attackP;
    public float maxHp;
    public float hp;
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

    public void Attack()
    {
        anim.SetTrigger("attack");
    }

    public void GetHit(int damage)
    {
        anim.SetTrigger("getHit");
        hp -= damage;
    }
}
