using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class vrButtonKociol : MonoBehaviour
{
    public float deadTime = 2.0f;
    private bool _deadTimeActive = false;

    public UnityEvent onPressed, onReleased;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "button" && !_deadTimeActive)
        {
            onPressed.Invoke();
            Debug.Log("pressed");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "button"&& !_deadTimeActive)
        {
            onReleased.Invoke();
            Debug.Log("realsed");
            StartCoroutine(WaitForDead());
        }
    }

    IEnumerator WaitForDead()
    {
        _deadTimeActive = true;
        yield return new WaitForSeconds(deadTime);
        _deadTimeActive = false;
    }
}
