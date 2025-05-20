using FMOD.Studio;
using FMODUnity;
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

    [SerializeField] GameObject sarko;

    [SerializeField] private GameObject soundEmitterObject;
    [SerializeField] private StudioEventEmitter sarkofagMoveEmitter;

    Vector3 startPos;
    Vector3 endPos;

    float speed = 0.7f;
    float moveP = 0f;
    bool moveS = false;

    private bool soundPlayed = false;
    private EventInstance sarkofagMove;


    private void Start()
    {
        foreach(var s in status)
        {
            s.SetActive(false);
        }

        startPos = sarko.transform.position;
        endPos = sarko.transform.position - new Vector3(0, 0, 1f);

        sarkofagMove = AudioManager.instance.CreateEventInstance(FMODEvents.instance.sarkofagMove);

        if (sarkofagMoveEmitter == null && soundEmitterObject != null)
        {
            sarkofagMoveEmitter = soundEmitterObject.GetComponent<StudioEventEmitter>();
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
         if(status.TrueForAll(s => s.activeSelf))
        {
           moveS = true;

            if (!soundPlayed)
            {
                PlaySarkofagMoveSound();
                soundPlayed = true;
            }
        }
        if (moveS && moveP < 1f)
        {
            moveP += Time.deltaTime * speed;
            sarko.transform.position = Vector3.Lerp(startPos, endPos, moveP);
        }
    }

    private void PlaySarkofagMoveSound()
    {
        if (sarkofagMoveEmitter != null && !sarkofagMoveEmitter.IsPlaying())
        {
            sarkofagMoveEmitter.Play();
            Debug.Log("playing dzwiek przesuwania sarkofagu");
        }
        else
        {
            Debug.Log("playing dzwiek przesuwania sarkofagu thru event instance");
            sarkofagMove.start();
        }
    }
}
