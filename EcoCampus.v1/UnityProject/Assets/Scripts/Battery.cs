using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour {
	private SpriteRenderer spriteRenderer;
	public GameObject collectedEffect;

	public int Score;

	void Start () {
		//Inicializando variável
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Verificando colisaão com o player
	void OnTriggerEnter2D(Collider2D collider2D){

		if(collider2D.gameObject.tag == "Player"){
			GameControl.Instance.TotalScoreBat += Score;
			GameControl.Instance.UpdateScore(2);
			
			spriteRenderer.enabled = false;
			collectedEffect.SetActive(true);

			Destroy(gameObject, 0.2f);
		}
	}
}
