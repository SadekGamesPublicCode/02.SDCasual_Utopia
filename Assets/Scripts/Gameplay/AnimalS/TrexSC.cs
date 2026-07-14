using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrexSC : AnimalSC
{
    void Start()
    {
        base.Start();
        isPredators = true;
        nutritionAmount = 50;
    }
}
