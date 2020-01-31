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
    public bool isCleared;
}

public class StageList : MonoBehaviour
{
    public Stage[] stageList;

    void Update()
    {

        //Stage Manager
        StartCoroutine(StageManagement());
    }

    public IEnumerator StageManagement()
    {
        for (int value = 0; value <= stageList.Length - 1; value++)
        {
            if(stageList[value].isCleared)
            {
                stageList[value + 1].isStageLocked = true;
            }
        }


        yield return 0;
    }
}
