using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MainConsol : MonoBehaviour
{
    [SerializeField] private List<GameObject> status;


    [SerializeField]private DoSomething doSomething;
    [SerializeField] private Puzzle2 puzzle2;

    [SerializeField] private int torchStatusIndex = 2;
    private bool torchPuzzleCompleted = false;

    private void Start()
    {
        foreach(var s in status)
        {
            s.SetActive(false);
        }
    }

    private void Update()
    {
        if (doSomething.Good == true)
        {
            if (!status[0].activeSelf)
                status[0].SetActive(true);
        }
         if (puzzle2.p2 == true)
        {
            if (!status[1].activeSelf)
                status[1].SetActive(true);
        }

         if (!torchPuzzleCompleted)
        {
            if (LightTorch.AreAllTorchesLit())
            {
                torchPuzzleCompleted = true;

                if (torchStatusIndex < status.Count)
                {
                    status[torchStatusIndex].SetActive(true);
                    Debug.Log("updated konsola po zagadce z pochodniami");
                }
            }
        }
    }
}
