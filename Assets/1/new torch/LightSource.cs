using UnityEngine;

public class LightSource : MonoBehaviour
{

    public bool canIgnite = true;
    public GameObject fire;


    private void Start()
    {
        if (fire != null)
        {
            fire.SetActive(true);
        }
    }
}
