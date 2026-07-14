using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMN : MonoBehaviour
{
    void Start() {}
    void Update() {}
    public void OnLoadScene(sbyte sceneOrder)
    {
        switch (sceneOrder)
        {
            case 0:
                //Loading Scene
                SceneManager.LoadScene("00_LoadScene");
                break;
            case 1:
                //Home Scene
                SceneManager.LoadScene("01_Home");
                break;
            case 2:
                //Utopia Jump Scene
                SceneManager.LoadScene("03_ArkMaking");
                break;
        }
    }
}
