using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System;

public class OcclusionScript : MonoBehaviour
{
    [Header("FMOD")]
    [FMODUnity.EventRef]
    public string selectAudio;
    public string parameterVolume;
    public string parameterLPF;
    FMOD.Studio.EventInstance audio;
    FMOD.Studio.PARAMETER_ID vol; // Volume of music 
    FMOD.Studio.PARAMETER_ID lpf; // Low Pass Filter

    Transform listenerLocation;

    [Header("Occlusion Options")]
    [Range(0.0f, 1.0f)]
    public float occlusionVolume = 0.5f;
    [Range(0.0f, 1.0f)]
    public float normalVolume = 0.5f;
    [Range(10.0f, 22000.0f)]
    public float lowPassFilter = 10000.0f;
    public LayerMask occlusionLayer = 1;

    [Header("Listener Collider")]
    public CapsuleCollider listenerCollider;

    private void Awake()
    {
        // Get the listener position
        listenerLocation = GameObject.FindObjectOfType<StudioListener>().transform;

        // Get the audio emitter
        audio = FMODUnity.RuntimeManager.CreateInstance(selectAudio);

        // Parameters Setting
        // Save description of things
        FMOD.Studio.EventDescription ed;
        FMOD.Studio.PARAMETER_DESCRIPTION pd;

        // Get event description
        audio.getDescription(out ed);

        // Get VOLUME parameter description and set ID
        ed.getParameterDescriptionByName(parameterVolume, out pd);
        vol = pd.id;

        // Get LPF parameter description and set ID
        ed.getParameterDescriptionByName(parameterLPF, out pd);
        lpf = pd.id;
    }

    private void Start()
    {
        // Check if audio is playing and set it to play
        FMOD.Studio.PLAYBACK_STATE state;
        audio.getPlaybackState(out state);
        if(state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            audio.start();
        }
    }

    private void Update()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(audio, GetComponent<Transform>(), GetComponent<Rigidbody>());

        RaycastHit rc;
        Debug.Log(this.transform.position);
        Physics.Linecast(this.transform.position, listenerLocation.position, out rc, occlusionLayer);

        if(rc.collider == listenerCollider)
        {
            // Change occlusion
            NotOccluded();
            Debug.DrawLine(transform.position, listenerLocation.position, Color.green);
        }
        else
        {
            Occluded();
            Debug.DrawLine(transform.position, rc.point, Color.red);
        }
    }

    private void Occluded()
    {
        audio.setParameterByID(vol, occlusionVolume);
        audio.setParameterByID(lpf, lowPassFilter);
    }

    private void NotOccluded()
    {
        audio.setParameterByID(vol, normalVolume);
        audio.setParameterByID(lpf, 22000f);
    }
}
