using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    public int remainLives = 3;
    public static PlayerLives current;
    void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }
        current = this;
        DontDestroyOnLoad(this);
    }



}
