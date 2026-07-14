using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainThemeSC : MonoBehaviour
{
    [SerializeField] AudioSource main;
    private bool isAllowSound;
    private int pMusic; //This variable handle communicate with PlayerPrefs
    private void Start()
    {
        //pMusic = PlayerPrefs.GetInt("soundState");
        //CheckPlayerMusic();
    }
    //private void CheckPlayerMusic()
    //{
    //    if (pMusic == 0) isAllowSound = false;
    //    else if (pMusic == 1)
    //    {
    //        isAllowSound = true;
    //        PlayTheme();
    //    }
    //}
    //public void UpdateMusic(bool isAllow)
    //{
    //    if (isAllow == false) MuteTheme();
    //    else PlayTheme();
    //}
    public void PlayTheme() => main.volume = 1;
    public void MuteTheme() => main.volume = 0;
}
