using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class TreeSC : MonoBehaviour
{
    //Do check bonus
    [SerializeField] NoahSC noah;
    [SerializeField] GameObject wood, fruits;
    [HideInInspector] ArkMakingMNSC genCtr;
    [HideInInspector] GeneralContrlSC omniCtr;
    int hitCount, randWoodDrop;
    int chanceDropFruits;
    void Start()
    {
        genCtr = GameObject.Find("CAN_ArkMaking").GetComponent<ArkMakingMNSC>();
        omniCtr = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        hitCount = 0;
        randWoodDrop = Random.Range(1, 4);
        chanceDropFruits = Random.Range(1, 100);
        noah = GameObject.Find("OBJ_Noah(Clone)").GetComponent<NoahSC>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Axe")
        {
            OnHandleChoping();
        }
    }
    private void OnSpawnWood()
    {
        if(omniCtr.isBoostWood == 1)
        {
            for (int i = 1; i < randWoodDrop * 2; i++)
            {
                Instantiate(wood, new Vector3(gameObject.transform.position.x - 0.25f, gameObject.transform.position.y, 0), Quaternion.identity);
            }
        }else  if(omniCtr.isBoostWood != 1)
        {
            for (int i = 1; i < randWoodDrop; i++)
            {
                Instantiate(wood, new Vector3(gameObject.transform.position.x - 0.25f, gameObject.transform.position.y, 0), Quaternion.identity);
            }
        }
       
    }
    private void OnSpawnFruist()
    {
        if(omniCtr.isBoostFruits == 1)
        {
            Instantiate(fruits, new Vector3(gameObject.transform.position.x - 0.25f, gameObject.transform.position.y, 0), Quaternion.identity);
            Instantiate(fruits, new Vector3(gameObject.transform.position.x + 0.25f, gameObject.transform.position.y, 0), Quaternion.identity);
        }
        else if(omniCtr.isBoostFruits != 1)
        {
            Instantiate(fruits, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);
        }
    }
    private void OnHandleChoping()
    {
        hitCount++;
        if (hitCount >= 3)
        {
            OnSpawnWood();
            if (chanceDropFruits > 20) OnSpawnFruist();
            genCtr.curTreeOnScreen--;
            Destroy(gameObject);
        }
    }
}
