using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PotPosition : MonoBehaviour
{
    


    [SerializeField] private Animator animator;

   
    

    bool isMoved = false;


   
 

    

    public void PotPos()
    {
        Debug.Log("animacja");
        if (animator == null) return;

        isMoved = !isMoved;
        animator.SetBool("moved", isMoved);
        animator.Update(0f);



    }
}
