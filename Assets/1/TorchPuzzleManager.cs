using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TorchPuzzleManager : MonoBehaviour
{
    [Header("puzzle configuration")]
    public List<TorchInteractable> correctSequence = new List<TorchInteractable>();
    public GameObject chest;
    public float chestOpenDelay = 1.5f;

    [Header("audio")]
    public AudioClip successSound;
    public AudioClip errorSound;
    public float volumeScale = 1.0f;

    [Header("debug")]
    public bool showDebugMessages = true;

    [Header("events")]
    public UnityEvent onPuzzleSolved;
    public UnityEvent onWrongSequence;

    private bool puzzleSolved = false;

    private List<TorchInteractable> currentSequence = new List<TorchInteractable>();

    private AudioSource audioSource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource= gameObject.AddComponent<AudioSource>();
        }

        RegisterTorchListeners();

        if (showDebugMessages)
        {
            Debug.Log($"puzzle zainicjalizowany. oczekiwana sekwencja: {correctSequence.Count} pochodni.");
        }
    }

    void RegisterTorchListeners()
    {
        foreach (var torch in currentSequence)
        {
            if (torch != null)
            {
                torch.onTorchLit.RemoveAllListeners();

                torch.onTorchLit.AddListener(() => TorchLit(torch));

                if (showDebugMessages)
                {
                    Debug.Log($"Zarejestrowano listener dla pochodni: {torch.gameObject.name}");
                }
            }
            else
            {
                Debug.LogError("Znaleziono pust¹ referencjê w sekwencji pochodni!");
            }
        }
    }

    public void TorchLit (TorchInteractable torch)
    {
        if (puzzleSolved)
            return;
        if (showDebugMessages)
        {
            Debug.Log("zapalono pochodnie: {torch.gameObject.name}");
        }

        currentSequence.Add(torch);

        bool isCorrect = CheckSequence();

        if (!isCorrect)
        {
            if (showDebugMessages)
            {
                Debug.Log("nieprawidlowa sekwencja, reset pochodni");
            }


            ResetTorches();

            if (audioSource != null && errorSound != null)
            {
                audioSource.PlayOneShot(errorSound, volumeScale);
            }
        }
        else if (currentSequence.Count == correctSequence.Count)
        {
            puzzleSolved = true;

            if (showDebugMessages)
            {
                Debug.Log("zagadka rozwi¹zana, otwieranie skrzyni...");
            }

            if (audioSource != null && successSound != null)
            {
                audioSource.PlayOneShot(successSound);
            }

            onPuzzleSolved.Invoke();

            OpenChest();
        }
    }

    private bool CheckSequence()
    {
        if (currentSequence.Count > correctSequence.Count)
            return false;

        for (int i = 0; i < currentSequence.Count; i++)
        {
            if (currentSequence[i] != correctSequence[i])
                return false;
        }

        return true;
    }

    private void ResetTorches()
    {
        foreach (var torch in correctSequence)
        {
            if (torch != null)
            {
                torch.Extinguish();
            }
        }

        currentSequence.Clear();
    }

    private void OpenChest()
    {
        if (chest != null)
        {
            StartCoroutine(OpenChestWithDelay());
        }
    }

    private IEnumerator OpenChestWithDelay()
    {
        yield return new WaitForSeconds(chestOpenDelay);

        if (chest != null)
        {
            // SprawdŸ ró¿ne mo¿liwe komponenty kontroluj¹ce skrzyniê
            ChestController chestController = chest.GetComponent<ChestController>();
            if (chestController != null)
            {
                chestController.OpenChest();
            }
            else
            {
                // Próba znalezienia innych komponentów, które mog¹ otwieraæ skrzyniê
                Animator animator = chest.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.SetTrigger("Open");
                }
                else
                {
                    Debug.LogWarning("Nie znaleziono komponentu do otwierania skrzyni. Dodaj ChestController lub skonfiguruj Animator.");
                }
            }
        }
    }

    public void DebugResetPuzzle()
    {
        puzzleSolved = false;
        ResetTorches();
        Debug.Log("Zagadka zresetowana przez debugowanie.");
    }
}
