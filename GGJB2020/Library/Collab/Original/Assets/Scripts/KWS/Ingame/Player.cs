using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;

    [Header("Player Speed")]
    public float speed;
    public float acceleration;

    private float currentSpeed;
    private float targetSpeed;

    private int moveVec;

    void Start()
    {
        moveVec = 1;
        rigid = GetComponent<Rigidbody2D>();

        targetSpeed = speed;
    }

    void FixedUpdate()
    {
        currentSpeed = IncrementTowards(currentSpeed, targetSpeed, acceleration);
        //rigid.velocity += Vector2.down * moveVec * currentSpeed * Time.deltaTime;

    }

    private float IncrementTowards(float n , float target, float a)
    {
        if( n == target)
        {
            return n;
        }
        else
        {
            float dir = Mathf.Sign(target - n);
            n += a * Time.deltaTime * dir;
            return (dir == Mathf.Sign(target - n)) ? n : target;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Object")
        {
            Debug.Log("Change");
            moveVec = -moveVec;
        }
    }
}
