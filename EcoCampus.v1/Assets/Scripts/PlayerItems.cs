using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class PlayerItems : MonoBehaviour
{
    [Header("Amaunts")]
    public int totalWood;
    public int carrots; 
    public float currentWater;
    public int fishes;


    [Header("Limits")]
    public float waterLimit = 50f;
    public float carrotsLimit = 10f;
    public float woodLimit = 5f;
    public float fishLimit = 3f;

    public void WaterLimit(float water)
    {
        if (currentWater < waterLimit)
        {
            currentWater += water;
        }


    }
}
