using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorAtkSC : MonoBehaviour
{
    private float timeBtwAtk; //1
    float startTimeBtwAtk;

    public Transform atkPos;
    public LayerMask whatIsEnemy;
    public float atkRange;
    public int dmg;
    private void Start()
    {
        timeBtwAtk = 1;
    }
    void Update()
    {
        //Allow Attack
        if (timeBtwAtk <= 0)
        {
            OnHandleAtack();
        }
        timeBtwAtk -= Time.deltaTime;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(atkPos.position, atkRange);
    }
    public void OnHandleAtack()
    {
        Collider2D[] enemiesToDmg = Physics2D.OverlapCircleAll(atkPos.position, atkRange, whatIsEnemy);
        if(enemiesToDmg.Length >= 1)
        {
            print("enemiesToDam = " + enemiesToDmg[0].name);
            enemiesToDmg[0].GetComponent<NoahSC>().OnTakeDamage();
            timeBtwAtk = startTimeBtwAtk;
        }
    }
}
