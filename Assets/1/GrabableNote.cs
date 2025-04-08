using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabableNote : MonoBehaviour
{
    [Header("settings")]
    [SerializeField] private GameObject noteVisual; //object notatka z tekstura lub tekstem
    [SerializeField] private Transform initialPosition; // poczatkowa pozycja kartki
    
    [Header("sounds")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip grabSound;
    [SerializeField] private AudioClip releaseSound;
    
    private XRGrabInteractable grabInteractable;
    private Rigidbody noteRigidbody;
    private bool wasGrabbed = false;

    private void Awake()
    {
        noteRigidbody = GetComponent<Rigidbody>();
        if (noteRigidbody == null)
        {
            noteRigidbody = gameObject.AddComponent<Rigidbody>();
        }

        //fizyka- konfiguracja
        noteRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        noteRigidbody.interpolation = RigidbodyInterpolation.Interpolate;

        //dodanie i konfiguracja XRGrabInteractable
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            grabInteractable = gameObject.AddComponent<XRGrabInteractable>();
        }

        grabInteractable.movementType = XRBaseInteractable.MovementType.VelocityTracking;
        grabInteractable.throwOnDetach = true;

        //dodajemy listenery do zdarzen
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);

        if (initialPosition == null)
        {
            initialPosition = new GameObject(gameObject.name + "_InitialPosition").transform;
            initialPosition.position = transform.position;
            initialPosition.rotation = transform.rotation;
            initialPosition.parent = transform.parent;
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        wasGrabbed = true;

        if (audioSource != null && grabSound != null)
        {
            audioSource.PlayOneShot(grabSound);
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if (audioSource != null && releaseSound != null)
        {
            audioSource.PlayOneShot(releaseSound);
        }
    }
}
