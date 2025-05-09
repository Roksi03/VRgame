using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class KeySlot : MonoBehaviour
{
    public GameObject wall;
    public float riseHeight = 3f;
    public float riseSpeed = 1f;

    private bool isKeyInserted = false;
    private bool isWallRising = false;
    private Vector3 wallInitialPosition;
    private Vector3 wallTargetPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (wall != null)
        {
            wallInitialPosition = wall.transform.position;
            wallTargetPosition = wallInitialPosition + new Vector3(0, riseHeight, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isWallRising && wall != null)
        {
            wall.transform.position = Vector3.Lerp(
                wall.transform.position,
                wallTargetPosition,
                Time.deltaTime * riseSpeed
                );
        }

        if (Vector3.Distance(wall.transform.position, wallTargetPosition) < 0.01f)
        {
            isWallRising = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        InteractableObject interactable = other.GetComponent<InteractableObject>();

        if (interactable != null && interactable.objectType == InteractableObject.ObjectType.Key
            && interactable.isPickedUp && !isKeyInserted)
        {
            isKeyInserted = true;

            interactable.GetComponent<Rigidbody>().isKinematic = true;
            interactable.transform.position = transform.position;
            interactable.transform.rotation = transform.rotation;

            XRGrabInteractable grabInteractable = interactable.GetComponent<XRGrabInteractable>();
            if (grabInteractable != null)
            {
                grabInteractable.enabled = false;
            }

            isWallRising = true;

            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
            }

        }
    }
}
