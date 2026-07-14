using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSC : MonoBehaviour
{
    //[HideInInspector] PlayerInforSC infor;

    public string deviceID;
    public bool isFirstPlay;

    //Player's data variables
    [HideInInspector] public string pName;
    [HideInInspector] public int pCoin, pGems;
    [HideInInspector] public int pWoods, pIron, pStone, pCrop, pFruits;
    [HideInInspector] public int pSFX, pTheme;

    //Dialy Patrol
    [HideInInspector] public int pAllowClaimDaily, pDailyStreak;
    [HideInInspector] public string pLastDailyClaim;

    //In-game boosting
    [HideInInspector] public int pBoostWood, pBoostIron, pBoostStone, pBoostFruits, pBoostCrop, pBoostCoin;

    [SerializeField] SaveSystem saveSys;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        SettingStart();
    }
    void Start() { }
    void Update() { }

    #region Local Handle
    private void SettingStart()
    {
        deviceID = SystemInfo.deviceUniqueIdentifier;
        if (CheckFirstPlay() == true)
        {
            SetNewPlayer();
        }
        else if (CheckFirstPlay() == false)
        {
            LoadOldPlayer();
        }
        //infor = GameObject.Find("PNL_PlayerInfo").GetComponent<PlayerInforSC>();
    }

    #endregion

    #region Player Data Handle
    private void SetNewPlayer()
    {
        Debug.Log("in new layer");
        string subStringID = deviceID.Substring(8, 12).ToString();
        string nameFistPlay = "Player" + subStringID;
        PlayerPrefs.SetInt("HasPlayed", 1);
        PlayerPrefs.SetString("PlayerName", nameFistPlay);

        PlayerPrefs.SetInt("TotalCoin", 0); //Actual player in-game currency
        PlayerPrefs.SetInt("TotalGems", 0); //Player's PIA currency

        PlayerPrefs.SetInt("soundState", 1);
        PlayerPrefs.SetInt("sfxState", 1);


        //Patrol Reward
        PlayerPrefs.SetInt("AllowClaimDaily", 0);
        PlayerPrefs.SetString("LastPatrolDailyTime", "");
        PlayerPrefs.SetInt("PatrolDailyStreak", 0);

        //In-game Boosting
        PlayerPrefs.SetInt("BoostWood", 0);
        PlayerPrefs.SetInt("BoostIron", 0);
        PlayerPrefs.SetInt("BoostStone", 0);
        PlayerPrefs.SetInt("BoostFruits", 0);
        PlayerPrefs.SetInt("BoostCrop", 0);
        PlayerPrefs.SetInt("BoostCoin", 0);

        Invoke("LoadOldPlayer", 3f); //De tam thoi
    }
    private void LoadOldPlayer()
    {
        pName = PlayerPrefs.GetString("PlayerName");
        pGems = PlayerPrefs.GetInt("TotalGems");
        pCoin = PlayerPrefs.GetInt("TotalCoin");
        pTheme = PlayerPrefs.GetInt("soundState");
        pSFX = PlayerPrefs.GetInt("sfxState");

        //Patrol Reward
        pLastDailyClaim = PlayerPrefs.GetString("LastPatrolDailyTime");
        pAllowClaimDaily = PlayerPrefs.GetInt("AllowClaimDaily");
        pDailyStreak = PlayerPrefs.GetInt("PatrolDailyStreak");

        //In-game boosting
        pBoostWood = PlayerPrefs.GetInt("BoostWood");
        pBoostIron = PlayerPrefs.GetInt("BoostIron");
        pBoostStone = PlayerPrefs.GetInt("BoostStone");
        pBoostFruits = PlayerPrefs.GetInt("BoostFruits");
        pBoostCrop = PlayerPrefs.GetInt("BoostCrop");
        pBoostCoin = PlayerPrefs.GetInt("BoostCoin");
    }
    public void DataDelete()
    {
        PlayerPrefs.DeleteAll();
        OnResetJSON();
        SetNewPlayer();
        //infor.GetPlayerData();
    }
    public void UploadPlayerData()
    {

    }
    #endregion

    #region Data Update
    public void UpdatePName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
        pName = PlayerPrefs.GetString("PlayerName");
    }
    public void UpdateTotalScore(int currency)
    {
        PlayerPrefs.SetInt("TotalCoin", currency);
        pCoin = PlayerPrefs.GetInt("TotalCoin");
    }
    public void UpdateTotalGem(int gems)
    {
        PlayerPrefs.SetInt("TotalGems", gems);
        pGems = PlayerPrefs.GetInt("TotalGems");
    }
    public void UpdateSFXState(int sfxState)
    {
        PlayerPrefs.SetInt("sfxState", sfxState);
        pSFX = PlayerPrefs.GetInt("sfxState");
    }
    public void UpdateThemeState(int thameState)
    {
        PlayerPrefs.SetInt("soundState", thameState);
        pTheme = PlayerPrefs.GetInt("soundState");
    }

    //Daily Reward
    public void UpdatePatrolDailyReward(string lastPatrolDaily)
    {
        PlayerPrefs.SetString("LastPatrolDailyTime", lastPatrolDaily);
        pLastDailyClaim = PlayerPrefs.GetString("LastPatrolDailyTime");
    }
    public void UpdateAllowClaimDaily(int state)
    {
        PlayerPrefs.SetInt("AllowClaimDaily", state);
        pAllowClaimDaily = PlayerPrefs.GetInt("AllowClaimDaily");
    }
    public void UpdateStreak(int value)
    {
        PlayerPrefs.SetInt("PatrolDailyStreak", value);
        pDailyStreak = PlayerPrefs.GetInt("PatrolDailyStreak");
    }
    #endregion

    #region Update Ingame Stats
    public void UpdateWoods(int value)
    {
        pWoods = value;
        int[] a = { pWoods, pIron, pStone, pFruits, pCrop, pCoin };
        saveSys.OnSaveResourceData(a);
    }
    public void UpdateIron(int value)
    {
        pIron = value;
        int[] a = { pWoods, pIron, pStone, pFruits, pCrop, pCoin };
        saveSys.OnSaveResourceData(a);
    }
    public void UpdateStone(int value)
    {
        pStone = value;
        int[] a = { pWoods, pIron, pStone, pFruits, pCrop, pCoin };
        saveSys.OnSaveResourceData(a);
    }
    public void UpdateFruist(int value)
    {
        pFruits = value;
        int[] a = { pWoods, pIron, pStone, pFruits, pCrop, pCoin };
        saveSys.OnSaveResourceData(a);
    }
    public void UpdateCrop(int value)
    {
        pCrop = value;
        int[] a = { pWoods, pIron, pStone, pFruits, pCrop, pCoin};
        saveSys.OnSaveResourceData(a);
    }

    #endregion
    public void UpdatePlayerBoost(int indexs, int value)
    {
        switch (indexs)
        {
            case 0:
                PlayerPrefs.SetInt("BoostWood", value);
                pBoostWood = PlayerPrefs.GetInt("BoostWood");
                break;
            case 1:
                PlayerPrefs.SetInt("BoostIron", value);
                pBoostIron = PlayerPrefs.GetInt("BoostIron");
                break;
            case 2:
                PlayerPrefs.SetInt("BoostStone", value);
                pBoostStone = PlayerPrefs.GetInt("BoostStone");
                break;
            case 3:
                PlayerPrefs.SetInt("BoostFruits", value);
                pBoostFruits = PlayerPrefs.GetInt("BoostFruits");
                break;
            case 4:
                PlayerPrefs.SetInt("BoostCrop", value);
                pBoostCrop = PlayerPrefs.GetInt("BoostCrop");
                break;
            case 5:
                PlayerPrefs.SetInt("BoostWood", value);
                pBoostWood = PlayerPrefs.GetInt("BoostWood");
                PlayerPrefs.SetInt("BoostIron", value);
                pBoostIron = PlayerPrefs.GetInt("BoostIron");
                PlayerPrefs.SetInt("BoostStone", value);
                pBoostStone = PlayerPrefs.GetInt("BoostStone");
                PlayerPrefs.SetInt("BoostFruits", value);
                pBoostFruits = PlayerPrefs.GetInt("BoostFruits");
                PlayerPrefs.SetInt("BoostCrop", value);
                pBoostCrop = PlayerPrefs.GetInt("BoostCrop");
                PlayerPrefs.SetInt("BoostCoin", value);
                pBoostCoin = PlayerPrefs.GetInt("BoostCoin");
                break;
        }
    }

    #region Checking Zone
    private bool CheckFirstPlay()
    {
        //print("FirstPlay: " + PlayerPrefs.GetInt("HasPlayed"));
        if (PlayerPrefs.GetInt("HasPlayed") == 1)
        {
            return isFirstPlay = false;
        }
        else return true;
    }
    private bool CheckNewDay()
    {
        return true;
    }
    #endregion

    #region JSON HANDLERS
    public void OnLoadInventory()
    {
        InventoryData inventLoad = saveSys.Load();

        pWoods = inventLoad.resourceDatas[0];
        pIron = inventLoad.resourceDatas[1];
        pStone = inventLoad.resourceDatas[2];
        pFruits = inventLoad.resourceDatas[3];
        pCrop = inventLoad.resourceDatas[4];
        pCoin = PlayerPrefs.GetInt("TotalCoin");
    }
    public void OnAutoSaveInfors()
    {
        int[] a = { pWoods, pIron, pStone, pFruits, pCrop};
        saveSys.OnSaveResourceData(a);
    }
    private void OnResetJSON() { saveSys.OnReset(); }
    #endregion
}
