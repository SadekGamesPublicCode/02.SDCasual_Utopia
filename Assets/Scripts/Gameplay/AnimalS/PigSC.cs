using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigSC : AnimalSC
{
    void Start()
    {
        base.Start();
        isPredators = false;
        nutritionAmount = 80;
    }
}
