using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArkMakingMNSC : MonoBehaviour
{
    [HideInInspector] GeneralContrlSC genCtrl;
    [HideInInspector] CameraFollowPlayerSC camFollow;
    [HideInInspector] DataSC dataCtr;

    [SerializeField] Text woodsAmountTxt, ironAmountsTxt, stoneAmountsTxt, fruitAmountsTxt, cropAmountsTxt, moneyAmountsTxt;
    [SerializeField] Text insuffencingResourcetxt;
    [SerializeField] NoahSC player;
    [SerializeField] GameObject treeA, treeB, ironMine, stoneMine;
    [SerializeField] GameObject actionPnl;
    [SerializeField] Slider playerHPSlide, playerExpSlide;
    [HideInInspector] public int deviceType;
    [HideInInspector] public int curTreeOnScreen;

    //Gameplay Control variables
    [HideInInspector]
    public bool isGameStart; //Use for detech game start and pauise even
    private int woodAmount, ironAmount, stoneAmount, fruistAmount, cropAmount, moneyAmount;
    public int treeOnScreenCapacity, randTreeToSpawn;
    [HideInInspector] public int playerHP, playerXp, playerTargetXP, playerHPFull, playerLevel;
    private int playtimeCount;
    void Start()
    {
        genCtrl = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        camFollow = GameObject.Find("CAM_Follow").GetComponent<CameraFollowPlayerSC>();
        dataCtr = GameObject.Find("CAN_GenControl").GetComponent<DataSC>();
        isGameStart = false;
        deviceType = genCtrl.deviceType;
        curTreeOnScreen = 0;
        treeOnScreenCapacity = 100;
        playtimeCount = 0;
        genCtrl.AssitsGamemode(1);
        GetPlayerDatas();
        OnInitMap();
        SpawnIronMine();
        SpawnStoneMine();
        InvokeRepeating(nameof(CountToSpawnEnemy), 0f, 1f);
    }

    private void GetPlayerDatas()
    {
        woodAmount = dataCtr.pWoods;
        ironAmount = dataCtr.pIron;
        stoneAmount = dataCtr.pStone;
        fruistAmount = dataCtr.pFruits;
        cropAmount = dataCtr.pCrop;
        moneyAmount = dataCtr.pCoin;

        playerHP = dataCtr.pHP;
        playerXp = dataCtr.pXP;
        playerLevel = dataCtr.pLv;
        playerHPFull = 100;
        playerTargetXP = playerLevel * 100;

        OnHandleUIs();
    }

    #region Handle Controle Gameplay
    private void OnInitMap()
    {
        player = Instantiate(player,new Vector3(1,1,0), Quaternion.identity);
        camFollow.AssistCamFollowCutWood(player);
        isGameStart = true;
        LoadTownOnPlay(); //Load town
        InvokeRepeating(nameof(SpawnTree), 0f, 4);
        InvokeRepeating(nameof(SpawnEnemies), 0f, 20f);
    }
    private void SpawnTree()
    {
        if(isGameStart == true)
        {
            if(curTreeOnScreen <= treeOnScreenCapacity)
            {
                randTreeToSpawn = Random.Range(0, 2);
                Vector3 randPos;
                do
                {
                    randPos.x = Random.Range(player.transform.position.x - 1, player.transform.position.x + 1);
                }while(randPos.x > -1 && randPos.x < 1);
                do
                {
                    randPos.y = Random.Range(player.transform.position.y - 1, player.transform.position.y + 1);
                } while (randPos.y > -1 && randPos.y < 1);
                curTreeOnScreen++;
                if (randTreeToSpawn == 0) Instantiate(treeA, new Vector3(randPos.x , randPos.y, 0), Quaternion.identity);
                else if (randTreeToSpawn != 0) Instantiate(treeB, new Vector3(randPos.x, randPos.y, 0), Quaternion.identity);

            }
        }
    }
    private void SpawnEnemies()
    {
        if(playtimeCount >= 300)
        {
            //Spawn Enemies
        }
    }
    private void SpawnIronMine()
    {
        float randPosX, randPosY;
        do
        {
            randPosX = Random.Range(-5, 5);
            
        } while (randPosX > -1 && randPosX < 1);
        do
        {
            randPosY = Random.Range(-5, 5);
        }while(randPosY > -1 && randPosY < 1);

        Instantiate(ironMine, new Vector3(randPosX, randPosY, 0), Quaternion.identity);
    }
    private void SpawnStoneMine()
    {
        float randPosX, randPosY;
        do
        {
            randPosX = Random.Range(-5, 5);
        } while (randPosX > -1  && randPosX < 1);
        do
        {
            randPosY = Random.Range(-5, 5);
        } while (randPosY > -1 && randPosY < 1);

        Instantiate(stoneMine, new Vector3(randPosX, randPosY, 0), Quaternion.identity);
    }
    #endregion

    #region Handle UI events
    private void OnHandleUIs()
    {
        //Handle what UI show on screen
        woodsAmountTxt.text = woodAmount.ToString();
        ironAmountsTxt.text = ironAmount.ToString();
        stoneAmountsTxt.text = stoneAmount.ToString();
        cropAmountsTxt.text = cropAmount.ToString();
        fruitAmountsTxt.text = fruistAmount.ToString();
        moneyAmountsTxt.text = moneyAmount.ToString();

        playerExpSlide.maxValue = playerTargetXP;
        playerHPSlide.maxValue = playerHPFull;
        playerHPSlide.value = playerHP;
        playerExpSlide.value = playerXp;
    }
    public void OnShowBuildOption() { OnVisibleBuildOption(true); }
    private void CountToSpawnEnemy()
    {
        playtimeCount++;
        if(playtimeCount >= 300)
        {
            CancelInvoke();
        }
    }
    public void OnPause()
    {
        //Handle pause game
        isGameStart = false;
        genCtrl.ShowPause(true);
    }
    public void OnAttack()
    {
        player.OnAttack();
    }
    public void OnIncreaseWoods()
    {
        woodAmount++;
        dataCtr.UpdateWoods(woodAmount);
        woodAmount = dataCtr.pWoods;
        OnHandleUIs();
    }
    public void OnIncreaseFood()
    {
        fruistAmount++;
        dataCtr.UpdateFruist(fruistAmount);
        fruistAmount = dataCtr.pFruits;
        OnHandleUIs();
    }
    public void OnIncreaseIron()
    {
        ironAmount++;
        dataCtr.UpdateIron(ironAmount);
        ironAmount = dataCtr.pIron;
        OnHandleUIs();
    }
    public void OnIncreaseStone()
    {
        stoneAmount++;
        dataCtr.UpdateStone(stoneAmount);
        stoneAmount = dataCtr.pStone;
        OnHandleUIs();
    }
    public void OnInCreaseFruits()
    {
        fruistAmount++;
        dataCtr.UpdateFruist(fruistAmount);
        fruistAmount = dataCtr.pFruits;
        OnHandleUIs();
    }
    public void OnIncreaseCrop()
    {
        cropAmount++;
        dataCtr.UpdateCrop(cropAmount);
        cropAmount = dataCtr.pCrop;
        OnHandleUIs();
    }
    public void OnInCreaseMoney()
    {
        moneyAmount++;
        dataCtr.UpdateTotalScore(moneyAmount);
        moneyAmount = dataCtr.pCoin;
        OnHandleUIs();
    }
    #endregion

    public void OnBuildStructure(int structureIndex)
    {
        switch (structureIndex)
        {
            case 0:
                //House
                if (IsAllowBUy(200, 0, 0, 0) == true)
                {
                    
                    //Spawn house in pos
                    //Update resource
                    //Update amount of struct in JSON
                }
                else Invoke(nameof(OnDisableText), 3);
                break;
                case 1:
                //Market
                if (IsAllowBUy(200, 50, 100, 0) == true)
                {
                    //Spawn house in pos
                    //Update resource
                    //Update amount of struct in JSON
                }
                else Invoke(nameof(OnDisableText), 3);
                break;
                case 2:
                //Orchard
                if (IsAllowBUy(100, 0, 200, 0) == true)
                {
                    //Spawn house in pos
                    //Update resource
                    //Update amount of struct in JSON
                }
                else Invoke(nameof(OnDisableText), 3);
                break;
            case 3:
                //Wheat Farm
                if (IsAllowBUy(100, 0, 0, 0) == true)
                {
                    //Spawn house in pos
                    //Update resource
                    //Update amount of struct in JSON
                }
                else Invoke(nameof(OnDisableText), 3);
                break;
            case 4:
                //Def Tower
                if (IsAllowBUy(100, 200, 300, 100) == true)
                {
                    //Spawn house in pos
                    //Update resource
                    //Update amount of struct in JSON
                }
                else Invoke(nameof(OnDisableText), 3);
                break;
        }
    }
    private bool IsAllowBUy(int woodPrice, int ironPrice, int rockPrice, int moneyPrice)
    {
        if (woodPrice <= woodAmount)
        {
            if (ironPrice <= ironAmount)
            {
                if (rockPrice <= stoneAmount)
                {
                    if (moneyPrice <= moneyAmount)
                    {
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }
        else return false;
    }
    public void OnVisibleBuildOption(bool isShow)
    {
        actionPnl.gameObject.SetActive(isShow);
        insuffencingResourcetxt.gameObject.SetActive(false);
    }
    private void OnDisableText()
    {
        insuffencingResourcetxt.gameObject.SetActive(false);
    }
    private void LoadTownOnPlay()
    {
        //Load town from JSON
    }
    public void OnHandleXP(int value) 
    {
        dataCtr.UpdatePlayerStat(1, value);
        //Handle Slider XP Bar
        playerExpSlide.value = value;
        if(value >= playerLevel * 100)
        {
            playerLevel++;
            dataCtr.UpdatePlayerStat(2, playerLevel);
        }
    }
    public void OnHandleHP(int value)
    {
        dataCtr.UpdatePlayerStat(0, value);
        //Handle slider HP
        playerHPSlide.value = value;
    }
    public void OnRunOutHP()
    {
        genCtrl.ShowLoose(true);
    }
    public void OnRefillHP()
    {
        playerHP = playerHPFull;
        OnHandleHP(playerHP);
    }
    public void OnSwitchWeap()
    {
        player.SwitchWeapPlayer();
    }
}
