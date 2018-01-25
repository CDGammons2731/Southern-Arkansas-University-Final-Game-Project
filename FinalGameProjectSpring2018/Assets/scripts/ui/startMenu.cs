using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startMenu : MonoBehaviour {

    public GameObject newGame1;
    public GameObject newGame2;
    public GameObject newGame3;
    public GameObject fillInText;
    public GameObject enter;
    public GameObject start;

  

    public void Update()
    {

    }
    public void StartButton()
    {
        //SceneManager.LoadScene("main", LoadSceneMode.Single);
        //load scene level

        newGame1.SetActive(true);
        newGame2.SetActive(true);
        newGame3.SetActive(true);
        start.SetActive(false);



    }
    public void LoadButton()
    {

    }
    public void CreditsButton()
    {
        //credits.SetActive(true);

    }
    public  void newGame(){
        fillInText.SetActive(true);
        enter.SetActive(true);

        
    }
}


