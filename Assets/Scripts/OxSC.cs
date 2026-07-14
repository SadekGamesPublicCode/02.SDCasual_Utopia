using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxSC : AnimalSC
{
    void Start()
    {
        base.Start();
        isPredators = false;
        nutritionAmount = 40;
    }
}
