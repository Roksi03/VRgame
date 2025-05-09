using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class InteractableObject : MonoBehaviour
{
    public enum ObjectType { Crowbar, Key, Other }

    public ObjectType objectType;
    public bool isPickedUp = false;

    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            grabInteractable = gameObject.AddComponent<XRGrabInteractable>();
        }

        grabInteractable.selectEntered.AddListener(OnPickUp);
        grabInteractable.selectExited.AddListener(OnPutDown);
    }

    private void OnPickUp(SelectEnterEventArgs args)
    {
        isPickedUp = true;
    }

    private void OnPutDown(SelectExitEventArgs args)
    {
        isPickedUp = false;
    }

}
