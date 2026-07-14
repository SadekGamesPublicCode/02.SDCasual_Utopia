using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSC : Singleton<SoundSC>
{
    [SerializeField] AudioSource theme;
    public bool allowSFX;
    private void Start() { }
    public void PlaySFX() => allowSFX = true;
    public void MuteSFX() => allowSFX = false;
}
