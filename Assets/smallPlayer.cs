using UnityEngine;

public class smallPlayer : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;

  
    private void OnTriggerEnter(Collider other)
    {
        if (characterController != null && other.CompareTag("Player")){
            characterController.height = 0.77f;
        }
    }
}


