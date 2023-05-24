using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float lenght = 1;
    [SerializeField] LayerMask mask;
    float moveSeed = 2;
    void FixedUpdate()
    {
        rb.velocity = new Vector2(2, rb.velocity.y);
    }
    void Update()
    {
        //it cast ray just from behind mybe becus it collide with barrel first... so the ray wont be calculatred further
        RaycastHit2D hit = Physics2D.Linecast(
            transform.position - new Vector3(lenght, 0),
            transform.position + new Vector3(lenght, 0), mask.value);
        if (hit.collider)
        {
            moveSeed *= -1;
            //reverse move
        }
        Debug.DrawLine(transform.position + new Vector3(-lenght, 0), transform.position + new Vector3(lenght, 0), Color.cyan);
    }
}

