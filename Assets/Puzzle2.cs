using UnityEngine;

public class Puzzle2 : MonoBehaviour
{
    public static Puzzle2 Instance;
    public bool p2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("b"))
        {
            p2 = true;
        }
    }
}
