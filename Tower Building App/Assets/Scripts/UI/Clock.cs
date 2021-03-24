﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class Clock : MonoBehaviour {


    //The variable will be used in scoring.cs 
    public static float TimeCounted;
    //Used lower case character to indicate private field
    public static bool playing;
    private DateTime gamePausedTime;

    public static Clock Instance;   
    void Awake()
    {
        if (Instance == null){
            Instance = this;   
        }
        else
        {      
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update () {
        if (playing == true)
        {   
            TimeCounted += Time.deltaTime;
        }
    }

    
    //Unity built-in method to detect when the user left the app
    void OnApplicationPause (bool isGamePause)
    {   
        //Store the current time when the app is paused
        if (isGamePause) {
            gamePausedTime = DateTime.Now;
        }
    }

    
    //Unity built-in method to detect when the user is come back
    void OnApplicationFocus  (bool isGameFocus)
    {   
        //Trigger BackgroundTimer method when user come back 
        if (isGameFocus && playing == true ) {
            BackgroundTimer();
        }
    }

    /*
    Calculate the total time difference between 
    time when user left and time when user come back
    */
    void BackgroundTimer(){
        TimeCounted += ((float)(DateTime.Now - gamePausedTime).TotalSeconds);
    }

    public void ClickPlay ()
    {
        playing = true;
    }

    public void ClickStop()
    {   
        playing = false;
        //Set the time to 0 
        TimeCounted = 0;
    }
}