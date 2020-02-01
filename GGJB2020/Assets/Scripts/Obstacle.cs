using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Collider2D col2d;
    Rigidbody2D rigi2d;

    // Start is called before the first frame update
    void Start()
    {
        col2d = GetComponent<Collider2D>();
        rigi2d = GetComponent<Rigidbody2D>();
    }

    public void Release_Object()
    {
        StartCoroutine(IE_Set_Kinetic());
    }

    IEnumerator IE_Set_Kinetic()
    {
        col2d.enabled = true;
        rigi2d.velocity = Vector2.zero;

        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            if (rigi2d.velocity == Vector2.zero)
            {
                rigi2d.isKinematic = true;
                yield break;
            }

            yield return null;
        }
    }
}
