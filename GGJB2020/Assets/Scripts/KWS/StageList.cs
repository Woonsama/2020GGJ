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

    void Start()
    {

        //Stage Manager
        StartCoroutine(StageManagement());
        StartCoroutine(IE_CheckESC());
    }

    public IEnumerator StageManagement()
    {
        for (int value = 0; value < stageList.Length - 1; value++)
        {
            if(value + 1 <= GameManager.instance.Get_Cleared_HighStage())
            //if(stageList[value].isCleared)
            {
                stageList[value + 1].isStageLocked = true;
            }
        }


        yield return 0;
    }

    private IEnumerator IE_CheckESC()
    {
        while(true)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

            yield return null;
        }
    }
}
