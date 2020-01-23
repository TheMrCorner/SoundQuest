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
                open = true;
                gm.addOpenBox(transform.parent.gameObject);
            }
            else
            {
                gameObject.GetComponentInParent<Animation>().CrossFade("Crate_Close");
                open = false;
                gm.removeOpenBox(transform.parent.gameObject);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == player )
        {
            
            playerInside = true;
        }
    }

}
