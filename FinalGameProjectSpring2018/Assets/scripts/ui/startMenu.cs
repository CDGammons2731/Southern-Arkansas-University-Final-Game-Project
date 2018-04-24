using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class startMenu : MonoBehaviour {


    public GameObject creds;
    public GameObject controlsBt;
    public GameObject controlImage;

    FirstPersonController cur;
    bool unlocked = false;
    bool locked = true;
    

  

    public void Update()
    {
        if (cur == null)
        {
            GameObject playerGO = GameObject.FindGameObjectWithTag("player");
            if (playerGO != null)
            {
                cur = playerGO.GetComponent<FirstPersonController>();
            }
        }
    }
    public void StartButton()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene("main", LoadSceneMode.Single);
        cur.m_MouseLook.SetCursorLock(locked);
        //load scene level
    }

    public void CreditsButton()
    {
        creds.SetActive(true);
    }
    public void Controls(){
        controlImage.SetActive(true);
    }

    public void ExitButton(){
        Application.Quit();
    }

    public void QuitCred(){
        creds.SetActive(false);
    }
    public void QuitCont(){
        controlImage.SetActive(false);
    }
}


