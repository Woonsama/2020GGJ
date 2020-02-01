using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nam_PlayManager : MonoBehaviour
{
    [SerializeField] GameObject _playerRigi;
    [SerializeField] GameObject _playerRrc;
    Vector2 _InitplayerPos;
    Quaternion _InitplayerRot;

    [Header("UI")]
    [SerializeField] GameObject _btnGameStart;

    private enum GAMESTATE
    {
        READY,
        PLAY,
        CLEAR,
        DEAD
    }

    GAMESTATE _state;

    private void Start()
    {
        _InitplayerPos = _playerRigi.GetComponent<Transform>().position;
        _InitplayerRot = _playerRigi.GetComponent<Transform>().rotation;

        Set_GameState_Ready();
    }

    public void Set_GameState_Ready()
    {
        StopAllCoroutines();

        _state = GAMESTATE.READY;

        //UI
        _btnGameStart.SetActive(true);

        //위치, 회전값 초기화
        //가중력 초기화, 키네틱 활성화
        _playerRigi.GetComponent<Transform>().position = _InitplayerPos;
        _playerRigi.GetComponent<Transform>().rotation = _InitplayerRot;
        _playerRigi.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _playerRigi.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    public void Set_GameState_Play()
    {
        _state = GAMESTATE.PLAY;

        //UI
        _btnGameStart.SetActive(false);

        //키네틱 비활성화
        _playerRigi.GetComponent<Rigidbody2D>().isKinematic = false;

        StartCoroutine(IE_MoveForward());
        StartCoroutine(IE_Set_SyncroResourceTr());
    }

    public void Set_GameState_Clear()
    {
        _state = GAMESTATE.CLEAR;

    }

    public void Set_GameState_Dead()
    {
        _state = GAMESTATE.DEAD;

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Set_GameState_Ready();
        }
    }

    private IEnumerator IE_MoveForward()
    {
        Rigidbody2D rigi2d = _playerRigi.GetComponent<Rigidbody2D>();
        float pushSpeed = 3f;

        //초기 velocity가 0이기 때문에 지연시간 부여.
        yield return new WaitForSeconds(0.1f);

        while(_state == GAMESTATE.PLAY)
        {
            if(rigi2d.velocity == Vector2.zero || rigi2d.velocity == new Vector2(pushSpeed, 0))
            {
                rigi2d.velocity = new Vector2(pushSpeed, 0);
            }

            yield return null;
        }
    }

    private IEnumerator IE_Set_SyncroResourceTr()
    {
        Rigidbody2D rigi2d = _playerRigi.GetComponent<Rigidbody2D>();
        Vector2 prevPos = _playerRrc.transform.position;
        Vector2 currPos;
        Vector2 lookDir;

        while(_state == GAMESTATE.PLAY)
        {
            //Syncronize Position
            _playerRrc.transform.position = _playerRigi.transform.position;
            //================================= /Position

            //Syncronize Rotation
            currPos = _playerRrc.transform.position;
            lookDir = currPos - prevPos;
            lookDir.Normalize();

            float rot_z = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            float rot_temp = rot_z - 90;
            if (rot_temp > -90) rot_temp = -90;
            if (rot_z != 0)
            {
                if (rigi2d.velocity.x == 0) rot_temp = -90;
                _playerRrc.transform.rotation = Quaternion.Euler(0f, 0f, rot_temp);
            }

            prevPos = currPos;
            //================================== /Rotation

            yield return null;
        }
    }
}
