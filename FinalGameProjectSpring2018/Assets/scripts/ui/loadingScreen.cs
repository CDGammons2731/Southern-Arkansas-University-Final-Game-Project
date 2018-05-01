using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadingScreen : MonoBehaviour {
    public GameObject loadingScrn;

	public void LoadLevels(){
		StartCoroutine(LoadAsynch());
        loadingScrn.SetActive(true);
	
	}

	IEnumerator LoadAsynch(){
        
		AsyncOperation op= SceneManager.LoadSceneAsync(1);


		while(op.isDone==false){

			yield return null;
		}
        

	}

	
}
