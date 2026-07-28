using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NoahSC : MonoBehaviour, IDamageablePlayer
{
    [HideInInspector] DataSC pData;
    [HideInInspector] Joystick joystickCtr;
    [HideInInspector] GeneralContrlSC genCtr;
    [HideInInspector] ArkMakingMNSC cutwoodMn;
    [SerializeField] GameObject weapon;
    [SerializeField] PlayerAtkSC playerAtk; //editor assigned
    [SerializeField] WeapSysSC weapCtr; //editor assigned
    //Gameplay Attributes
    private int deviceType;
    private bool isPause;
    public bool isAllowoMove;

    //Player attribute
    private int noahDir;
    private float moveSpd;
    private int playerHP, playerXP;
    Vector3 curTargetPos, weapOriginPos;
    Vector3 curPlayerPos;
    void Start()
    {
        moveSpd = 3f;
        noahDir = 0;
        genCtr = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        pData = GameObject.Find("CAN_GenControl").GetComponent<DataSC>();
        joystickCtr = GameObject.Find("IMG_JoystickHandle").GetComponent<Joystick>();
        cutwoodMn = GameObject.Find("GameplayMN").GetComponent<ArkMakingMNSC>();
        deviceType = genCtr.deviceType;
        curTargetPos = Vector3.zero;
        isAllowoMove = true;
        playerHP = pData.pHP;
        playerXP = pData.pXP;
    }
    void Update()
    {
        if(isAllowoMove == true)
        {
            if (deviceType == 1)
            {
                OnMoveActionKey();
            }
            else if (deviceType == 2)
            {
                OnMoveByTouch();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Logs") { cutwoodMn.OnIncreaseWoods(); }
        else if(collision.gameObject.tag == "Iron") { cutwoodMn.OnIncreaseIron(); }
        else if (collision.gameObject.tag == "Stone") { cutwoodMn.OnIncreaseStone(); }
        else if (collision.gameObject.tag == "Fruits") { cutwoodMn.OnInCreaseFruits(); }
        else if(collision.gameObject.tag == "Foods") { cutwoodMn.OnIncreaseFood(); }
        else if (collision.gameObject.tag == "Wheat") { cutwoodMn.OnIncreaseCrop(); }
        else if (collision.gameObject.tag == "Coin") { cutwoodMn.OnInCreaseMoney(); }
        else if(collision.gameObject.tag == "XP")
        {
            OnGainXP();
        }
        else if (collision.gameObject.tag == "Build_Pos") 
        {
            cutwoodMn.OnShowBuildOption(); 
        }
    }

    private void OnMoveActionKey()
    {
        if(Input.GetKey(KeyCode.W) == true)
        {
            if (curPlayerPos.y <= 5f)
            {
                transform.position += Vector3.up * Time.deltaTime * moveSpd;
                curPlayerPos = gameObject.transform.position;
            }
        }
        else if(Input.GetKey(KeyCode.S) == true) 
        {
            if (curPlayerPos.y >= -5f)
            {
                transform.position += Vector3.down * Time.deltaTime * moveSpd;
                curPlayerPos = gameObject.transform.position;
            }
        }
        else if(Input.GetKey(KeyCode.A) == true)
        {
            if (noahDir == 0) { ChangeDir(); }
            if(curPlayerPos.x >= -5f)
            {
                transform.position += Vector3.left * Time.deltaTime * moveSpd;
                curPlayerPos = gameObject.transform.position;
            }

        }
        else if(Input.GetKey(KeyCode.D) == true)
        {
            if (noahDir == 1) { ChangeDir(); }
            if(curPlayerPos.x <= 5f)
            {
                transform.position += Vector3.right * Time.deltaTime * moveSpd;
                curPlayerPos = gameObject.transform.position;
            }
        }
    }
    private void OnMoveByTouch()
    {
        float horizontal = joystickCtr.Horizontal();
        float vertical = joystickCtr.Vertical();

        if(horizontal >= 0)
        {
            if(noahDir == 1)
            {
                ChangeDir();
            }
        }else if(horizontal < 0)
        {
            if (noahDir == 0)
            {
                ChangeDir();
            }
        }

        Vector3 direction = new Vector3(horizontal, vertical,0).normalized;
        transform.Translate(direction * Time.deltaTime * moveSpd);
        curPlayerPos = gameObject.transform.position;
    }
    public void ChangeDir()
    {
        if (noahDir == 0)
        {
            noahDir = 1;
            float prefabCurentScale = gameObject.transform.localScale.y;
            gameObject.transform.localScale = new Vector3(prefabCurentScale, prefabCurentScale, prefabCurentScale);
        }
        else if (noahDir == 1)
        {
            noahDir = 0;
            float prefabCurentScale = gameObject.transform.localScale.y;
            gameObject.transform.localScale = new Vector3(-prefabCurentScale, prefabCurentScale, prefabCurentScale);
        }
    }
    public void  OnAttack()
    {
        playerAtk.OnHandleAtack();
    }
    public void OnTakeDamage()
    {
        playerHP--;
        cutwoodMn.OnHandleHP(playerHP);
        if (playerHP <= 0) { cutwoodMn.OnRunOutHP(); }
    }
    public void OnGainXP()
    {
        playerXP++;
        cutwoodMn.OnHandleXP(playerXP);
    }
    public void SwitchWeapPlayer() { weapCtr.HandleWitchWeap(); }
}