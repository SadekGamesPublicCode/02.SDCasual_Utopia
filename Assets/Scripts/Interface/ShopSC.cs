using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShopSC : MonoBehaviour
{
    [SerializeField] GameObject shopPnl, warningPnl;
    [SerializeField] GeneralContrlSC genCtr;
    [SerializeField] AdsMN adsCtr;
    [SerializeField] DataSC dataCtr;
    [SerializeField] Text curPlayerCoinTxt, curPlayerGemsTxt;

    [HideInInspector] int curPCoin, curPGems;
    [HideInInspector] int unitPrice;
    [HideInInspector] int itemIndexBuy;
    void Start()
    {
        genCtr = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        adsCtr = GameObject.Find("AdsMN").GetComponent<AdsMN>();
        dataCtr = GameObject.Find("CAN_GenControl").GetComponent<DataSC>();
        SwitchPanel(true, false);
        LoadDatas();
        HandleShopUIs();

        itemIndexBuy = -1;
    }
    void SwitchPanel(bool isShowShop, bool isShowWarn)
    {
        shopPnl.SetActive(isShowShop);
        warningPnl.SetActive(isShowWarn);
    }
    void LoadDatas()
    {
        curPCoin = dataCtr.pCoin;
        curPGems = dataCtr.pGems;
    }
    void HandleShopUIs()
    {
        LoadDatas();
        curPlayerGemsTxt.text = curPGems.ToString();
        curPlayerCoinTxt.text = curPCoin.ToString();
    }
    public void OnBuyBonus(int index)
    {
        itemIndexBuy = index;
        switch (index)
        {
            case 0:
                //Woods bonus
                unitPrice = 100;
                if (IsAllowBuy(unitPrice) == true)
                {
                    //Do buy
                    HandleBuyItem(unitPrice);
                    genCtr.OnUpdateBoostingFromShop(0);
                } else SwitchPanel(false, true);
                break;
            case 1:
                //Iron bonus
                unitPrice = 100;
                if (IsAllowBuy(unitPrice) == true)
                {
                    //Do buy
                    HandleBuyItem(unitPrice);
                    genCtr.OnUpdateBoostingFromShop(1);
                }
                else SwitchPanel(false, true);
                break;
            case 2:
                //Stone Bonus
                unitPrice = 100;
                if (IsAllowBuy(unitPrice) == true)
                {
                    //Do buy
                    HandleBuyItem(unitPrice);
                    genCtr.OnUpdateBoostingFromShop(2);
                }
                else SwitchPanel(false, true);
                break;
            case 3:
                //Crop Bonus
                unitPrice = 100;
                if (IsAllowBuy(unitPrice) == true)
                {
                    //Do buy
                    HandleBuyItem(unitPrice);
                    genCtr.OnUpdateBoostingFromShop(3);
                }
                else SwitchPanel(false, true);
                break;
            case 4:
                //Fruist Bonus
                unitPrice = 100;
                if (IsAllowBuy(unitPrice) == true)
                {
                    //Do buy
                    HandleBuyItem(unitPrice);
                    genCtr.OnUpdateBoostingFromShop(4);
                }
                else SwitchPanel(false, true);
                break;
            case 5:
                //All Bonus
                unitPrice = 150;
                if (IsAllowBuy(unitPrice) == true)
                {
                    //Do buy
                    HandleBuyItem(unitPrice);
                    genCtr.OnUpdateBoostingFromShop(5);  
                }
                else SwitchPanel(false, true);
                break;
        }
    }
    void HandleBuyItem(int value)
    {
        int tempCurCurrency;
        tempCurCurrency = curPCoin;
        curPCoin = tempCurCurrency - value;
        dataCtr.UpdateTotalScore(curPCoin);

        genCtr.OnLoadBoosting(); //Trigger for GenCtr to re-load the boosting
        HandleShopUIs();
    }
    bool IsAllowBuy(int itemPrice)
    {
        if(itemPrice <= curPCoin)
        {
            return true;
        }
        return false;
    }
    public void OnDoneWarningBuy() => SwitchPanel(true, false);
    public void BuyByAds()
    {
        adsCtr.ShowAds(2);
        genCtr.OnUpdateBoostingFromShop(itemIndexBuy);
        genCtr.OnLoadBoosting();
        OnDoneWarningBuy();
        itemIndexBuy = -1;
    }
}
