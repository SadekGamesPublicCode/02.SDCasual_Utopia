using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSC : Singleton<PauseSC>
{
    GeneralContrlSC genCtrl;
    private ArkMakingMNSC gamectr;
    void Start()
    {
        genCtrl = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
    }

    public void QuitGame()
    {
        Application.Quit();
        //UPdate Data
    }
    public void Resume()
    {
        //Resume All Corotine
    }
    public void ToHome()
    {
        //Update Datas
        genCtrl.OnToHome();
    }
}
