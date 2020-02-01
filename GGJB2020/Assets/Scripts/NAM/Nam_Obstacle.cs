using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nam_Obstacle : MonoBehaviour
{
    Rigidbody2D rigi2d;

    // Start is called before the first frame update
    void Start()
    {
        rigi2d = GetComponent<Rigidbody2D>();
        StartCoroutine(IE_Set_Kinetic());
    }

    IEnumerator IE_Set_Kinetic()
    {
        yield return new WaitForSeconds(0.1f);
        while(true)
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
