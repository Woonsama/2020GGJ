using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nam_Player : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer.Equals(LayerMask.NameToLayer("Obstacle")))
        {
            //transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);

            Rigidbody2D rigid2d = transform.GetComponent<Rigidbody2D>();
            Vector2 rigi2dVelocity = rigid2d.velocity;

            rigid2d.velocity = new Vector2(-rigi2dVelocity.x, rigi2dVelocity.y);
        }
    }
}
