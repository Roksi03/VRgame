using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
   public static AudioManager instance {  get; private set; }

    private List<EventInstance> eventInstances;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("found more than one Audio Manager in the scene");
        }
        instance = this;

        eventInstances = new List<EventInstance>();
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    private void Cleanup()
    {
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }

    private void OnDestroy()
    {
        Cleanup();
    }
}
