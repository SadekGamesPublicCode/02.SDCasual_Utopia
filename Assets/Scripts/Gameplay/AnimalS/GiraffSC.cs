using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiraffSC : AnimalSC
{
    void Start()
    {
        base.Start();
        isPredators = false;
        nutritionAmount = 60;
    }
}
