using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeSC : MonoBehaviour
{
    //Common zone
    [HideInInspector] DataSC data;
    [HideInInspector] GeneralContrlSC genCtrl;
    [SerializeField] Text curretScore, currentGems;
    public Text facingText;
    int pCurrency, pGems;
    void Start()
    {
        genCtrl = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        data = GameObject.Find("CAN_GenControl").GetComponent<DataSC>();
        HandleHomeUIs();
        genCtrl.AssitsGamemode(0);
    }

    // Update is called once per frame
    void Update() { }
    public void OnShowSetting() => genCtrl.ShowSetting(true);
    public void OnShowPause() => genCtrl.ShowPause(true);
    public void OnShowReward() => genCtrl.ShowReward(true);
    public void OnShowShop() => genCtrl.ShowShop(true);
    public void OnShowPromotion() => genCtrl.ShowPromo(true);
    public void OnShowLeader()
    {
        genCtrl.ShowLeader(true);
    }
    public void OnShowAchievement()
    {
        genCtrl.ShowAchievement(true);
    }
    public void HandleHomeUIs()
    {
        pCurrency = data.pCoin;
        pGems = data.pGems;

        curretScore.text = pCurrency.ToString();
        currentGems.text = pGems.ToString();
    }
    #region Goes To Game Scenes
    public void ToArcadeJump()
    {
        genCtrl.GenLoadScene(2);
    }
    public void ToCutWood()
    {
        genCtrl.GenLoadScene(3);
    }
    public void ToPickAnimals()
    {
        genCtrl.GenLoadScene(4);
    }
    #endregion
}
