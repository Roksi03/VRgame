using UnityEngine;

public class Sarkofag : MonoBehaviour
{
    public GameObject keySlot;

    public Animator lidAnimator;

    public string openTriggerName = "Open";

    public float animationDuration = 1.5f;

    private bool isOpened = false;

    private void Start()
    {
        if (keySlot != null)
        {
            keySlot.SetActive(false);
        }

        if (lidAnimator == null)
        {
            lidAnimator = GetComponent<Animator>();

            if (lidAnimator == null)
            {
                // Spróbuj znaleŸæ komponent na dzieciach obiektu
                lidAnimator = GetComponentInChildren<Animator>();

                if (lidAnimator == null)
                {
                    Debug.LogError("nie dodano komponentu animator");
                }
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        InteractableObject interactable = other.GetComponent<InteractableObject>();

        if (interactable != null &&
            interactable.objectType == InteractableObject.ObjectType.Crowbar && interactable.isPickedUp && !isOpened)
        {
            OpenSarkofag();
        }
    }

    private void OpenSarkofag()
    {
        isOpened = true;

        if (lidAnimator != null)
        {
            lidAnimator.SetTrigger(openTriggerName);

            Invoke("ShowKeySlot", animationDuration);
        }
        else
        {
            Debug.LogError("nie dodano animatora");
            ShowKeySlot();
        }

        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    private void ShowKeySlot()
    {
        if (keySlot != null)
        {
            keySlot.SetActive(true);
        }
    }
}
