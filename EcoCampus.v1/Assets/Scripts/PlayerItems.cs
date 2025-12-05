using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class PlayerItems : MonoBehaviour
{
    
    public int totalWood;
   
    public float currentWater;
    private float waterLimit = 50;

    public void WaterLimit(float water)
    {
        if (currentWater < waterLimit)
        {
            currentWater += water;
        }


    }


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
