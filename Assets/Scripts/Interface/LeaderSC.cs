using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderSC : MonoBehaviour
{
    [HideInInspector] GeneralContrlSC genCtrl; 
    void Start()
    {
        genCtrl = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToPlayerInfo() => genCtrl.ShowInfor(true);
}
