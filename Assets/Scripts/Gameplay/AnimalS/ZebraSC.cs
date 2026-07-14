using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebraSC : AnimalSC
{
    void Start()
    {
        base.Start();
        isPredators = false;
        nutritionAmount = 40;
    }
}
