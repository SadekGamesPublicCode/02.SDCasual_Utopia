using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepMN : MonoBehaviour
{
    [SerializeField] ArkMakingMNSC gameplayCtr;
    [SerializeField] Text progressTxt;
    int progressCount;
    private void Start()
    {
        progressCount = 0;
    }
    private void OnEnable()
    {
        if(gameplayCtr.playerHP < gameplayCtr.playerHPFull)
        {
            gameplayCtr.OnRefillHP();
        }
        InvokeRepeating(nameof(OnHandleSleepAnim), 0f, 1f);
        Invoke(nameof(OnHandleSleepComplate), 5f);
    }
    private void OnHandleSleepComplate()
    {
        gameObject.SetActive(false);
        CancelInvoke(nameof(OnHandleSleepAnim));
    }
    private void OnHandleSleepAnim()
    {
        progressCount++;
        if (progressCount == 1)
        {
            progressTxt.text = "SLEEPING.";
        }
        else if (progressCount == 2)
        {
            progressTxt.text = "SLEEPING..";
        }
        else if (progressCount == 3)
        {
            progressCount = 0;
            progressTxt.text = "SLEEPING...";
        }
    }
}
