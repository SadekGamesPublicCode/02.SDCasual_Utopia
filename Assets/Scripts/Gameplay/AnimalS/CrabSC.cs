using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabSC : AnimalSC
{
    void Start()
    {
        base.Start();
        isPredators = false;
        nutritionAmount = 20;
    }
}
