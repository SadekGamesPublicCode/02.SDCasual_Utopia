using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditSC : MonoBehaviour
{
    [HideInInspector] GeneralContrlSC genCtr;
    void Start() { genCtr = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>(); }
    void Update() { }
    public void ToPrivaciPolicy() { Application.OpenURL("https://sadekgame.wordpress.com/2025/07/13/privacy-policy-of-utopia-rise/"); }
    public void ToTermUse() { Application.OpenURL("https://sadekgame.wordpress.com/2025/11/15/temr-use-utopia-rise/"); }
    public void ToFB() { Application.OpenURL("https://www.facebook.com/sadeksoftVn"); }
    public void ToIG() { Application.OpenURL("https://www.instagram.com/sdsoftvn/"); }
    public void ToX() { Application.OpenURL("https://x.com/SadekGame15769"); }
    public void ToWebsite() { Application.OpenURL("https://play.google.com/store/apps/developer?id=Sadek+Games+Studio"); }
    public void ToYTB() { Application.OpenURL("https://www.youtube.com/@SadekGamesStudio"); }
    public void ToTikTok() { Application.OpenURL("https://www.tiktok.com/@sdsoft"); }
}
