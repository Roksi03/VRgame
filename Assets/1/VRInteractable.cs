using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;


public class VRInteractable : MonoBehaviour
{
    // Event wywo�ywany przy interakcji z obiektem
    public UnityEvent onInteract;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;

    private void Start()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        if (interactable == null)
        {
            interactable = gameObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        }

        interactable.selectEntered.AddListener((SelectEnterEventArgs args) => {
            onInteract.Invoke();
        });
    }
}
