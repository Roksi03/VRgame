using FMOD.Studio;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightTorch : MonoBehaviour
{
    private EventInstance torchIgnite;

    public Light torchLight;
    public GameObject childObjectToActivate;

    [SerializeField] private bool isLit = false;

    public bool isTorchLit { get { return isLit; } }

    public Material litMaterial;
    public Renderer torchRenderer;
    public int materialIndex = 0;

    public Material childLitMaterial;
    public Renderer childRenderer;

    private static List<LightTorch> allTorches = new List<LightTorch>();
    private static bool allTorchesLit = false;
    public static bool AreAllTorchesLit()
    {
        return allTorchesLit;
    }

    private void OnEnable()
    {
        if (!allTorches.Contains(this))
        {
            allTorches.Add(this);
        }

        CheckAllTorchesLitStatus();
    }

    private void OnDisable()
    {
        if (allTorches.Contains(this))
        {
            allTorches.Remove(this);
        }

        CheckAllTorchesLitStatus();
    }

    private void Start()
    {

        if (torchLight != null)
        {
            torchLight.enabled = false;
        }

        if (childObjectToActivate != null)
        {
            childObjectToActivate.SetActive(false);

            if (childRenderer == null)
            {
                childRenderer = childObjectToActivate.GetComponent<Renderer>();
            }
        }

        torchIgnite = AudioManager.instance.CreateEventInstance(FMODEvents.instance.torchIgnite);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (isLit)
        {
            return;
        }

        LightSource lightSource = other.GetComponent<LightSource>();

        if (lightSource != null && lightSource.canIgnite)
        {
            IgniteTorch();
        }
    }

    public void IgniteTorch()
    {
        if (isLit)
        {
            return;
        }

        isLit = true;

        if (torchLight != null)
        {
            torchLight.enabled = true;
        }

        if (torchRenderer != null && litMaterial != null)
        {
            Material[] materials = torchRenderer.materials;
            materials[materialIndex] = litMaterial;
            torchRenderer.materials = materials;
        }

        if (childObjectToActivate != null)
        {
            childObjectToActivate.SetActive(true);

            if (childRenderer != null && childLitMaterial != null)
            {
                Material[] childMaterials = childRenderer.materials;

                childMaterials[0] = childLitMaterial;
                childRenderer.materials = childMaterials;
            }
        }

        UpdateSound();

        CheckAllTorchesLitStatus();
    }

    private void UpdateSound()
    {
        PLAYBACK_STATE playbackState;
        torchIgnite.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
        {
            torchIgnite.start();
        }
        else
        {
            torchIgnite.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }

    private static void CheckAllTorchesLitStatus()
    {
        if (allTorches.Count == 0)
        {
            allTorchesLit = false;
            return;
        }

        allTorchesLit = !allTorches.Any(torch => torch == null || !torch.isLit);

        if (allTorchesLit)
        {
            Debug.Log("wszystkie torche zapalone");
        }
    }
}
