using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PossessAreaScript : MonoBehaviour
{
    private GameObject player;
    public bool playerAround = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            playerAround = true;


            Debug.Log("Player entered.");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            playerAround = false;


            Debug.Log("Player left.");
        }
    }
}
