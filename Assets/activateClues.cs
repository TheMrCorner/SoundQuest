using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateClues : MonoBehaviour
{
    CapsuleCollider player;
    bool playerInside;

    private void Start()
    {
        playerInside = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerInside)
        {
            //GetComponent<Emitter>().play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == player)
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == player)
        {
            playerInside = false;
        }
    }
}
