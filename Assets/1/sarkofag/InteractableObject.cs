using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class InteractableObject : MonoBehaviour
{
    public enum ObjectType { Crowbar, Key, Other }

    public ObjectType objectType;
    public bool isPickedUp = false;

    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            grabInteractable = gameObject.AddComponent<XRGrabInteractable>();
        }

        grabInteractable.throwOnDetach = true;
        grabInteractable.retainTransformParent = false;

        grabInteractable.trackPosition = true;
        grabInteractable.smoothPosition = true;

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
