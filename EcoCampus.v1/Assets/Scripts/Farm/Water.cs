using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class Water : MonoBehaviour
{

    [SerializeField] private bool detectingPlayer;
    [SerializeField] private int waterValue;

    private PlayerItems player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<PlayerItems>();
    }

    // Update is called once per frame
    void Update()
    {
        if (detectingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            player.WaterLimit(waterValue);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectingPlayer = false;
        }
    }
}
