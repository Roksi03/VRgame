using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PotPosition : MonoBehaviour
{
    private XRSimpleInteractable xr;

    [SerializeField] private GameObject bowl;

    [SerializeField] private Animator animator;

    private Vector3 firstpos;
    private Vector3 lastpos = new Vector3(11, -10, 4);

    bool isMoved = false;


    private void Awake()
    {
         xr = GetComponent<XRSimpleInteractable>();
        if (xr != null )
        {
            xr.selectEntered.AddListener(PotPos);
        }

        if ( bowl != null)
        {
            firstpos = bowl.transform.position;
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

        isMoved =!isMoved;
        animator.SetFloat("Moved",isMoved ? 1f:-1f);
        
    }
}
