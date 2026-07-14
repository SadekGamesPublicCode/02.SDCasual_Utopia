using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSC : AnimalSC
{
    void Start()
    {
        base.Start();
        isPredators = true;
        nutritionAmount = 10;
    }
}
