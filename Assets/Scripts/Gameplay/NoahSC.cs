using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NoahSC : MonoBehaviour
{
    [HideInInspector] Joystick joystickCtr;
    [HideInInspector] GeneralContrlSC genCtr;
    [HideInInspector] ArkMakingMNSC cutwoodMn;
    [SerializeField] GameObject weapon, curTarget;
    //Gameplay Attributes
    private int deviceType;
    private bool isPause;
    public bool isAllowoMove;

    //Player attribute
    private int noahDir;
    private float moveSpd;
    Vector3 curTargetPos, weapOriginPos;
    void Start()
    {
        moveSpd = 3f;
        noahDir = 0;
        genCtr = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        joystickCtr = GameObject.Find("IMG_JoystickHandle").GetComponent<Joystick>();
        cutwoodMn = GameObject.Find("CAN_ArkMaking").GetComponent<ArkMakingMNSC>();
        deviceType = genCtr.deviceType;
        curTargetPos = Vector3.zero;
        curTarget = null;
        isAllowoMove = true;
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
        if (collision.gameObject.tag == "Tree" || collision.gameObject.tag == "Animals")
        {
            curTarget = collision.gameObject;
            curTargetPos = curTarget.transform.position;
        }
        else if (collision.gameObject.tag == "Logs") { cutwoodMn.OnIncreaseWoods(); }
        else if(collision.gameObject.tag == "Iron") { cutwoodMn.OnIncreaseIron(); }
        else if (collision.gameObject.tag == "Stone") { cutwoodMn.OnIncreaseStone(); }
        else if (collision.gameObject.tag == "Fruits") { cutwoodMn.OnInCreaseFruits(); }
        else if (collision.gameObject.tag == "Wheat") { cutwoodMn.OnIncreaseCrop(); }
        else if (collision.gameObject.tag == "Coin") { cutwoodMn.OnInCreaseMoney(); }
        else if (collision.gameObject.tag == "Build_Pos") 
        {
            isAllowoMove = false;
            cutwoodMn.OnShowBuildOption(); 
        }
    }

    private void OnMoveActionKey()
    {
        if(Input.GetKey(KeyCode.W) == true) { transform.position += Vector3.up * Time.deltaTime * moveSpd; }
        else if(Input.GetKey(KeyCode.S) == true) {    transform.position += Vector3.down * Time.deltaTime * moveSpd; }
        else if(Input.GetKey(KeyCode.A) == true)
        {
            if(noahDir == 0) { ChangeDir(); }
            transform.position += Vector3.left * Time.deltaTime * moveSpd;
        }
        else if(Input.GetKey(KeyCode.D) == true)
        {
            if (noahDir == 1) { ChangeDir(); }
            transform.position += Vector3.right * Time.deltaTime * moveSpd;
        }
    }
    private void OnMoveByTouch()
    {
        float horizontal = joystickCtr.Horizontal();
        float vertical = joystickCtr.Vertical();

        Vector3 direction = new Vector3(horizontal, vertical,0).normalized;
        transform.Translate(direction * Time.deltaTime * moveSpd);
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
    public void OnAttack()
    {
        isAllowoMove = false;
        weapon.transform.DOMove(curTargetPos, 1f).SetLoops(2, LoopType.Yoyo).OnComplete(() => isAllowoMove = true);
    }
}