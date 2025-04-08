using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class wALLsYMBOLS : MonoBehaviour
{
    public GameObject[]symbol;

    public Transform[] point;

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
            StartCoroutine(spawn());


        }
    }

    private IEnumerator spawn()
    {
        int c = Mathf.Min(symbol.Length, point.Length);

        for(int i = 0; i < c; i++)
        {
            GameObject newSymbol = Instantiate(symbol[i], point[i].position, Quaternion.identity);
            yield return new WaitForSeconds(1);
            Destroy(newSymbol);
            yield return new WaitForSeconds(1);
        }
       


        
        
      
    }
}
