using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool current;
    public GameObject flame;

    void Awake()
    {
        current = this;
    }
}
