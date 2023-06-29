using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    void Start()
    {
        AudioManager.current.Play("Music");
        GameEvents.current.onGameOver += StopTheMusic;
    }

    void StopTheMusic()
    {
        AudioManager.current.Stop("Music");

    }
}
