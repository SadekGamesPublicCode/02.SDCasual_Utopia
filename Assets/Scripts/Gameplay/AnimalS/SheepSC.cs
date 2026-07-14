using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSC : AnimalSC
{
    void Start()
    {
        base.Start();
        isPredators = false;
        nutritionAmount = 40;
    }
}
