﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Scoring : MonoBehaviour
{   
    //All static attributes will be used in UnlockBuilding.cs & ColorPicker.cs
    public static double MainXP = 0,
                        ArtsXP = 0,
                        BioCheXP = 100000,
                        ComSciXP = 0,
                        EngXP = 0,
                        GeoXP = 0,
                        LanXP = 0,
                        LawPolXP = 0,
                        PhyMathXP = 0;
    /*
    XP multiplier is used to multiply the XP per second
    Default is 1XP per second
    */
    private int multiplierXP = 1000000;
    //Local Earned XP is how much XP earned in a specific building
    private double localEarnedXP;
    /*
    Global Earned XP is how much XP earned for all buildings
    Default is 10% of the local XP 
    */
    private double globalEarnedXP;
    //The text that appear in the Pop Up Window
    public TextMeshProUGUI EarnedScoreText;
    //The drop down menu to get the current value that user selected
    public TMP_Dropdown DropDown;
    //Pop up is used to pop up a window when they earn XP
    public GameObject PopUp;
    /*
    timeCounted is how much time user has studied
    In order to do this, we need to access the static variable TimeCounted in StopWatch.cs
    */
    private float timeCounted;

    public void earnXP(){
        //Get the value in StopWatch.cs
        timeCounted = StopWatch.TimeCounted;
        //Local XP 1XP per second
        localEarnedXP = Math.Round(timeCounted) * multiplierXP;
        //Global XP is 10% of local XP
        globalEarnedXP = localEarnedXP * 0.1;
        
        //Pop up appears to show how much XP user has earned
        PopUp.SetActive(true);
        //Pop up text
        EarnedScoreText.text = "You've just earned" + " " + (localEarnedXP + globalEarnedXP).ToString() +"XP in" + " " + (DropDown.options[DropDown.value].text)
                                + " " + (globalEarnedXP).ToString() + "XP in other buildings";

        //Every building earns global XP (10 % of local XP)
        MainXP += globalEarnedXP;
        ArtsXP += globalEarnedXP;
        BioCheXP += globalEarnedXP;
        ComSciXP += globalEarnedXP;
        EngXP += globalEarnedXP;
        GeoXP += globalEarnedXP;
        LanXP += globalEarnedXP;
        LawPolXP += globalEarnedXP;
        PhyMathXP += globalEarnedXP;

        //Get the current value of the dropdown, when the stop button is clicked
        switch (DropDown.options[DropDown.value].text){
            case "MAIN":
                MainXP += localEarnedXP;
                break;
            case "ARTS":
                ArtsXP += localEarnedXP;
                break;
            case "BIOLOGY CHEMISTRY":
                BioCheXP += localEarnedXP;
                break;
            case "COMPUTER SCIENCE":
                ComSciXP += localEarnedXP;
                break;
            case "ENGINEERING":
                EngXP += localEarnedXP;
                break;
            case "GEOGRAPHY":
                GeoXP += localEarnedXP;
                break;
            case "LANGUAGES":
                LanXP += localEarnedXP;
                break;
            case "LAW POLITICS":
                LawPolXP += localEarnedXP;
                break;
            case "PHYSICS MATH":
                PhyMathXP += localEarnedXP;
                break;
        }
    }
}
