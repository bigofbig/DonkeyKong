using UnityEngine;

public class Player : MonoBehaviour
{
    public StateManager stateManager;
    public Rigidbody2D rb;
    public float runSpeed = 2;

    [Header("Ground Cast")]
    public Vector2 boxCastSize;//y is height and x is widht 
    public Vector2 boxCastOffset;

    void Awake()
    {
        stateManager = new StateManager(this);
        stateManager.Initialize(stateManager.idle);
    }

    void Update()
    {
        stateManager.Update();
    }
    void FixedUpdate()
    {
        stateManager.FixedUpdate();
    }

    public void SetFaceing(bool toRight)
    {
        if (toRight)
            transform.rotation = new Quaternion(0, 0, 0, 0);
        else
            transform.rotation = new Quaternion(0, 180, 0, 0);

    }
}
