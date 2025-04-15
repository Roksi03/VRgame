using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TorchPuzzleManager : MonoBehaviour
{
    public List<Torch> correctSequence = new List<Torch>();

    public GameObject chest;

    public AudioClip successSound;
    public AudioClip errorSound;

    private bool puzzleSolved = false;

    private List<Torch> currentSequence = new List<Torch>();

    private AudioSource audioSource;

    public UnityEvent onPuzzleSolved;

    public float chestOpenDelay = 1.5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource= gameObject.AddComponent<AudioSource>();
        }

        foreach (var torch in correctSequence)
        {
            if (torch != null)
            {
                torch.onTorchLit.AddListener(() => TorchLit(torch));
            }
        }
    }

    public void TorchLit (Torch torch)
    {
        if (puzzleSolved)
            return;

        currentSequence.Add(torch);

        bool isCorrect = CheckSequence();

        if (!isCorrect)
        {
            ResetTorches();

            if (audioSource != null && errorSound != null)
            {
                audioSource.PlayOneShot(errorSound);
            }
        }
        else if (currentSequence.Count == correctSequence.Count)
        {
            puzzleSolved = true;

            OpenChest();

            if (audioSource != null && successSound != null)
            {
                audioSource.PlayOneShot(successSound);
            }

            onPuzzleSolved.Invoke();
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
        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (correctSequence[i] != null)
            {
                correctSequence[i].Extinguish();
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
            ChestController chestController = chest.GetComponent<ChestController>();
            if (chestController != null)
            {
                chestController.OpenChest();
            }
            else
            {
                Debug.LogWarning("skrzynia nie posiada komponentu ChestController");
            }
        }
    }
}
