using UnityEngine;

public class LightTorch : MonoBehaviour
{
    public ParticleSystem fireParticles;
    public Light torchLight;

    private bool isLit = false;

    public Material litMaterial;
    public Renderer torchRenderer;
    public int materialIndex = 0;

    private void Start()
    {
        if (fireParticles != null)
        {
            fireParticles.Stop();
        }

        if (torchLight != null)
        {
            torchLight.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isLit)
        {
            return;
        }

        LightSource lightSource = other.GetComponent<LightSource>();

        if (lightSource != null && lightSource.canIgnite)
        {
            IgniteTorch();
        }
    }

    public void IgniteTorch()
    {
        if (isLit)
        {
            return;
        }

        isLit = true;

        if (fireParticles != null)
        {
            fireParticles.Play();
        }

        if (torchLight != null)
        {
            torchLight.enabled = true;
        }

        if (torchRenderer != null && litMaterial != null)
        {
            Material[] materials = torchRenderer.materials;
            materials[materialIndex] = litMaterial;
            torchRenderer.materials = materials;
        }
    }
}
