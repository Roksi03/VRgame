using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PotPosition : MonoBehaviour
{
    private XRSimpleInteractable xr;

    [SerializeField] private GameObject bowl;

    [SerializeField] private Animator animator;

   
    

    bool isMoved = false;


    private void Awake()
    {
         xr = GetComponent<XRSimpleInteractable>();
        if (xr != null )
        {
            xr.selectEntered.AddListener(PotPos);
        }

       
    }
    private void OnDestroy()
    {
        if ( xr != null )
        {
            xr.selectEntered.RemoveListener(PotPos);
        }
    }

    

    private void PotPos(SelectEnterEventArgs ar)
    {
        if (animator == null) return;

        isMoved = !isMoved;
        animator.SetFloat("Moved",isMoved ? 1f:-1f);
        
    }
}
