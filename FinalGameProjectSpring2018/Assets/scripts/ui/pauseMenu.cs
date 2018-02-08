using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GAMEMANAGER;

public class pauseMenu : MonoBehaviour
{
    GameManager sa;
    public GameObject pm;
    private bool isEnabled = false;

    private void Start()
    {
     
    }

    void Update()
    {
        // Enable pause menu
        if (Input.GetKeyDown(KeyCode.Escape) && !isEnabled)
        {
            pm.SetActive(true);
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
    public void Quit()
    {
        SceneManager.LoadScene("startMenu", LoadSceneMode.Single);//loads start menu
    }

}

