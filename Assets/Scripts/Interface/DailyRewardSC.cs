using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DailyRewardSC : MonoBehaviour
{
    [HideInInspector] DataSC data;
    [HideInInspector] GeneralContrlSC genCtr;
    [SerializeField] List<Button> rewardBtn = new List<Button>();

    private const string LastPatrolTimeKey = "LastPatrolTime";
    private const string PatrolStreakKey = "PatrolStreak";
    public int baseReward = 10; // example reward, x2 for each time count
    public int rewardToGive;
    private bool isAllowDailyClaim;
    private int streakDaily;
    private string lastCollectDay;
    void Start()
    {
        genCtr = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        data = GameObject.Find("CAN_GenControl").GetComponent<DataSC>();

        isAllowDailyClaim = false;
        streakDaily = data.pDailyStreak;
        lastCollectDay = "";
        rewardToGive = 0;
        ShowRewardDaily();
        OnCheckDailyClaimOnInit();
    }
    public void OnClosePanel() => genCtr.UpdateUI();

    #region Handle Claim Daily
    void ShowRewardDaily()
    {
        print(data.pAllowClaimDaily);
        if (data.pLastDailyClaim == "")
        {
            print("1");
            //First day of play
            isAllowDailyClaim = true;
            for (int i = 0; i < rewardBtn.Count; i++)
            {
                rewardBtn[i].GetComponent<Button>().interactable = false;
            }
            rewardBtn[0].GetComponent<Button>().interactable = true;
        }
        else
        {
            if (genCtr.toDay != data.pLastDailyClaim)
            {
                print("2");
                //New day access + unclaimed
                for (int i = 0; i < rewardBtn.Count; i++)
                {
                    rewardBtn[streakDaily].GetComponent<Button>().interactable = false;
                }

                if (streakDaily >= 1 && streakDaily < 8)
                {
                    print("3");
                    //Lock previous day claim buttons
                    for (int i = 0; i < streakDaily; i++)
                    {
                        rewardBtn[i].GetComponent<Button>().interactable = false;
                    }

                    for (int j = streakDaily + 1; j > rewardBtn.Count; j++)
                    {
                        rewardBtn[j].GetComponent<Button>().interactable = false;
                    }
                    rewardBtn[streakDaily].GetComponent<Button>().interactable = true;

                    isAllowDailyClaim = false;
                }
            }
            else if (genCtr.toDay == data.pLastDailyClaim)
            {
                print("4");
                if (data.pAllowClaimDaily == 1)
                {
                    print("5");
                    //Same day access + claimed
                    isAllowDailyClaim = false;
                    for (int i = 0; i < rewardBtn.Count; i++)
                    {
                        rewardBtn[i].GetComponent<Button>().interactable = false;
                    }
                }
                else if (data.pAllowClaimDaily == 0)
                {
                    print("6");
                    //Same day, unclaimed
                    isAllowDailyClaim = true;
                    for (int i = 0; i < rewardBtn.Count; i++)
                    {
                        rewardBtn[i].GetComponent<Button>().interactable = false;
                    }
                    rewardBtn[streakDaily].GetComponent<Button>().interactable = true; //Enable only able-to-claim button
                }
            }
        }
    }
    public void OnClaimDaily()
    {
        int tempFinalScoreToOverride;
        SelectRewardDaily();
        rewardBtn[streakDaily].GetComponent<Button>().interactable = false;
        lastCollectDay = DateTime.Today.Day.ToString();
        isAllowDailyClaim = false;
        tempFinalScoreToOverride = rewardToGive;
        streakDaily++;
        data.UpdateAllowClaimDaily(1);

        print("tempFinalScoreToOverride = " + tempFinalScoreToOverride);

        data.UpdateTotalScore(tempFinalScoreToOverride); // Update score
        data.UpdateStreak(streakDaily); //Update streak
        data.UpdatePatrolDailyReward(lastCollectDay); //Update last collect day
        ShowRewardDaily();
        genCtr.UpdateUI();
    }
    private void SelectRewardDaily()
    {
        switch (streakDaily)
        {
            case 0:
                rewardToGive = 10;
                break;
            case 1:
                rewardToGive = 20;
                break;
            case 2:
                rewardToGive = 40;
                break;
            case 3:
                rewardToGive = 80;
                break;
            case 4:
                rewardToGive = 160;
                break;
            case 5:
                rewardToGive = 320;
                break;
            case 6:
                rewardToGive = 640;
                break;
            case 7:
                rewardToGive = 1280;
                break;
            case 8:
                rewardToGive = 2048;
                OnResetStreak();
                break;
        }
    }
    private void OnResetStreak()
    {
        //Reset streak
        data.UpdateStreak(0); //Update streak
        data.UpdatePatrolDailyReward(""); //Update last collect day
    }
    #endregion
    private void OnCheckDailyClaimOnInit()
    {
        if (genCtr.toDay != data.pLastDailyClaim)
        {
            data.UpdateAllowClaimDaily(0);
            isAllowDailyClaim = true;
        }
        else
        {
            isAllowDailyClaim = false;
        }
    }
}
