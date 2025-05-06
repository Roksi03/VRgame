using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ButtonSpawning : MonoBehaviour
{
    public GameObject SpawnPrefab;

    public Vector3 offset = new Vector3(0.5f,0,0);

    private XRSimpleInteractable interactable;

    private void Awake()
    {
        var interactable = GetComponent<XRSimpleInteractable>();
        if (interactable != null )
        {
            interactable.selectEntered.AddListener(spawnObject);
        }
    }

    private void OnDestroy()
    {
        if ( interactable != null)
        {
            interactable.selectEntered.RemoveListener(spawnObject);
        }
    }


    private void spawnObject(SelectEnterEventArgs args)
    {
        if(SpawnPrefab != null)
        {
            Vector3 spawnPosition = transform.position + offset;
            Instantiate(SpawnPrefab,spawnPosition, Quaternion.identity);
        }
    }

}
