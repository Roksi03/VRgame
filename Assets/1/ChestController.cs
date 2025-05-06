using UnityEngine;

public class ChestController : MonoBehaviour
{
    public Animation chestAnimation;

    public string openAnimationName = "ChestOpen";

    public GameObject openEffect;

    public AudioClip openSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (openEffect != null)
        {
            openEffect.SetActive(false);
        }
    }

    public void OpenChest()
    {
        // Odtwórz animacjê otwierania
        if (chestAnimation != null && !string.IsNullOrEmpty(openAnimationName))
        {
            chestAnimation.Play(openAnimationName);
        }

        // W³¹cz efekt
        if (openEffect != null)
        {
            openEffect.SetActive(true);
        }

        // Odtwórz dŸwiêk
        if (audioSource != null && openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }
    }
}
