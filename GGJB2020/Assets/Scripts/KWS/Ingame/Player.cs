using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    ParticleSystem _ps;

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
        _ps = GetComponent<ParticleSystem>();
        _ps.Stop();

        targetSpeed = speed;
    }

    void FixedUpdate()
    {
        //currentSpeed = IncrementTowards(currentSpeed, targetSpeed, acceleration);
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
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Default")))
        {
            if (collision.collider.tag.Equals("Untagged"))
            {
                _ps.Play();
            }
        }

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Obstacle")))
        {
            if(collision.collider.tag.Equals("Mirror"))
            {
                float dir = transform.position.x - collision.transform.position.x;
                if (dir < 0) dir = -1;
                else dir = 1;

                float force = 300;
                Vector2 revDir = new Vector2(3 * dir, 0);
                revDir.Normalize();
                
                rigid.velocity = Vector2.zero;
                rigid.AddForce(revDir * force);
                collision.collider.tag = "Untagged";
            }
        }

        if (collision.collider.tag == "Object")
        {
            Debug.Log("Change");
            moveVec = -moveVec;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer.Equals(LayerMask.NameToLayer("Default")))
        {
            if (collision.collider.tag.Equals("Untagged"))
            {
                _ps.Stop();
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("DeadLine"))
        {
            PlayManager.instance.Set_GameState_Dead();
        }
        else if (col.tag.Equals("ClearLine"))
        {
            PlayManager.instance.Play_FX_KIA();
            GameManager.instance.Play_GoalEffect(transform.position);
            col.gameObject.SetActive(false);
            PlayManager.instance.Set_GameState_Clear();
        }
    }

}
