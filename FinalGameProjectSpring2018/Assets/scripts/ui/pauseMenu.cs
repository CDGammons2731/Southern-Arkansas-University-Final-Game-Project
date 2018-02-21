using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GAMEMANAGER;


public class pauseMenu : MonoBehaviour
{
    public GameObject resumeBt;
    public GameObject optionBt;
    public GameObject evidenceBt;
    public GameObject quitBt;
    public GameObject soundSlider;
    public GameObject soundText;

    bool back=false;
  
    

    GameManager sa;
    public GameObject pm;
    private bool isEnabled = false;


    void Update()
    {
        // Enable pause menu
        if (Input.GetKeyDown(KeyCode.Escape) && !isEnabled)
        {
            pm.SetActive(true);
            Reset();
            back=false;
            isEnabled = true;
            Time.timeScale = 0;
           
        }
        // disable pause menu
        else if (Input.GetKeyDown(KeyCode.Escape) && isEnabled)
        {
            pm.SetActive(false);
            isEnabled = false;
            Time.timeScale = 1;
        }
        
    }
    public void SaveButton()
    {
        sa.Save();
    }
    public void Resume(){
   
        pm.SetActive(false);
        isEnabled = false;
        Time.timeScale = 1;
    }
    public void Option(){
        
        if(!back){
            resumeBt.SetActive(false);
            evidenceBt.SetActive(false);
            quitBt.SetActive(false);
            soundSlider.SetActive(true);
            soundText.SetActive(true);
            back=true;
        }
        else{
            resumeBt.SetActive(true);
            evidenceBt.SetActive(true);
            quitBt.SetActive(true);
            soundSlider.SetActive(false);
            soundText.SetActive(false);
        }        
    }
    public void Quit()
    {
        SceneManager.LoadScene("startMenu", LoadSceneMode.Single);//loads start menu
        Time.timeScale = 1;
    }
    public void Reset(){
        resumeBt.SetActive(true);
        optionBt.SetActive(true);
        evidenceBt.SetActive(true);
        quitBt.SetActive(true);
        soundSlider.SetActive(false);
        soundText.SetActive(false);
    }
    

}

