using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TorchInteractable : XRSimpleInteractable
{
    [Header("torch settings")]
    public GameObject flameEffect;
    public Light torchLight;
    public AudioClip igniteSound;
    public float lightIntensity = 1.5f;

    [Header("events")]
    public UnityEvent onTorchLit;

    private bool isLit = false;
    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();

        if (audioSource == null && igniteSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (flameEffect != null)
            flameEffect.SetActive(false);

        if (torchLight != null)
        {
            torchLight.enabled = false;
            torchLight.intensity = lightIntensity;
        }

        // Sprawd� czy mamy collider
        if (GetComponent<Collider>() == null)
        {
            Debug.LogWarning("Dodaj� domy�lny collider do pochodni. Zalecane jest r�czne ustawienie odpowiedniego collidera.");
            BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        selectEntered.AddListener(OnSelectEntered);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        selectEntered.RemoveListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        LightTorch();
    }

    public void LightTorch()
    {
        if (isLit)
            return;

        isLit = true;

        if (flameEffect != null)
            flameEffect.SetActive(true);

        if (torchLight != null)
            torchLight.enabled = true;

        if (audioSource != null && igniteSound != null)
        {
            audioSource.PlayOneShot(igniteSound);
        }

        onTorchLit.Invoke();

        Debug.Log("torch is Lit " + gameObject.name);
    }

    public void Extinguish()
    {
        if (!isLit)
            return;

        isLit = false;

        if (flameEffect != null)
            flameEffect.SetActive(false);

        if (torchLight != null)
            torchLight.enabled = false;

        Debug.Log("torch got extinguised " + gameObject.name);
    }

    public bool IsLit()
    {
        return isLit;
    }
}
