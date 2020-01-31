using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Fix Check")]
    static bool isFixEnd;

    [Header("Canvas")]
    public GameObject fixCanvas;
    public GameObject ingameCanvas;

    void Start()
    {
        isFixEnd = false;
    }

    void Update()
    {
        
        StartCoroutine(CanvasCheck());
    }

    IEnumerator CanvasCheck()
    {
        if(isFixEnd)
        {
            ingameCanvas.SetActive(true);
            fixCanvas.SetActive(false);
        }
        else
        {
            ingameCanvas.SetActive(false);
            fixCanvas.SetActive(true);
        }

        yield return 0;
    }

    public static bool GetFixCheck()
    {
        return isFixEnd;
    }
    
    public static void SetFixCheck(bool value)
    {
        isFixEnd = value;
    }

}
