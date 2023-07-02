using UnityEngine;

public class UIPlayChestHitSound : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;
    public void playChestHitSound()
    {
        source.PlayOneShot(clip);
    }
}
