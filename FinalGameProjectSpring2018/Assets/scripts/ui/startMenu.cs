using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startMenu : MonoBehaviour {


    public GameObject creds;
    public GameObject controlsBt;
    public GameObject controlImage;


    

  

    public void Update()
    {

    }
    public void StartButton()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene("main", LoadSceneMode.Single);
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


