using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startMenu : MonoBehaviour {

    public GameObject newGame1;
    public GameObject newGame2;
    public GameObject newGame3;
    public GameObject fillInText;
    public GameObject enter;
    public GameObject fillInText1;
    public GameObject enter1;
    public GameObject fillInText2;
    public GameObject enter2;
    public GameObject start;
    public GameObject name;
    public GameObject name2;
    public GameObject name3;
    public int clicked = 0;

  

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
    public  void newGameBt1(){
        if (clicked == 0)
        {
            fillInText.SetActive(true);
            enter.SetActive(true);
            newGame1.SetActive(false);
            clicked++;
        }
        else{
            SceneManager.LoadScene("main", LoadSceneMode.Single);
            //load scene level
        } 
    }
     public  void newGameBt2(){
        if (clicked == 0)
        {
            fillInText1.SetActive(true);
            enter1.SetActive(true);
            newGame2.SetActive(false);
            clicked++;
        }
        else{
            SceneManager.LoadScene("main", LoadSceneMode.Single);
            //load scene level
        } 
    }
     public  void newGameBt3(){
        if (clicked == 0)
        {
            fillInText2.SetActive(true);
            enter2.SetActive(true);
            newGame3.SetActive(false);
            clicked++;
        }
        else{
            SceneManager.LoadScene("main", LoadSceneMode.Single);
            //load scene level
        } 
    }

    public void EnterBt(){
        newGame1.GetComponentInChildren<Text>().text = name.GetComponent<Text>().text;
        if(clicked==1){
            fillInText.SetActive(false);
            enter.SetActive(false);
            newGame1.SetActive(true);
        }
    }
    public void EnterBt2(){
        newGame2.GetComponentInChildren<Text>().text = name2.GetComponent<Text>().text;
        if(clicked==1){
            fillInText1.SetActive(false);
            enter1.SetActive(false);
            newGame2.SetActive(true);
        }
    }
    public void EnterBt3(){
        newGame3.GetComponentInChildren<Text>().text = name3.GetComponent<Text>().text;
        if(clicked==1){
            fillInText2.SetActive(false);
            enter2.SetActive(false);
            newGame3.SetActive(true);
        }
    }
}


