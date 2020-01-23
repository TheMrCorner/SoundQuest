using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODFirstPersonFootSteps : MonoBehaviour
{
    // Vaariables that will be set in the inspector
    [Header("FMOD Settings")]  // This ones are to set the different events and parameters
    [SerializeField] [FMODUnity.EventRef] private string footstepsEventPath;
    [SerializeField] [FMODUnity.EventRef] private string jumpingEventPath;
    [SerializeField] private string speedParameterName;
    [SerializeField] private string jumpingParameterName;
    public PlayerMovement pm; // Just to check if player is grounded

    [Header("Playback Settings")]
    [SerializeField] private float stepDistance = 2.0f;
    //[SerializeField] private float RayDistance = 1.2f; // Esta variable se usaría si añadimos más materiales
    [SerializeField] private float startRunningTime = 0.3f;
    // Aquí habría que añadir más cosas si vamos a usar diferente materiales en otras zonas y etc.

    // Variables used to control when the player walks
    private float stepRandom;
    private Vector3 prevPos;
    private float distanceTravelled;
    // Times between steps
    private float timeSinceLastStep;
    private int running; // This variable will tell if the player is running or not (0 or 1)

    // Start is called before the first frame update
    void Start()
    {
        stepRandom = Random.Range(0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastStep += Time.deltaTime;
        distanceTravelled += (pm.getPosition() - prevPos).magnitude;
        if (distanceTravelled >= stepDistance + stepRandom)
        {
            SpeedCheck();
            if (pm.getGroundCheck())
            {
                PlayFootStep();
            }
            stepRandom = Random.Range(0f, 0.5f);
            distanceTravelled = 0f;
        }
        prevPos = pm.getPosition();
    }

    /// <summary>
    /// Function used to set the previous position. Used for calculations. (Updated every frame) 
    /// </summary>
    /// <param name="p"> Current position </param>
    public void SetPosition(Vector3 p)
    {
        prevPos = p;
    }

    /// <summary>
    /// Checking if player is running or not
    /// </summary>
    void SpeedCheck()
    {
        Debug.Log(timeSinceLastStep);
        if(timeSinceLastStep < startRunningTime)
        {
            running = 1;
        }
        else
        {
            running = 0;
        }
        timeSinceLastStep = 0f; 
    }

    /// <summary>
    /// Function that triggers the step to play in the scene.
    /// </summary>
    void PlayFootStep()
    {
        // Create an instance of the event
        FMOD.Studio.EventInstance fs = FMODUnity.RuntimeManager.CreateInstance(footstepsEventPath);

        // Place it in scenario
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(fs, transform, GetComponent<Rigidbody>());

        // Set all parameters and play
        fs.setParameterByName(speedParameterName, running);
        fs.start();
        fs.release();
    }

    /// <summary>
    /// Function that triggers the jumping or landing effect. 
    /// </summary>
    /// <param name="effect"> Which effect will be played (0 jumping, 1 landing) </param>
    public void JumpOrLand(int effect)
    {
        // First create an instance of the event
        FMOD.Studio.EventInstance jL = FMODUnity.RuntimeManager.CreateInstance(jumpingEventPath);

        // Set the position where it is going to be played
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(jL, transform, GetComponent<Rigidbody>());

        // Choose between jumping or landing and play
        jL.setParameterByName(jumpingParameterName, effect);
        jL.start();

        // Release that instance
        jL.release();
    }
}
