using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarSC : AnimalSC
{
    void Start()
    {
        base.Start();
        isPredators = true;
        nutritionAmount = 0;
    }
}
