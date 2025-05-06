using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Torch : MonoBehaviour
{

    public GameObject flameEffect;

    public Light torchLight;

    public AudioClip igniteSound;

    private bool isLit = false;
    private AudioSource audioSource;

    public UnityEvent onTorchLit;

    private XRSimpleInteractable interactable;

    void Start()
    {
        if (flameEffect != null)
            flameEffect.SetActive(false);

        if (torchLight != null)
            torchLight.enabled = false;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && igniteSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        SetUpInteraction(); 
    }

    private void SetUpInteraction()
    {
        XRSimpleInteractable interactable = GetComponent<XRSimpleInteractable>();
        if (interactable == null)
        {
            interactable = gameObject.AddComponent<XRSimpleInteractable>();
        }

        interactable.selectEntered.RemoveAllListeners();

        if (GetComponent<Collider>() == null)
        {
            BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        ToggleTorch();
    }

    public void ToggleTorch()
    {
        if (!isLit)
        {
            LightTorch();
        }
    }

    public void LightTorch()
    {
        if (isLit)
            return;

        isLit = true;

        if (flameEffect != null)
            flameEffect.SetActive(true);

        if (torchLight != null)
            torchLight.enabled = true;

        if (audioSource != null && igniteSound != null)
        {
            audioSource.PlayOneShot(igniteSound);
        }

        onTorchLit.Invoke();

        Debug.Log("torch is Lit" + gameObject.name);
    }

    public void Extinguish()
    {
        isLit = false;

        if (flameEffect != null)
            flameEffect.SetActive(false);

        if (torchLight != null)
            torchLight.enabled = false;

        Debug.Log("torch got extinguised" + gameObject.name);
    }
}
