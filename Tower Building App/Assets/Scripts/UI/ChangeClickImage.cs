﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class ChangeClickImage : MonoBehaviour
{
    public Sprite normalImage;
    public Sprite clickedImage;
    public Button icon;
    private bool isRotate = true;

    public CameraMovePhone movement;


    void Start()
    {
        icon = GetComponent<Button>();
    }

    //Rotation Icon
    public void RotationIcon()
    {
        if (isRotate == true){
            icon.image.overrideSprite = clickedImage;
            isRotate = false;
        }
        else{
            icon.image.overrideSprite = normalImage;
            isRotate = true;
        }

        //plays standard button click sound
        FindObjectOfType<SoundManager>().Play("standard button click");

        //boolean in camera movement is true if panning. this script is true if rotating
        movement.setMode(isRotate);
    }

    //Main Scene Footer
    public void Building()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1){
            icon.image.overrideSprite = clickedImage;
        }
        else{
            SceneManager.LoadScene(1);
            icon.image.overrideSprite = clickedImage;
        }

        //plays standard button click sound
        FindObjectOfType<SoundManager>().Play("standard button click");

        //turn filter off
        FindObjectOfType<ListenerPersist>().toggleFilterOn(false);
    }

    //Timing Clock Footer
    public void TimingClock()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2){
            icon.image.overrideSprite = clickedImage;
        }
        else{
            SceneManager.LoadScene(2);
            icon.image.overrideSprite = clickedImage;
        }

        //plays standard button click sound
        FindObjectOfType<SoundManager>().Play("standard button click");

        //turn filter on
        FindObjectOfType<ListenerPersist>().toggleFilterOn(true);
    }

    //FriendList Footer
    public void FriendList()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3){
            icon.image.overrideSprite = clickedImage;
        }
        else{
            SceneManager.LoadScene(3);
            icon.image.overrideSprite = clickedImage;
        }

        //plays standard button click sound
        FindObjectOfType<SoundManager>().Play("standard button click");
        //turn filter on
        FindObjectOfType<ListenerPersist>().toggleFilterOn(true);
    }

    //LeaderBoard Footer
    public void LeaderBoard()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4){
            icon.image.overrideSprite = clickedImage;
        }
        else{
            SceneManager.LoadScene(4);
            icon.image.overrideSprite = clickedImage;
        }

        //plays standard button click sound
        FindObjectOfType<SoundManager>().Play("standard button click");
        //turn filter on
        FindObjectOfType<ListenerPersist>().toggleFilterOn(true);
    }

    //Profile Icon
    public void XPcode()
    {
        SceneManager.LoadScene(5);

        //plays standard button click sound
        FindObjectOfType<SoundManager>().Play("standard button click");
        //turn filter on
        FindObjectOfType<ListenerPersist>().toggleFilterOn(true);
    }

    //Customisation Icon
    public void Customisation()
    {
        SceneManager.LoadScene(6);

        //plays standard button click sound
        FindObjectOfType<SoundManager>().Play("standard button click");
        //turn filter on
        FindObjectOfType<ListenerPersist>().toggleFilterOn(true);
    }


    //Buildings
    public void MainBuilding()
    {
        SceneManager.LoadScene(7);

        //plays standard button click sound
        FindObjectOfType<SoundManager>().Play("standard button click");
        //turn filter on
        FindObjectOfType<ListenerPersist>().toggleFilterOn(true);
    }

    public void ComputerScienceBuilding()
    {
        SceneManager.LoadScene(8);

        //plays standard button click sound
        FindObjectOfType<SoundManager>().Play("standard button click");
        //turn filter on
        FindObjectOfType<ListenerPersist>().toggleFilterOn(true);
    }

    public void GeographyBuilding()
    {
        SceneManager.LoadScene(9);

        //plays standard button click sound
        FindObjectOfType<SoundManager>().Play("standard button click");
        //turn filter on
        FindObjectOfType<ListenerPersist>().toggleFilterOn(true);
    }

    public void HistoryBuilding()
    {
        SceneManager.LoadScene(10);

        //plays standard button click sound
        FindObjectOfType<SoundManager>().Play("standard button click");
        //turn filter on
        FindObjectOfType<ListenerPersist>().toggleFilterOn(true);
    }

    public void BiologyBuilding()
    {
        SceneManager.LoadScene(11);

        //plays standard button click sound
        FindObjectOfType<SoundManager>().Play("standard button click");
        //turn filter on
        FindObjectOfType<ListenerPersist>().toggleFilterOn(true);
    }
}


