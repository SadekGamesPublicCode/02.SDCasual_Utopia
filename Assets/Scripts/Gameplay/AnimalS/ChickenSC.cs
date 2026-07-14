using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSC : AnimalSC
{
    void Start()
    {
        base.Start();
        isPredators = false;
        nutritionAmount = 30;
    }
}
