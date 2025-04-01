using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class simpleChest : MonoBehaviour
{ 
    [Header("components")]
   [SerializeField] private GameObject chest;
   [SerializeField] private Transform lid;
   [SerializeField] private float openAngle = 90f;
   [SerializeField] private float openSpeed = 2f;

   [Header("sounds")] 
   [SerializeField] private AudioSource audioSource;
   [SerializeField] private AudioClip openSound;
   
   private bool isOpen = false;
   private Quaternion initialRotation;
   private Quaternion targetRotation;
   private XRSimpleInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        if (interactable == null )
        {
            interactable = gameObject.AddComponent<XRSimpleInteractable>();
        }

        if (lid != null) //zapisz poczatkowa rotacje chest lid
        {
            initialRotation = lid.localRotation;
            targetRotation = initialRotation * Quaternion.Euler(-openAngle, 0, 0);
        }

        interactable.selectEntered.AddListener(OnInteract); // dodaj listener do interakcjii
    }

    private void OnInteract(SelectEnterEventArgs args)
    {
        if (!isOpen)
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        if (isOpen) return;

        isOpen = true;

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
    }

}
