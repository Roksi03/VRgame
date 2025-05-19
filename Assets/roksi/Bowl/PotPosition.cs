using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PotPosition : MonoBehaviour
{
    


    [SerializeField] private Animator animator;

    private XRSimpleInteractable interactablee;

   
    

    bool isMoved = false;
    private void Awake()
    {
       interactablee = GetComponent<XRSimpleInteractable>();

    }


    private void OnEnable()
    {
        interactablee.selectEntered.AddListener(PotPos);
    }

    private void OnDisable()
    {
        interactablee.selectEntered.RemoveListener(PotPos);
    }




    public void PotPos(SelectEnterEventArgs arg)
    {
        Debug.Log("animacja");
        if (animator == null) return;

        isMoved = !isMoved;
        animator.SetBool("moved", isMoved);
        animator.Update(0f);



    }
}
