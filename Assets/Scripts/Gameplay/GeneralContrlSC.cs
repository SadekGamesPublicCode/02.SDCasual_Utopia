using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GeneralContrlSC : Singleton<GeneralContrlSC>
{
    [HideInInspector] PauseSC pausPnl;
    [HideInInspector] SettingSC settingPnl;
    [HideInInspector] PlayerInforSC playerPnl;
    [HideInInspector] DailyRewardSC dailyrewardPnl;
    [HideInInspector] ShopSC shopCtr;
    [HideInInspector] SoundSC sfxMuzik;
    [HideInInspector] MainThemeSC mainthemMuzik;
    [HideInInspector] ArkMakingMNSC cutwoodCtr;
    [HideInInspector] CreditSC creditPnl;
    [HideInInspector] RatingSC ratingPnl;
    [HideInInspector] SceneMN sceneCtr;
    [HideInInspector] GameObject readmePnl;
    [HideInInspector] LeaderSC leaderPnl;
    [HideInInspector] AchievementSC achievePnl;
    [HideInInspector] DataSC data;
    [HideInInspector] PromotionSC promoPNl;
    [HideInInspector] AdsMN adsMN;
    [SerializeField] HomeSC home;

    public string toDay;
    public int gameMode, deviceType, genThemeAllow, genSFXAllow;
    int adsInterCount, targetIntersAdsCount;
    [HideInInspector] public int isBoostWood, isBoostIron, isBoostStone, isBoostFruits, isBoostCrop;
    [HideInInspector] private int countdonwBoostWood, countdownBoostIron, countdownBoostStone, countdownBoostFruits, countdownBoostCrop, isBoostAll;
    void Start() 
    {
        adsInterCount = 0;
        targetIntersAdsCount = 3;
        gameMode = 0;
        CheckDeviceType();
        InitEnviroment();
        OnLoadBoosting();
        Invoke(nameof(ShowRatePnl), 600f);


        countdonwBoostWood = countdownBoostIron = countdownBoostStone = countdownBoostCrop = countdownBoostCrop = 0;
    } 
    void InitEnviroment()
    {
        pausPnl = GameObject.Find("PNL_PausePnl").GetComponent<PauseSC>();
        settingPnl = GameObject.Find("PNL_SettingPnl").GetComponent<SettingSC>();
        playerPnl = GameObject.Find("PNL_PlayerInfo").GetComponent<PlayerInforSC>();
        dailyrewardPnl = GameObject.Find("PNL_DailyReward").GetComponent<DailyRewardSC>();
        shopCtr = GameObject.Find("PNL_Shop").GetComponent<ShopSC>();
        creditPnl = GameObject.Find("PNL_Credit").GetComponent<CreditSC>();
        ratingPnl = GameObject.Find("PNL_Rating").GetComponent<RatingSC>();
        readmePnl = GameObject.Find("PNL_ReadMe");
        leaderPnl = GameObject.Find("PNL_Leaderboard").GetComponent<LeaderSC>();
        achievePnl = GameObject.Find("PNL_Achievement").GetComponent<AchievementSC>();
        promoPNl = GameObject.Find("PNL_Promo").GetComponent<PromotionSC>();
        sfxMuzik = GameObject.Find("OBJ_SoundControl").GetComponent<SoundSC>();
        mainthemMuzik = GameObject.Find("OBJ_SoundControl").GetComponent<MainThemeSC>();
        sceneCtr = GameObject.Find("CAN_GenControl").GetComponent<SceneMN>();
        data = GameObject.Find("CAN_GenControl").GetComponent<DataSC>();
        HideAllPanel();
        
        toDay = DateTime.Today.Day.ToString();
        CheckSoundOnStart();
    }

    #region Handle Panels Visibles
    private void HideAllPanel()
    {
        leaderPnl.gameObject.SetActive(false);
        readmePnl.gameObject.SetActive(false);

        ShowPause(false);
        ShowShop(false);
        ShowInfor(false);
        ShowCredit(false);
        ShowRating(false);
        ShowReward(false);
        ShowAchievement(false);
        ShowLeader(false);
        ShowSetting(false);
        ShowPromo(false);
    }
    public void ShowSetting(bool isShow) => settingPnl.gameObject.SetActive(isShow);
    public void ShowPause(bool isShow) => pausPnl.gameObject.SetActive(isShow);
    public void ShowReward(bool isShow) => dailyrewardPnl.gameObject.SetActive(isShow);
    public void ShowShop(bool isShow) => shopCtr.gameObject.SetActive(isShow);
    public void ShowInfor(bool isShow)
    {
         playerPnl.gameObject.SetActive(isShow);
         ShowLeader(!isShow);
    }
    public void ShowCredit(bool isShow) => creditPnl.gameObject.SetActive(isShow);
    public void ShowRating(bool isShow) => ratingPnl.gameObject.SetActive(isShow);
    public void ShowAchievement(bool isShow) => achievePnl.gameObject.SetActive(isShow);
    public void ShowLeader(bool isShow) => leaderPnl.gameObject.SetActive(isShow);
    public void ShowPromo(bool isShow) => promoPNl.gameObject.SetActive(isShow);
    #endregion

    private void CheckDeviceType()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            deviceType = 1;
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.OSXPlayer)
        {
            deviceType = 2;
        }
    }
    private void CheckSoundOnStart()
    {
        genThemeAllow = data.pTheme;
        genSFXAllow = data.pSFX;
        switch (genThemeAllow)
        {
            case 0:
                mainthemMuzik.MuteTheme();
                break;
            case 1:
                mainthemMuzik.PlayTheme();
                break;
        }

        switch (genSFXAllow)
        {
            case 0:
                sfxMuzik.MuteSFX();
                break;
            case 1:
                sfxMuzik.PlaySFX();
                break;
        }
    }
    public void UpdateUI() => home.HandleHomeUIs();
    
    public void GenLoadScene(sbyte orderScene) 
    {
        gameMode = orderScene;
        sceneCtr.OnLoadScene(orderScene);
    }
    public void AssitsGamemode(int a)
    {
        switch (a) 
        {
            case 0:
                //Home menu
                home = GameObject.Find("CAN_Home").GetComponent<HomeSC>();
                break;
                case 1:
                //Main game Cutwood
                cutwoodCtr = GameObject.Find("CAN_ArkMaking").GetComponent<ArkMakingMNSC>();
                break;
        }
    }


    #region Handle Loose Events
    public void OnToHome()
    {
        gameMode = 0;
        adsInterCount += 1;
        if(adsInterCount >= targetIntersAdsCount)
        {
            adsMN.ShowAds(1);
            sceneCtr.OnLoadScene(1);
            adsInterCount = 0;
        }else if(adsInterCount < targetIntersAdsCount)
        {
            sceneCtr.OnLoadScene(1);
        }
    }
    public void OnExistGame()
    {
        data.OnAutoSaveInfors();
        Application.Quit(0);
    }
    #endregion
    private void ShowRatePnl() => ratingPnl.gameObject.SetActive(true);
    public void OnResumeGame()
    {
        cutwoodCtr.isGameStart = true;
        ShowPause(false);
    }
    public void OnPlayTheme(int state)
    {
        if(state == 0)
        {
            mainthemMuzik.MuteTheme();
        }else if(state == 1)
        {
            mainthemMuzik.PlayTheme();
        }
        data.UpdateThemeState(state);
    }
    public void OnPlaySFX(int state)
    {
        if (state == 0)
        {
            sfxMuzik.MuteSFX();
        }
        else if (state == 1)
        {
            sfxMuzik.PlaySFX();
        }
        data.UpdateSFXState(state);
    }
    public void OnLoadBoosting()
    {
        isBoostWood = data.pBoostWood;
        if(isBoostWood == 1)
        {
            InvokeRepeating(nameof(OnCountDownBonusTimeBoostWoods), 0f, 1f);
        }
        isBoostIron = data.pBoostIron;
        if(isBoostIron == 1)
        {
            InvokeRepeating(nameof(OnCoundownBoostingIron), 0f, 1f);
        }
        isBoostStone = data.pBoostStone;
        if (isBoostIron == 1)
        {
            InvokeRepeating(nameof(OnCountDownBonusTimeBoostStone), 0f, 1f);
        }
        isBoostFruits = data.pBoostFruits;
        if (isBoostIron == 1)
        {
            InvokeRepeating(nameof(OnCountDownBonusTimeBoostFruist), 0f, 1f);
        }
        isBoostCrop = data.pBoostCrop;
        if (isBoostIron == 1)
        {
            InvokeRepeating(nameof(OnCountDownBonusTimeBoostCrop), 0f, 1f);
        }
        if (isBoostAll == 1)
        {
            InvokeRepeating(nameof(OnCountdownBonusAll), 0f, 1f);
        }
    }
    public void OnUpdateBoostingFromShop(int index)
    {
        data.UpdatePlayerBoost(index, 1);
    }

    private void OnCountDownBonusTimeBoostWoods()
    {
        countdonwBoostWood++;
        if(countdonwBoostWood >= 900)
        {
            CancelInvoke(nameof(OnCountDownBonusTimeBoostWoods));
            data.UpdatePlayerBoost(0, 0);
            OnLoadBoosting();
            countdonwBoostWood = 0;
        }
    }
    private void OnCoundownBoostingIron()
    {
        countdownBoostIron++;
        if (countdownBoostIron >= 900)
        {
            CancelInvoke(nameof(OnCoundownBoostingIron));
            data.UpdatePlayerBoost(1, 0);
            OnLoadBoosting();
            countdownBoostIron = 0;
        }
    }
    private void OnCountDownBonusTimeBoostStone()
    {
        countdownBoostStone++;
        if (countdownBoostStone >= 900)
        {
            CancelInvoke(nameof(OnCountDownBonusTimeBoostStone));
            data.UpdatePlayerBoost(2, 0);
            OnLoadBoosting();
            countdownBoostStone = 0;
        }
    }
    private void OnCountDownBonusTimeBoostFruist()
    {
        countdownBoostFruits++;
        if (countdownBoostIron >= 900)
        {
            CancelInvoke(nameof(OnCountDownBonusTimeBoostFruist));
            data.UpdatePlayerBoost(3, 0);
            OnLoadBoosting();
            countdownBoostFruits = 0;
        }
    }
    private void OnCountDownBonusTimeBoostCrop()
    {
        countdownBoostIron++;
        if (countdownBoostIron >= 900)
        {
            CancelInvoke(nameof(OnCountDownBonusTimeBoostCrop));
            data.UpdatePlayerBoost(4, 0);
            OnLoadBoosting();
            countdownBoostCrop = 0;
        }
    }
    private void OnCountdownBonusAll()
    {
        countdonwBoostWood++;
        countdownBoostIron++;
        countdownBoostStone++;
        countdownBoostFruits++;
        countdownBoostCrop++;
        if(countdonwBoostWood >= 300)
        {
            CancelInvoke(nameof(OnCountdownBonusAll));
            data.UpdatePlayerBoost(5, 0);
            OnLoadBoosting();
            countdonwBoostWood = countdownBoostIron = countdownBoostStone = countdownBoostCrop = countdownBoostCrop = 0;
        }
    }
}
