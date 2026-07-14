using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceItemSC : MonoBehaviour
{
    [HideInInspector] internal GeneralContrlSC genCtr;
    [HideInInspector] internal ArkMakingMNSC cutwoodMn;
    protected virtual void Start()
    {
        genCtr = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        cutwoodMn = GameObject.Find("CAN_ArkMaking").GetComponent<ArkMakingMNSC>();
    }
    void Update() { }
    internal void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Noah")
        {
            print("destroyed");
            Destroy(gameObject);
        }
    }
}
