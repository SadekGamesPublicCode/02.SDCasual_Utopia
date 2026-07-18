using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtkSC : MonoBehaviour
{
    private float timeBtwAtk; //1
    public float startTimeBtwAtk;

    public Transform atkPos;
    public LayerMask whatIsEnemy;
    public float atkRange;
    public int dmg;
    void Update()
    {
        if (timeBtwAtk <= 0)
        {
            //Allow Attack
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Collider2D[] enemiesToDmg = Physics2D.OverlapCircleAll(atkPos.position, atkRange, whatIsEnemy);
                print("in press Space");
                enemiesToDmg[0].GetComponent<TreeSC>().OnHandleChoping();
                timeBtwAtk = startTimeBtwAtk;
            }
            else
            {
                timeBtwAtk -= Time.deltaTime;
            }
        }
        print("timBtwAtk: " + timeBtwAtk);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(atkPos.position, atkRange);
    }
}
