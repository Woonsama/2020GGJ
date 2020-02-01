using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageClick : MonoBehaviour
{
    StageList stageList;

    private void Start()
    {
        stageList = GetComponent<StageList>();
    }

    public void Stage1()
    {
        GameManager.instance.currentStageIndex = 1;
        SceneManager.LoadScene("Main_1");
    }

    public void Stage2()
    {
        if(stageList.stageList[1].isStageLocked)
        {
            GameManager.instance.currentStageIndex = 2;
            SceneManager.LoadScene("Main_2");
        }
        else
        {
            Debug.Log("Can't Access");
        }
    }

    public void Stage3()
    {
        if (stageList.stageList[2].isStageLocked)
        {
            GameManager.instance.currentStageIndex = 3;
            SceneManager.LoadScene("Main_3");
        }
        else
        {
            Debug.Log("Can't Access");
        }
    }

    public void Stage4()
    {
        if (stageList.stageList[3].isStageLocked)
        {
            GameManager.instance.currentStageIndex = 4;
            SceneManager.LoadScene("Main_4");
        }
        else
        {
            Debug.Log("Can't Access");
        }
    }

    public void Stage5()
    {
        if (stageList.stageList[4].isStageLocked)
        {
            GameManager.instance.currentStageIndex = 5;
            SceneManager.LoadScene("Main_5");
        }
        else
        {
            Debug.Log("Can't Access");
        }
    }

    public void Stage6()
    {
        if (stageList.stageList[5].isStageLocked)
        {
            GameManager.instance.currentStageIndex = 6;
            SceneManager.LoadScene("Main_6");
        }
        else
        {
            Debug.Log("Can't Access");
        }
    }

}
