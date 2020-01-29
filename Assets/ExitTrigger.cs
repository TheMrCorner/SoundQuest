using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour
{
    public CapsuleCollider playerCollider;
    public GameObject player;
    Vector3 originalPosition;

    FMOD.Studio.EventInstance instance; 

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
                instance.release();

                //Ir a la escena de final
                SceneManager.LoadScene(1);
            }
            else
            {
                player.transform.position = originalPosition;
            }
        }
    }

    public void setEventEmitter(FMOD.Studio.EventInstance st)
    {
        instance = st; 
    }

    public void setExit()
    {
        isExit = true;
    }
}
