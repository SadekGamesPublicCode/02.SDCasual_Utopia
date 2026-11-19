using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.UI;

public class PlayerInforSC : MonoBehaviour
{
    [SerializeField] InputField usernameField;
    [SerializeField] DataSC dataCtr;

    [SerializeField] Text nameTxt, coinTxt, gemTxt, curLvTxt, dailyStreakTxt; 

    [HideInInspector] string playerName;
    private void Start() { }
    public void OnSubmitLocal()
    {
        playerName = usernameField.text;
        dataCtr.UpdatePName(playerName);
    }
    public void OnEnable()
    {
        OnLoadPlayerInfors();
    }
    public void OnLoadPlayerInfors()
    {
        nameTxt.text = dataCtr.pName.ToUpper();
        coinTxt.text = dataCtr.pCoin.ToString();
        gemTxt.text = dataCtr.pGems.ToString();
        curLvTxt.text = dataCtr.pLv.ToString();
        dailyStreakTxt.text = dataCtr.pDailyStreak.ToString();
    }
}
