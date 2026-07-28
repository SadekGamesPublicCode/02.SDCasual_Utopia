using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;

public class AnimalSC : MonoBehaviour, IDamageableTarget
{
    Vector3 previousPos, newPos;
    [HideInInspector] ArkMakingMNSC genCtr;
    [HideInInspector]
    AnimalSpawnerSC animalSpawnStr;
    [HideInInspector] GeneralContrlSC omniCtr;
    [SerializeField] GameObject dropItem, coin, expDrop;
    [HideInInspector] NoahSC player;
    [SerializeField] internal bool isPredators;
    internal int nutritionAmount;
    internal int hitCount;
    internal int chanceDropMoney;
    internal int expToDrop = 3;
    protected virtual void Start()
    {
        previousPos = gameObject.transform.position;
        genCtr = GameObject.Find("GameplayMN").GetComponent<ArkMakingMNSC>();
        animalSpawnStr = GameObject.Find("GameplayMN").GetComponent<AnimalSpawnerSC>();
        omniCtr = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        InvokeRepeating(nameof(WanderAround), 0f, 2f);
        hitCount = 0;
        Invoke(nameof(CaculateChanceToDropCoin), 5f);
        player = GameObject.Find("GameplayMN").GetComponent<NoahSC>();
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
        if (isPredators == false)
        {
            //Run away
            transform.DOMove(new Vector3(transform.position.x - 1, transform.position.y, 0), 0.2f);
        }
    }
    internal void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DoBehaviour();
        }
    }
    internal void PredatorOnCollide()
    {
        //Attack back
        hitCount++;
        if (hitCount >= 3)
        {
            for(int i = 0; i < expToDrop; i++)
            {
                Instantiate(expDrop, transform.position, Quaternion.identity);
            }

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
            animalSpawnStr.curPreyOnScreen--;
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
            animalSpawnStr.curPredatorOnScreen--;
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
    public virtual void OnTakeDamage() 
    {
        if(isPredators == true)
        {
            PredatorOnCollide();
        }else if(isPredators == false)
        {
            PreyInCollide();
        }
    }
}
