using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PenguinTip : MonoBehaviour
{
    [Header("Tip")]
    [SerializeField] private string[] tip;
    public float tipDelay;
    private float t;
    string tipTemp;
    public Text penguinText;

    void Start()
    {
        t = 0;
        tipTemp = tip[Random.Range(0, tip.Length)];
        penguinText.text = tipTemp;
    }

    void Update()
    {
        DelayCheck();
    }

    public void DelayCheck()
    {
        if(t >= tipDelay)
        {
            tipTemp = tip[Random.Range(0, tip.Length)];
            penguinText.text = tipTemp;
            t = 0;
        }
        else
        {
            t += Time.deltaTime;
        }
    }
}
