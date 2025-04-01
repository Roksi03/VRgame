using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class simpleChest : MonoBehaviour
{ 
    [Header("components")]
   [SerializeField] private GameObject chest;
   [SerializeField] private Transform lid;
   [SerializeField] private float openAngle = 90f;
   [SerializeField] private float openSpeed = 2f;

   [Header("sounds")] 
   [SerializeField] private AudioSource audioSource;
   [SerializeField] private AudioClip openSound;
   
   private bool isOpen = false;
   private Quaternion initialRotation;
   private Quaternion targetRotation;
   private XRSimpleInteractable interactable;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        
    }
}
