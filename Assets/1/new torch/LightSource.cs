using UnityEngine;
using FMOD.Studio;

public class LightSource : MonoBehaviour
{
    private EventInstance lightSource;

    public bool canIgnite = true;
    public GameObject fire;


    private void Start()
    {
        if (fire != null)
        {
            fire.SetActive(true);
        }

        lightSource = AudioManager.instance.CreateEventInstance(FMODEvents.instance.lightSource);
    }

    private void FixedUpdate()
    {
        UpdateSound();
    }

    private void UpdateSound()
    {
        PLAYBACK_STATE playbackState;
        lightSource.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
        {
            lightSource.start();
        }
        else
        {
            lightSource.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}
