using System.Collections;
using UnityEngine;

public class Flame : MonoBehaviour
{
    //random going left and right 
    //ranadom decide to giong up nad down the ladders 
    Rigidbody2D rb;
    [SerializeField] float moveSpeed = 70;
    enum States { Wandering, Climb }
    States state = States.Wandering;

    float bound = 9;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(nameof(DirectionRandomizer));
    }
    void FixedUpdate()
    {
        switch (state)
        {
            case States.Wandering:
                SetDirectionBasedOnBoundry();
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y) * Time.deltaTime;
                break;
            case States.Climb:
                break;
        }
    }

    void SetDirectionBasedOnBoundry()
    {
        if (transform.position.x > bound)
        {
            Debug.Log("BVoudn");
            moveSpeed = Mathf.Abs(moveSpeed * -1);
        }
        else if (transform.position.x < -bound)
        {
            Debug.Log("BVoudn");
            moveSpeed = Mathf.Abs(moveSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Leader"))
        {
            //decicde to go up or no
            Debug.Log("Ladder for fire");
        }
    }
    IEnumerator DirectionRandomizer()
    {
        yield return new WaitForSeconds(Random.Range(0, 15));
        moveSpeed *= -1;
        StartCoroutine(nameof(DirectionRandomizer));
    }
}
