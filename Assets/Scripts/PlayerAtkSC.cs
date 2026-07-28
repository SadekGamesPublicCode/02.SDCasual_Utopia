using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAtkSC : MonoBehaviour
{
    [SerializeField] GameObject weapPivot;
    private float timeBtwAtk; //1
    float startTimeBtwAtk;

    public Transform atkPos;
    public LayerMask whatIsEnemy;
    public float atkRange;
    public int dmg;
    private void Start()
    {
        timeBtwAtk = 3;
    }
    void Update()
    {
        //Allow Attack
        if (Input.GetKeyDown(KeyCode.Space) && timeBtwAtk <= 0)
        {
           OnHandleAtack();
        }
        timeBtwAtk -= Time.deltaTime;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(atkPos.position, atkRange);
    }
    public void OnHandleAtack()
    {
        Collider2D[] enemiesToDmg = Physics2D.OverlapCircleAll(atkPos.position, atkRange, whatIsEnemy);
        if (enemiesToDmg.Length >= 1)
        {

            enemiesToDmg[0].GetComponent<IDamageableTarget>().OnTakeDamage();
            weapPivot.transform.DOLocalRotate(new Vector3(0, 0, 90f), 0.2f).SetLoops(2, LoopType.Yoyo);
            timeBtwAtk = startTimeBtwAtk;
        }
    }
}
