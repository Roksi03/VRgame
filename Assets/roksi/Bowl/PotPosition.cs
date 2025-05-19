using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PotPosition : MonoBehaviour
{


    [SerializeField] private Animator animator; 
    private XRSimpleInteractable interactablee;

    [SerializeField] private GameObject wallBowl; 

    private float height = 0.200f;
    private float wallSpeed = 1.0f;

    private Vector3 startPos;
    private Vector3 endPos;

    private float moveProgress = 0f;
    private bool isWallMoving = false;
    private bool wallIsRaised = false;
    private bool bowlMoved = false;
    private bool hasAutoAnimatedBowl = false;

    private void Awake()
    {
        interactablee = GetComponent<XRSimpleInteractable>();
    }

    private void Start()
    {
        if (wallBowl != null)
        {
            startPos = wallBowl.transform.position;
            endPos = startPos + new Vector3(0, height, 0);
        }
    }

    private void OnEnable()
    {
        interactablee.selectEntered.AddListener(PotPos);
    }

    private void OnDisable()
    {
        interactablee.selectEntered.RemoveListener(PotPos);
    }

    private void Update()
    {
        if (isWallMoving)
        {
            moveProgress += Time.deltaTime * wallSpeed;
            wallBowl.transform.position = Vector3.Lerp(startPos, endPos, moveProgress);

            if (moveProgress >= 1f)
            {
                wallBowl.transform.position = endPos;
                isWallMoving = false;
                wallIsRaised = true;

               
                if (!hasAutoAnimatedBowl && animator != null)
                {
                    bowlMoved = true;
                    animator.SetBool("moved", true);
                    hasAutoAnimatedBowl = true;
                    Debug.Log("Autoanimacja miski po ruchu œciany.");
                }
            }
        }
    }

    public void PotPos(SelectEnterEventArgs arg)
    {
        if (!wallIsRaised && !isWallMoving)
        {
           
            moveProgress = 0f;
            isWallMoving = true;
            Debug.Log("Rozpoczynam podnoszenie œciany...");
        }
        else if (wallIsRaised)
        {
           
            bowlMoved = !bowlMoved;
            if (animator != null)
            {
                animator.SetBool("moved", bowlMoved);
                Debug.Log("Klikniêcie po podniesieniu — zmiana stanu animacji miski: " + bowlMoved);
            }
        }
    }
}