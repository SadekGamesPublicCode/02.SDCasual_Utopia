using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSC : MonoBehaviour
{
    SoundSC sfxMNs;
    MainThemeSC mainThemeMN;
    int themeAllow, sfxAllow;
    [HideInInspector] GeneralContrlSC genCtrl;
    [SerializeField] Image themeLoud, themeMute, sfxLoud, sfxMute;
    private void Awake()
    {
        sfxMNs = GameObject.Find("OBJ_SoundControl").GetComponent<SoundSC>();
        mainThemeMN = GameObject.Find("OBJ_SoundControl").GetComponent<MainThemeSC>();
        themeAllow = PlayerPrefs.GetInt("soundState");
        sfxAllow = PlayerPrefs.GetInt("sfxState");
        genCtrl = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
    }
    void Start()
    {
        CheckSound();
    }
    public void CheckSound()
    {
        themeAllow = genCtrl.genThemeAllow;
        sfxAllow = genCtrl.genSFXAllow;
        switch (themeAllow)
        {
            case 0:
                themeMute.gameObject.SetActive(true);
                themeLoud.gameObject.SetActive(false);
                break;
            case 1:
                themeMute.gameObject.SetActive(false);
                themeLoud.gameObject.SetActive(true);
                break;
        }

        switch(sfxAllow)
        {
            case 0:
                sfxMute.gameObject.SetActive(true);
                sfxLoud.gameObject.SetActive(false);
                break;
            case 1:
                sfxMute.gameObject.SetActive(false);
                sfxLoud.gameObject.SetActive(true);
                break;
        }
    }
    public void OnChangeThemState()
    {
        if(themeAllow == 1)
        {
            themeAllow = 0;
            themeMute.gameObject.SetActive(true);
            themeLoud.gameObject.SetActive(false);
            genCtrl.OnPlayTheme(themeAllow);
        }else if(themeAllow == 0)
        {
            themeAllow = 1;
            themeMute.gameObject.SetActive(false);
            themeLoud.gameObject.SetActive(true);
            genCtrl.OnPlayTheme(themeAllow);
        }

    }
    public void OnChangeSFXState()
    {
        if (sfxAllow == 1)
        {
            sfxAllow = 0;
            sfxMute.gameObject.SetActive(true);
            sfxLoud.gameObject.SetActive(false);
            genCtrl.OnPlaySFX(sfxAllow);
        }
        else if (sfxAllow == 0)
        {
            sfxAllow = 1;
            sfxMute.gameObject.SetActive(false);
            sfxLoud.gameObject.SetActive(true);
            genCtrl.OnPlaySFX(sfxAllow);
        }
    }
    public void ExitGame() => Application.Quit();
    public void ToGGPlayStore() => Application.OpenURL("https://play.google.com/store/apps/developer?id=Sadek+Games+Studio");
    public void ToPrivacyPolicy() => Application.OpenURL("https://sadekgame.wordpress.com/2025/07/13/privacy-policy-of-utopia-rise/");
}
