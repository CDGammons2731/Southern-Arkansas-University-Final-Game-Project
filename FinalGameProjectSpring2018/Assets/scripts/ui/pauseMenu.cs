﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GAMEMANAGER;
using UnityStandardAssets.Characters.FirstPerson;


public class pauseMenu : MonoBehaviour
{
    public GameObject resumeBt;
    public GameObject optionBt;
    public GameObject quitBt;
    public GameObject soundSlider;
    public GameObject soundText;
   
    public Slider volume;

    bool back=false;

    FirstPersonController cur;
    bool unlocked = false;
    bool locked = true;

    GameManager sa;
    public GameObject pm;
    private bool isEnabled = false;



    void Update()
    {
        if(cur==null){
            GameObject playerGO = GameObject.FindGameObjectWithTag("player");
            if (playerGO != null)
            {
                cur = playerGO.GetComponent<FirstPersonController>();
            }
        }

        // Enable pause menu
        if (Input.GetKeyDown(KeyCode.Escape) && !isEnabled)
        {
            
            pm.SetActive(true);
            Reset();
            back=false;
            isEnabled = true;
            Time.timeScale = 0;

            cur.m_MouseLook.SetCursorLock(unlocked);
           
           
        }
        // disable pause menu
        else if (Input.GetKeyDown(KeyCode.Escape) && isEnabled)
        {
            pm.SetActive(false);
            isEnabled = false;
            Time.timeScale = 1;
            cur.m_MouseLook.SetCursorLock(locked);
           
        }


        
    }
  
    public void Resume(){
   
        pm.SetActive(false);
        isEnabled = false;
        Time.timeScale = 1;
        cur.m_MouseLook.SetCursorLock(locked);

    }
    public void Option(){
        cur.m_MouseLook.SetCursorLock(unlocked);

        soundSlider.SetActive(true);
        soundText.SetActive(true);
            
    }
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
    public void Reset(){
        resumeBt.SetActive(true);
        optionBt.SetActive(true);
        quitBt.SetActive(true);
        soundSlider.SetActive(false);
        soundText.SetActive(false);
    }



    public void SoundSlider(){
        AudioListener.volume = volume.value;
    }

}

