using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantSC : AnimalSC
{
    void Start()
    {
        base.Start();
        isPredators = false;
        nutritionAmount = 0;
    }
}
