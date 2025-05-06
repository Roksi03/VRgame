using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MainConsol : MonoBehaviour
{
    [SerializeField] private List<GameObject> status;


    [SerializeField]private DoSomething doSomething;
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
            for (int i = 0; i < status.Count; i++)
            {
                status[0].SetActive(true);
            }
        }
    }
}
