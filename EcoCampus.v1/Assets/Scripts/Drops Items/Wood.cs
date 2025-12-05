using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class Wood : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float timeMove;

    private float timeCount;

    void Start()
    {

        
    }

    
    void Update()
    {

        // quando a madeira for instanciada vai iniciar o contador 
        timeCount += Time.deltaTime;

        
        if (timeCount < timeMove)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           collision.GetComponent<PlayerItems>().totalWood++;
            Destroy(gameObject);
        }
    }
}
