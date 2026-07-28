using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeapSysSC : MonoBehaviour
{
    public int curWeapIndex;
    [SerializeField] Image weapIcon;
    [SerializeField] List<Sprite> weapSpriteList = new List<Sprite>();
    [SerializeField] List<GameObject> weapList = new List<GameObject>();
    void Start()
    {
        weapIcon = GameObject.Find("IMG_IconItemHolding").GetComponent<Image>();
        curWeapIndex = 0;
        weapIcon.GetComponent<Image>().sprite = weapSpriteList[curWeapIndex];
        OnvisibleWeap(curWeapIndex);
    }
    void OnvisibleWeap(int index)
    {
        print("index: " + index);
        for (int i = 0; i < weapList.Count; i++)
        {
            weapList[i].SetActive(false);
        }
        weapList[index].SetActive(true);
    }
    public void HandleWitchWeap()
    {
        curWeapIndex++;
        if (curWeapIndex >= weapList.Count)
        {
            print("in indexer > count");
            curWeapIndex = 0;
        }
        OnvisibleWeap(curWeapIndex);
        weapIcon.GetComponent<Image>().sprite = weapSpriteList[curWeapIndex];
    }
}
