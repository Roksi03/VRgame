using UnityEngine;

public class Sarkofag : MonoBehaviour
{
    public Transform lidTransform;
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public GameObject keySlot;

    private bool isOpening = false;
    private bool isOpened = false;
    private float currentAngle = 0f;
    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = lidTransform.rotation;
        if (keySlot != null)
        {
            keySlot.SetActive(false);
        }
    }

    private void Update()
    {
        if (isOpening && !isOpened)
        {
            currentAngle += Time.deltaTime * openSpeed;
            if (currentAngle >= openAngle)
            {
                currentAngle = openAngle;
                isOpened = true;
                
                if (keySlot != null)
                {
                    keySlot.SetActive(true);
                }
            }

            lidTransform. localRotation = initialRotation * Quaternion.Euler(currentAngle, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        InteractableObject interactable = other.GetComponent<InteractableObject>();

        if (interactable != null && interactable.objectType == InteractableObject.ObjectType.Crowbar && interactable.isPickedUp && !isOpening)
        {
            isOpening = true;

            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }
}
