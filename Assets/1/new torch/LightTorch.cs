using UnityEngine;

public class LightTorch : MonoBehaviour
{
    public Light torchLight;
    public GameObject childObjectToActivate;

    private bool isLit = false;

    public Material litMaterial;
    public Renderer torchRenderer;
    public int materialIndex = 0;

    public Material childLitMaterial;
    public Renderer childRenderer;

    private void Start()
    {

        if (torchLight != null)
        {
            torchLight.enabled = false;
        }

        if (childObjectToActivate != null)
        {
            childObjectToActivate.SetActive(false);

            if (childRenderer == null)
            {
                childRenderer = childObjectToActivate.GetComponent<Renderer>();
            }
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

        if (childObjectToActivate != null)
        {
            childObjectToActivate.SetActive(true);

            if (childRenderer != null && childLitMaterial != null)
            {
                Material[] childMaterials = childRenderer.materials;

                childMaterials[0] = childLitMaterial;
                childRenderer.materials = childMaterials;
            }
        }
    }
}
