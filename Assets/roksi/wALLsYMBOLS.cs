using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class wALLsYMBOLS : MonoBehaviour
{
    public GameObject symbol;

    public Transform point;

    private XRSimpleInteractable interactable;

    private void Awake()
    {
        var interactable = GetComponent<XRSimpleInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(symbolsSpawn);
        }
    }

    private void OnDestroy()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(symbolsSpawn);
        }
    }
    private void symbolsSpawn(SelectEnterEventArgs a)
    {
        if (symbol != null)
        {
            Instantiate(symbol, point.position, Quaternion.identity);
            StartCoroutine(spawn());
        }
    }

    private IEnumerator spawn()
    {
        yield return new WaitForSeconds(3);
        if(symbol != null)
        {
            Destroy(symbol);
        }
      
    }
}
