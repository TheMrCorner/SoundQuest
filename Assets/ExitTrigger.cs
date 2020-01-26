using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public CapsuleCollider playerCollider;
    public GameObject player;
    Vector3 originalPosition;

    bool isExit;

    private void Start()
    {
        isExit = false;
        originalPosition = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == playerCollider )
        {
            if (isExit)
            {
                //Ir a la escena de final
            }
            else
            {
                player.transform.position = originalPosition;
            }
        }
    }

    public void setExit()
    {
        isExit = true;
    }
}
