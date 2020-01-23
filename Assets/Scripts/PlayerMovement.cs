using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Public atributes
    [Header("Basic Information, Configuration")]
    public CharacterController contr;
    public float speed = 12f;
    public FMODFirstPersonFootSteps steps; // This variable is to trigger the footsteps effect

    [Header("Jumping and Gravity Configuration")]
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;

    [Header("Flashlight")]
    public GameObject flashLight;

    // Private atributes
    float x, z;
    Vector3 move;
    Vector3 vel;
    bool isGrounded;
    bool wasGrounded = true;

    private void Start()
    {
        steps.SetPosition(transform.position); // First set position for steps effect
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && !wasGrounded) // This plays the landing effect
        {
            steps.JumpOrLand(1);
        }

        if (isGrounded && vel.y < 0)
        {
            vel.y = -2f;
        }

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;

        contr.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) // This activates the jump
        {
            vel.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            // Then play the jumping effect
            steps.JumpOrLand(0);
        }

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

        wasGrounded = isGrounded;
    }

    public Vector3 getPosition()
    {
        return transform.position;
    }

    public bool getGroundCheck()
    {
        return isGrounded;
    }
}
