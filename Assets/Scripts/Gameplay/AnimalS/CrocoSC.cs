using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocoSC : AnimalSC
{
    void Start()
    {
        base.Start();
        isPredators =true;
        nutritionAmount = 20;
    }
}
