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
        // Odtw�rz animacj� otwierania
        if (chestAnimation != null && !string.IsNullOrEmpty(openAnimationName))
        {
            chestAnimation.Play(openAnimationName);
        }

        // W��cz efekt
        if (openEffect != null)
        {
            openEffect.SetActive(true);
        }

        // Odtw�rz d�wi�k
        if (audioSource != null && openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }
    }
}
