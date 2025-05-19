using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MainConsol : MonoBehaviour
{
    [SerializeField] private List<GameObject> status;


    [SerializeField]private DoSomething doSomething;
    [SerializeField] private Puzzle2 puzzle2;
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
    }
}
