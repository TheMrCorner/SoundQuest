using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Public atributes
    public CharacterController contr;

    public float speed = 12f;

    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public GameObject flashLight;

    // Private atributes
    float x, z;
    Vector3 move;
    Vector3 vel;
    bool isGrounded;


    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && vel.y < 0)
        {
            vel.y = -2f;
        }

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;

        contr.Move(move * speed * Time.deltaTime);

        vel.y += gravity * Time.deltaTime;

        contr.Move(vel * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flashLight.activeSelf)
            {
                flashLight.SetActive(false);
            }
            else
            {
                flashLight.SetActive(true);
            }
        }
    }
}
