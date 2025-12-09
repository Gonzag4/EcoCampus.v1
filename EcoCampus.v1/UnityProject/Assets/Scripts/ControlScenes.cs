using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlScenes : MonoBehaviour {

	public GameObject Scene1;
	public GameObject Scene2;
	public GameObject Scene3;

	public int countScenes = 1;

	public void Update(){

		if(countScenes == 1){
			Scene1.SetActive(true);
			Scene2.SetActive(false);
			Scene3.SetActive(false);

		}

		if(countScenes == 2){
			Scene2.SetActive(true);
			Scene1.SetActive(false);
			Scene3.SetActive(false);

		}

		if(countScenes == 3){
			Scene3.SetActive(true);
			Scene1.SetActive(false);
			Scene2.SetActive(false);

		}
	}


	public void Next(){
		countScenes +=1;
	}

	public void NextGame(string LevelName){
		SceneManager.LoadScene(LevelName);
	}


}