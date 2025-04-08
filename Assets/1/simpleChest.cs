using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class simpleChest : MonoBehaviour
{ 
   [Header("components")]
   [SerializeField] private Transform lid;
   [SerializeField] private float openAngle = 90f;
   [SerializeField] private float openSpeed = 2f;

   [Header("sounds")] 
   [SerializeField] private AudioSource audioSource;
   [SerializeField] private AudioClip openSound;
   
   [SerializeField] private Vector3 rotationAxis = new Vector3(-1, 0, 0);

   private bool isOpen = false;
   private Quaternion initialRotation;
   private Quaternion targetRotation;
   private XRGrabInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<XRGrabInteractable>();
        if (interactable == null )
        {
            interactable = gameObject.AddComponent<XRGrabInteractable>();

            interactable.movementType = XRBaseInteractable.MovementType.Instantaneous;
            interactable.trackPosition = false;
            interactable.trackRotation = false;
        }

        if (lid != null) //zapisz poczatkowa rotacje chest lid
        {
            initialRotation = lid.localRotation;
            targetRotation = initialRotation * Quaternion.Euler(rotationAxis.normalized * openAngle);
        }
        else
        {
            Debug.Log("lid transform is not assigned");
        }

        interactable.selectEntered.AddListener(OnInteract); // dodaj listener do interakcjii

        if (GetComponent<Collider>() == null)
        {
            BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();

            boxCollider.size = new Vector3(1f, 0.5f, 1f);
            boxCollider.center = new Vector3(0f, 0.25f, 0f);
        }

        if (GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
        }

        Debug.Log("simpleChest initialized, interactable setup complete");
    }

    private void OnInteract(SelectEnterEventArgs args)
    {
        Debug.Log("chest interaction detected");

        if (!isOpen)
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        if (isOpen) return;

        isOpen = true;

        Debug.Log("opening chest...");

        if (audioSource != null && openSound != null )
        {
            audioSource.PlayOneShot(openSound);
        }

        StartCoroutine(AnimateOpen()); //start animacji otwierania
    }
    
    private IEnumerator AnimateOpen()
    {
        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime * openSpeed;
            if (lid != null)
            {
                lid.localRotation = Quaternion.Slerp(initialRotation, targetRotation, time);
            }
            yield return null;
        }

        Debug.Log("chest fully opened");
    }

}
