using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowSC : AnimalSC
{
    void Start()
    {
        base.Start();
        isPredators = false;
        nutritionAmount = 50;
    }
}
