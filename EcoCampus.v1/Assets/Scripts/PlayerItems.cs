using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class PlayerItems : MonoBehaviour
{
    [Header("Amaunts")]
    public int totalWood;
    public int carrots; 
    public float currentWater;


    [Header("Limits")]
    public float waterLimit = 50;
    public float carrotsLimit = 10;
    public float woodLimit = 5;

    public void WaterLimit(float water)
    {
        if (currentWater < waterLimit)
        {
            currentWater += water;
        }


    }
}
