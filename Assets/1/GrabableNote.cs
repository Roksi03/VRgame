using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabableNote : MonoBehaviour
{
    [Header("Interakcja")]
    [SerializeField] private bool canPickUp = true;
    [SerializeField] private bool snapToHand = true;

    private XRGrabInteractable grabInteractable;
    private Rigidbody noteRigidbody;

    private void Start()
    {
        // Upewnij siê, ¿e obiekt ma Rigidbody
        noteRigidbody = GetComponent<Rigidbody>();
        if (noteRigidbody == null)
        {
            noteRigidbody = gameObject.AddComponent<Rigidbody>();
            Debug.Log("Added Rigidbody to note");
        }

        // Konfiguracja fizyki
        noteRigidbody.useGravity = true;
        noteRigidbody.isKinematic = false;
        noteRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        noteRigidbody.interpolation = RigidbodyInterpolation.Interpolate;

        // Upewnij siê, ¿e obiekt ma collider
        if (GetComponent<Collider>() == null)
        {
            BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
            boxCollider.size = new Vector3(0.2f, 0.001f, 0.3f);
            Debug.Log("Added BoxCollider to note");
        }

        // Dodaj i skonfiguruj XRGrabInteractable
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            grabInteractable = gameObject.AddComponent<XRGrabInteractable>();
            Debug.Log("Added XRGrabInteractable to note");
        }

        // Konfiguracja interakcji dla XRGrabInteractable
        grabInteractable.movementType = snapToHand ?
            XRBaseInteractable.MovementType.Instantaneous :
            XRBaseInteractable.MovementType.VelocityTracking;

        grabInteractable.throwOnDetach = true;
        grabInteractable.trackPosition = true;
        grabInteractable.trackRotation = true;
        grabInteractable.retainTransformParent = false;

        // W³¹czenie/Wy³¹czenie mo¿liwoœci podniesienia
        grabInteractable.enabled = canPickUp;

        // Dodaj listenery do zdarzeñ
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);

        Debug.Log("GrabableNote script initialized");
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("Note grabbed!");
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log("Note released!");
    }
}