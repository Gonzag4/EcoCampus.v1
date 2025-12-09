using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingNewLevel : MonoBehaviour {

	public string Text;

	void Start () {

	}

	// Verificando colisaão com o player
	void OnTriggerEnter2D(Collider2D collider2D){

		if(collider2D.gameObject.tag == "Player"){
			GameControl.Instance.UpdateMenssage(Text);

			GameControl.Instance.NextLevel();
		}
	}
}