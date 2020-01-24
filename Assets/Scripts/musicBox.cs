using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicBox : MonoBehaviour
{

    bool playerInside = false;
    bool open = false;
    GameManager gm;

    [Header("Player Collider")]
    public CapsuleCollider player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerInside)
        {
            if (!open)
            {
                gameObject.GetComponentInParent<Animation>().Play();
                PlaySoundEffect("event:/BoxOpen");
                open = true;
                gm.addOpenBox(transform.parent.gameObject);
            }
            else
            {
                gameObject.GetComponentInParent<Animation>().CrossFade("Crate_Close");
                PlaySoundEffect("event:/BoxClose");
                open = false;
                gm.removeOpenBox(transform.parent.gameObject);
            }
        }

    }

    private void PlaySoundEffect(string effect)
    {
        // First create an instance of the event
        FMOD.Studio.EventInstance jL = FMODUnity.RuntimeManager.CreateInstance(effect);

        // Set the position where it is going to be played
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(jL, transform, GetComponent<Rigidbody>());

        // Choose between jumping or landing and play
        jL.start();

        // Release that instance
        jL.release();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == player )
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
