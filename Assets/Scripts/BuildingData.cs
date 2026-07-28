using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingData : MonoBehaviour
{
    // Holds data for a single building
    [Serializable]
    public class SingleBuildingData
    {
        public string buildingID; // e.g., "house", "tower"
        public int x;
        public int y;
    }

    // Wrapper class because JsonUtility requires a top-level object (not a raw List)
    [Serializable]
    public class SaveData
    {
        public List<SingleBuildingData> buildings = new List<SingleBuildingData>();
    }
}
