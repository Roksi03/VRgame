using UnityEngine;

public class Sarkofag : MonoBehaviour
{
    public GameObject keySlot;

    public Animation lidAnimation;

    public string openAnimationName = "sarkofagLid";

    private bool isOpened = false;

    private void Start()
    {
        if (keySlot != null)
        {
            keySlot.SetActive(false);
        }

        if (lidAnimation == null)
        {
            lidAnimation = GetComponent<Animation>();

            if (lidAnimation == null)
            {
                Debug.LogError("nie ma komponentu animator na sarkofagu");
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

        if (lidAnimation != null && lidAnimation.GetClip(openAnimationName) != null)
        {
            lidAnimation.Play(openAnimationName);

            float animationLength = lidAnimation.GetClip(openAnimationName).length;
            Invoke("ShowKeySlot", animationLength);
        }
        else
        {
            Debug.LogError("brak animacji " + openAnimationName);

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
        keySlot.SetActive(true);
    }
}
