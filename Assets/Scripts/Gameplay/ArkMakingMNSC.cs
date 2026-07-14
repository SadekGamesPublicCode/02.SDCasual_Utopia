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
    [SerializeField] List<GameObject> animalPreyToSpawn = new List<GameObject>();
    [SerializeField] List<GameObject> animalPredatorToSpawn = new List<GameObject>();
    [SerializeField] NoahSC player;
    [SerializeField] GameObject treeA, treeB, ironMine, stoneMine;
    [SerializeField] GameObject buildOptionPnl;
    [HideInInspector] public int deviceType;

    //Gameplay Control variables
    [HideInInspector]
    public bool isGameStart; //Use for detech game start and pauise even
    private int woodAmount, ironAmount, stoneAmount, fruistAmount, cropAmount, moneyAmount;
    public int treeOnScreenCapacity, preyOnScreenCap, pretadorOnScreenCap;
    [HideInInspector] public int curTreeOnScreen;
    public int randTreeToSpawn, curPreyOnScreen, curPredatorOnScreen;

    //Collect Item Mode
    void Start()
    {
        genCtrl = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        camFollow = GameObject.Find("CAM_Follow").GetComponent<CameraFollowPlayerSC>();
        dataCtr = GameObject.Find("CAN_GenControl").GetComponent<DataSC>();
        isGameStart = false;
        deviceType = genCtrl.deviceType;
        curTreeOnScreen = 0;
        treeOnScreenCapacity = 100;
        preyOnScreenCap = 30;
        pretadorOnScreenCap = 10;
        genCtrl.AssitsGamemode(1);
        GetPlayerDatas();
        OnInitMap();
        SpawnIronMine();
        SpawnStoneMine();
    }

    private void GetPlayerDatas()
    {
        woodAmount = dataCtr.pWoods;
        ironAmount = dataCtr.pIron;
        stoneAmount = dataCtr.pStone;
        fruistAmount = dataCtr.pFruits;
        cropAmount = dataCtr.pCrop;
        moneyAmount = dataCtr.pCoin;

        OnHandleUIs();
    }

    #region Handle Gameplay
    private void OnInitMap()
    {
        player = Instantiate(player, Vector3.zero, Quaternion.identity);
        camFollow.AssistCamFollowCutWood(player);
        isGameStart = true;
        LoadTownOnPlay(); //Load town
        InvokeRepeating(nameof(SpawnTree), 0f, 4);
        InvokeRepeating(nameof(OnHandleInitAnimal), 0f, 10f);
    }
    private void OnHandleInitAnimal()
    {
        int randAnimalInitChance;
        randAnimalInitChance = Random.Range(1, 100);
        if(randAnimalInitChance < 70)
        {
            //Spawn prey
            int randAnimalToSpawn;
            randAnimalToSpawn = Random.Range(0, animalPreyToSpawn.Count);
            Vector3 randPos;
            randPos.x = Random.Range(player.transform.position.x - 5, player.transform.position.x + 5);
            randPos.y = Random.Range(player.transform.position.y - 5, player.transform.position.y + 5);
            if(curPreyOnScreen <= preyOnScreenCap)
            {
                curPreyOnScreen++;
                Instantiate(animalPreyToSpawn[randAnimalToSpawn], new Vector3(randPos.x, randPos.y, 0), Quaternion.identity);
            }
        }
        else if(randAnimalInitChance >= 70)
        {
            //Spawn Predator
            int randAnimalToSpawn;
            randAnimalToSpawn = Random.Range(0, animalPredatorToSpawn.Count);
            Vector3 randPos;
            randPos.x = Random.Range(player.transform.position.x - 5, player.transform.position.x + 5);
            randPos.y = Random.Range(player.transform.position.y - 5, player.transform.position.y + 5);
            if (curPreyOnScreen <= preyOnScreenCap)
            {
                curPreyOnScreen++;
                Instantiate(animalPredatorToSpawn[randAnimalToSpawn], new Vector3(randPos.x, randPos.y, 0), Quaternion.identity);
            }
        }
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
                    randPos.x = Random.Range(player.transform.position.x - 5, player.transform.position.x + 5);
                }while(randPos.x > -5 && randPos.x < 5);
                do
                {
                    randPos.y = Random.Range(player.transform.position.y - 5, player.transform.position.y + 5);
                } while (randPos.y > -5 && randPos.y < 5);
                curTreeOnScreen++;
                if (randTreeToSpawn == 0) Instantiate(treeA, new Vector3(randPos.x , randPos.y, 0), Quaternion.identity);
                else if (randTreeToSpawn != 0) Instantiate(treeB, new Vector3(randPos.x, randPos.y, 0), Quaternion.identity);

            }
        }
    }
    private void SpawnIronMine()
    {
        float randPosX, randPosY;
        do
        {
            randPosX = Random.Range(-60, 60);
            
        } while (randPosX > -10 && randPosX < 10);
        do
        {
            randPosY = Random.Range(-60, 60);
        }while(randPosY > -10 && randPosY < 10);

        Instantiate(ironMine, new Vector3(randPosX, randPosY, 0), Quaternion.identity);
    }
    private void SpawnStoneMine()
    {
        float randPosX, randPosY;
        do
        {
            randPosX = Random.Range(-32, 32);
        } while (randPosX > -10 && randPosX < 10);
        do
        {
            randPosY = Random.Range(-32, 32);
        } while (randPosY > -10 && randPosY < 10);

        Instantiate(stoneMine, new Vector3(randPosX, randPosY, 0), Quaternion.identity);
    }
    #endregion

    #region Handle UI events
    private void OnHandleUIs()
    {
        woodsAmountTxt.text = woodAmount.ToString();
        ironAmountsTxt.text = ironAmount.ToString();
        stoneAmountsTxt.text = stoneAmount.ToString();
        cropAmountsTxt.text = cropAmount.ToString();
        fruitAmountsTxt.text = fruistAmount.ToString();
        moneyAmountsTxt.text = moneyAmount.ToString();
    }
    public void OnShowBuildOption() { OnVisibleBuildOption(true); }
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
        buildOptionPnl.gameObject.SetActive(isShow);
        insuffencingResourcetxt.gameObject.SetActive(false);
        if (isShow == false)
        {
            player.isAllowoMove = true;
        }
    }
    private void OnDisableText()
    {
        insuffencingResourcetxt.gameObject.SetActive(false);
    }
    private void LoadTownOnPlay()
    {
        //Load town from JSON
    }
}
