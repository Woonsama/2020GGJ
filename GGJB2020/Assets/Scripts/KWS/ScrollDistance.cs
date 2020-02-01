using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollDistance : MonoBehaviour
{
    [Header("FinishPos")]
    public float upFinishPos;
    public float downFinishPos;

    Vector3 firstPos;
    RectTransform tr;


    void Start()
    {
        tr = GetComponent<RectTransform>();
        tr.transform.localPosition = new Vector3(tr.transform.localPosition.x, -1700, tr.transform.localPosition.z);
        firstPos = tr.transform.localPosition;

        Debug.Log(tr.transform.localPosition.y);
    }
    void Update()
    {
        if (tr.transform.localPosition.y <= upFinishPos)
        {
            tr.transform.localPosition = new Vector3(tr.transform.localPosition.x,upFinishPos,tr.transform.localPosition.z);
        }

        if(tr.transform.localPosition.y >= downFinishPos)
        {
            tr.transform.localPosition = new Vector3(tr.transform.localPosition.x, downFinishPos, tr.transform.localPosition.z);

        }
    }
}
