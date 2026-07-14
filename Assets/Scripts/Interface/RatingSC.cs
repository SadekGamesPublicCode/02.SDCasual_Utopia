using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingSC : MonoBehaviour
{
    [HideInInspector] GeneralContrlSC genCtr;
    [SerializeField] List<Image> rateFade = new List<Image>();
    [SerializeField] List<Image> rateLight = new List<Image>();
    void Start()
    {
        genCtr = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        SetLightInit();
    }
    private void SetLightInit() { for (int i = 0; i < rateLight.Count; i++) rateLight[i].gameObject.SetActive(false); }
    public void OnRateOneStar()
    {
        rateFade[0].gameObject.SetActive(false);
        rateLight[0].gameObject.SetActive(true);
        OnToRatePage();
    }
    public void OnRateTwoStar()
    {
        rateFade[0].gameObject.SetActive(false);
        rateLight[0].gameObject.SetActive(true);

        rateFade[1].gameObject.SetActive(false);
        rateLight[1].gameObject.SetActive(true);
        OnToRatePage();
    }
    public void OnRateThreeStar()
    {
        rateFade[0].gameObject.SetActive(false);
        rateLight[0].gameObject.SetActive(true);
        rateFade[1].gameObject.SetActive(false);
        rateLight[1].gameObject.SetActive(true);

        rateFade[2].gameObject.SetActive(false);
        rateLight[2].gameObject.SetActive(true);
        OnToRatePage();
    }
    public void OnRateFourStar()
    {
        rateFade[0].gameObject.SetActive(false);
        rateLight[0].gameObject.SetActive(true);
        rateFade[1].gameObject.SetActive(false);
        rateLight[1].gameObject.SetActive(true);
        rateFade[2].gameObject.SetActive(false);
        rateLight[2].gameObject.SetActive(true);

        rateFade[3].gameObject.SetActive(false);
        rateLight[3].gameObject.SetActive(true);
        OnToRatePage();
    }
    public void OnRateFiveStar()
    {
        rateFade[0].gameObject.SetActive(false);
        rateLight[0].gameObject.SetActive(true);
        rateFade[1].gameObject.SetActive(false);
        rateLight[1].gameObject.SetActive(true);
        rateFade[2].gameObject.SetActive(false);
        rateLight[2].gameObject.SetActive(true);
        rateFade[3].gameObject.SetActive(false);
        rateLight[3].gameObject.SetActive(true);

        rateFade[4].gameObject.SetActive(false);
        rateLight[4].gameObject.SetActive(true);
        OnToRatePage();
    }
    private void OnToRatePage()
    { Application.OpenURL("https://play.google.com/store/apps/details?id=com.SDSoft.Arcade_UtopiaRise_2D&pcampaignid=web_share");    }
}
