using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOcclusion : MonoBehaviour
{
    // This script will handle all occlusion events 
    private FMOD.Studio.EventInstance instance;

    [SerializeField]
    private bool occlusionEnabled = false;

    [SerializeField]
    private string occlusionParName = null;

    [Range(0.0f, 10.0f)]
    [SerializeField]
    private float occlusionIntensity = 1.0f;

    private float currentOcc = 0.0f;

    private float nextUpdate = 0.0f;

    [FMODUnity.EventRef]
    public string fmodEvent;

    // Start is called before the first frame update
    void Start()
    {
        //instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        //instance.start();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (instance.isValid())
        {
            instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.gameObject));
            if (!occlusionEnabled)
            {
                currentOcc = 0.0f; 
            }
            else if(Time.time >= nextUpdate)
            {
                nextUpdate = Time.time + FmodResonanceAudio.occlusionDetectionInterval;
                currentOcc = occlusionIntensity * FmodResonanceAudio.ComputeOcclusion(transform);
                instance.setParameterByName(occlusionParName, currentOcc);
            }
        }*/
    }
}
