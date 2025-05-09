using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class GrabbableLightSource : MonoBehaviour
{
    public LightSource lightSource;

    private XRGrabInteractable grabInteractable;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (lightSource == null)
        {
            lightSource = GetComponent<LightSource>();
        }

        if (lightSource == null)
        {
            Debug.LogError("nie ma komponentu lightSource");
        }

        if (grabInteractable == null)
        {
            Debug.LogError("nie ma komponentu XRGrabInteractable");
        }
    }
}
