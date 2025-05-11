using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Puzzle2 : MonoBehaviour
{
    public static Puzzle2 Instance;
    public bool p2;

    private XRSocketInteractor socket;

    private void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
        if (socket != null)
        {
            socket.selectEntered.AddListener(Bowl);
            socket.selectExited.AddListener(exitB);
        }
    }

    private void Bowl(SelectEnterEventArgs e)
    {
        p2 = true;
    }
     private void exitB(SelectExitEventArgs e)
    {
        p2 = false;
    }
    private void OnDestroy()
    {
        if (socket != null)
        {
            socket?.selectEntered.RemoveListener(Bowl);
            socket.selectExited.RemoveListener(exitB);
        }
    }
}
