using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class Stage
{
    public GameObject stageUI;
    public int stageNum;
    public bool isStageLocked;
}

public class StageList : MonoBehaviour
{
    public Stage[] stageList;

    void Update()
    {

    }

    public IEnumerator StageManagement()
    {
        int count = 0;

        foreach (var list in stageList)
        {
            count++;
        }

        yield return 0;
    }
}
