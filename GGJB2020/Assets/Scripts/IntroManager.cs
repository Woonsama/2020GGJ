using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public void UI_Button_GameStart()
    {
        SceneManager.LoadScene("Lobby");
    }
}
