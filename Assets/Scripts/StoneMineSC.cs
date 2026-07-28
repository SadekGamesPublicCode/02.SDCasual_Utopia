using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StoneMineSC : MonoBehaviour, IDamageableTarget
{
    [SerializeField] GameObject stone;
    [HideInInspector] ArkMakingMNSC genCtr;
    [HideInInspector] GeneralContrlSC omniCtr;
    private int resourceAmount, curResourceCount;
    private int contactCount;
    void Start()
    {
        genCtr = GameObject.Find("GameplayMN").GetComponent<ArkMakingMNSC>();
        omniCtr = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        resourceAmount = 100;
        curResourceCount = 0;
        contactCount = 0;
    }
    public virtual void OnTakeDamage()
    {
        contactCount++;
        if(contactCount >= 3)
        {
            curResourceCount++;
            Vector3 tempPos;
            tempPos = new Vector3(gameObject.transform.position.x + (Random.Range(2, 4)), gameObject.transform.position.y + (Random.Range(1, 3)), 0);
            if (omniCtr.isBoostStone == 1)
            {
                Instantiate(stone, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);
                Instantiate(stone, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);
            }
            else if (omniCtr.isBoostStone != 1)
            {
                Instantiate(stone, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);
            }
            stone.transform.DOMove(tempPos, 1f);
            if (curResourceCount >= resourceAmount)
            {
                Destroy(gameObject);
            }
        }
    }
}
