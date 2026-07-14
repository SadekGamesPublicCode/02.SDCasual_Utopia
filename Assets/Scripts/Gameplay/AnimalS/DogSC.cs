using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSC : AnimalSC
{
    void Start()
    {
        base.Start();
        isPredators = true;
        nutritionAmount = 0;
    }
}
