using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab9 : MonoBehaviour
{

    private float dTimer = 0;
    void Start()
    {
        // Functions return most members to be logged, sorry they don't do anything interesting.
        // The meat of the class is just manual reference counting.
        // Learning about garbage collection, disappointed that I can't manually call destructor in C#. 
        
        Lab9Class instance = new Lab9Class();
        Debug.Log($"Class instantiated at {instance.CheckWhenConstructed()}");
        Debug.Log($"Class first instantiated at {Lab9Class.CheckWhenFirstConstructed()}");
        Debug.Log($"Number of instances constructed: {Lab9Class.CheckTimesConstructed()} number of instances alive: {Lab9Class.CheckTotalAliveInstances()} ");
        Debug.Log($"Time alive: {instance.CheckAliveTime()}");
    }

    private void Update()
    {
        if (dTimer < 5)
        {
            dTimer += Time.deltaTime;
        }
        else
        {
            dTimer -= 5;
            Debug.Log(
                $"Listening for GC... \n" +
                $"instances constructed: {Lab9Class.CheckTimesConstructed()} \n" +
                $"instances alive: {Lab9Class.CheckTotalAliveInstances()} ");
        }
    }
}


public class Lab9Class
{
    private static bool existed = false;  
    private static int timesConstructed = 0;
    private static int aliveInstances = 0;
    private DateTime timeConstructed;
    private static DateTime firstTimeConstructed;
    
    public Lab9Class()
    {
        timeConstructed = DateTime.Now;
        Debug.Log($"This class existed prior: {existed}");
        if (! existed)
        {
            firstTimeConstructed = timeConstructed;
            existed = true;
        }

        aliveInstances++;
        timesConstructed++;
    }

    public DateTime CheckWhenConstructed()
    {
        return timeConstructed;
    }
    
    public static DateTime CheckWhenFirstConstructed()
    {
        return firstTimeConstructed;
    }

    public TimeSpan CheckAliveTime()
    {
        return DateTime.Now - timeConstructed;
    }
    
    public static int CheckTotalAliveInstances()
    {
        return aliveInstances;
    }

    public static int CheckTimesConstructed()
    {
        return timesConstructed;
    }

    ~Lab9Class()
    {
        Debug.Log("Instance has died.");
        aliveInstances--;
    }
    
}
