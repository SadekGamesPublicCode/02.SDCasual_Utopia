using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;

public class AnimalSC : MonoBehaviour
{
    Vector3 previousPos, newPos;
    [HideInInspector] ArkMakingMNSC genCtr;
    [HideInInspector] GeneralContrlSC omniCtr;
    [SerializeField] GameObject dropItem, coin;
    internal bool isPredators;
    internal int nutritionAmount;
    internal int hitCount;
    internal int chanceDropMoney;
    protected virtual void Start()
    {
        previousPos = gameObject.transform.position;
        genCtr = GameObject.Find("CAN_ArkMaking").GetComponent<ArkMakingMNSC>();
        omniCtr = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        InvokeRepeating(nameof(WanderAround), 0f, 2f);
        hitCount = 0;
        Invoke(nameof(CaculateChanceToDropCoin), 5f);
    }

    void WanderAround()
    {
        float tempX, tempY;
        tempX = Random.Range((float)previousPos.x - 0.25f, (float)previousPos.x + 0.25f);
        tempY = Random.Range((float)previousPos.y - 0.25f, (float)previousPos.y + 0.25f);
        newPos = new Vector3(tempX, tempY, 0);
        transform.DOMove(newPos, 1f);
        previousPos = newPos;
    }
    internal void DoBehaviour()
    {
        if (isPredators == true)
        {
            //Do something with player
        }else
        {
            //Run away
            transform.DOMove(new Vector3(transform.position.x - 0.5f, transform.position.y, 0), 0.5f);
        }
    }
    internal void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            DoBehaviour();
        }else if(collision.gameObject.tag == "Axe")
        {
            if(isPredators == true) { PredatorOnCollide();}
            else if(isPredators == false) { PreyInCollide(); }
        }
    }
    internal void PredatorOnCollide()
    {
        //Attack back
        hitCount++;
        if (hitCount >= 3)
        {
            if(omniCtr.isBoostFruits == 1)
            {
                Instantiate(dropItem, new Vector3(gameObject.transform.position.x - 0.25f, gameObject.transform.position.y, 0), Quaternion.identity);
                Instantiate(dropItem, new Vector3(gameObject.transform.position.x + 0.25f, gameObject.transform.position.y, 0), Quaternion.identity);
            }
            else if(omniCtr.isBoostFruits != 1)
            {
                Instantiate(dropItem, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);
            }

            if (chanceDropMoney > 20)
            {
                Instantiate(coin, new Vector3(gameObject.transform.position.x - 0.25f, gameObject.transform.position.y, 0), Quaternion.identity);
            }
            genCtr.curPreyOnScreen--;
            Destroy(gameObject);
        }else
        {
            //Attack back
            DoBehaviour();
        }
    }
    internal void PreyInCollide()
    {
        hitCount++;
        if (hitCount >= 3)
        {
            //Case of decease
            if (omniCtr.isBoostFruits == 1)
            {
                Instantiate(dropItem, new Vector3(gameObject.transform.position.x - 0.25f, gameObject.transform.position.y, 0), Quaternion.identity);
                Instantiate(dropItem, new Vector3(gameObject.transform.position.x + 0.25f, gameObject.transform.position.y, 0), Quaternion.identity);
            }
            else if (omniCtr.isBoostFruits != 1)
            {
                Instantiate(dropItem, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);
            }

            if (chanceDropMoney > 70)
            {
                Instantiate(coin, new Vector3(gameObject.transform.position.x - 0.25f, gameObject.transform.position.y, 0), Quaternion.identity);
            }
            genCtr.curPredatorOnScreen--;
            Destroy(gameObject);
        }else
        {
            DoBehaviour();
        }
    }
    internal void CaculateChanceToDropCoin()
    {
        if (isPredators)
        {
            chanceDropMoney = Random.Range(40, 100);
        }else
        {
            chanceDropMoney = Random.Range(0, 100);
        }
    }
}
