using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startMenu : MonoBehaviour {
  

    public void Update()
    {

    }
    public void StartButton()
    {
        SceneManager.LoadScene("main", LoadSceneMode.Single);//load scene level
    }
    public void LoadButton()
    {

    }
    public void CreditsButton()
    {
        //credits.SetActive(true);

    }
}


