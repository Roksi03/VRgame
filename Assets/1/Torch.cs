using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Torch : MonoBehaviour
{

    public GameObject flameEffect;

    public Light torchLight;

    private bool isLit = false;

    public UnityEvent onTorchLit;

    private XRSimpleInteractable interactable;

    void Start()
    {
        if (flameEffect != null)
            flameEffect.SetActive(false);

        if (torchLight != null)
            torchLight.enabled = false;

        interactable = GetComponent<XRSimpleInteractable>();
        if (interactable == null)
        {
            interactable = gameObject.AddComponent<XRSimpleInteractable>();
        }

        interactable.selectEntered.AddListener((SelectEnterEventArgs args) => ToggleTorch());
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

        onTorchLit.Invoke();
    }

    public void Extinguish()
    {
        isLit = false;

        if (flameEffect != null)
            flameEffect.SetActive(false);

        if (torchLight != null)
            torchLight.enabled = false;
    }
}
