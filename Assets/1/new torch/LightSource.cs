using UnityEngine;

public class LightSource : MonoBehaviour
{

    public bool canIgnite = true;

    public ParticleSystem fireEffect;

    private void Start()
    {
        if (fireEffect != null)
        {
            fireEffect.Play();
        }
    }
}
