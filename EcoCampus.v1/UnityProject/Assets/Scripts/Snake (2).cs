using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {
	
	private Rigidbody2D rigidbody2D;
	private Animator animator;
	public Transform HeadCollider1;
	public Transform HeadCollider2;
	public LayerMask layer;
	private BoxCollider2D boxCollider2D;
	private CircleCollider2D circleCollider2D;

	public float Speed_walk;
	public float ForceImpulse;
	private bool obstacle;

	void Start () {

		//inicializando variávei
		rigidbody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();	
		boxCollider2D = GetComponent<BoxCollider2D>();	
		circleCollider2D = GetComponent<CircleCollider2D>();
	}
	
	void Update () {

		// Movimentação do NPC
		rigidbody2D.linearVelocity = new Vector2(Speed_walk, rigidbody2D.linearVelocity.y);

		obstacle = Physics2D.Linecast(HeadCollider1.position, HeadCollider2.position, layer);

		if(obstacle){
			transform.localScale = new Vector2 (transform.localScale.x * -1f, transform.localScale.y);
			Speed_walk *= -1f;
		}	
	}
	
	void OnTriggerEnter2D(Collider2D collider2D){
		if(collider2D.gameObject.tag == "Player"){
			if(GameControl.Instance.VerifAnimal()){
			
				Speed_walk = 0;
				boxCollider2D.enabled = false;
				circleCollider2D.enabled = false;
				rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
		
				collider2D.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * ForceImpulse, ForceMode2D.Impulse);
				animator.SetBool("Die", true);

				Destroy(gameObject, 0.6f);

				GameControl.Instance.GameOver();
			}

			else{
				GameControl.Instance.AnimalDead +=1;
			}
		}
	}

}
