using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class PlayManager : MonoBehaviour
{
    [SerializeField] GameObject _playerRigi;
    Vector2 _InitplayerPos;
    Quaternion _InitplayerRot;
    bool isMoved = false;
    
    [Header("UI")]
    [SerializeField] GameObject _btnGameStart;
    [SerializeField] GameObject _btnRestart;
    [SerializeField] GameObject _btnNextStage;
    [SerializeField] GameObject _btnLobby;
    [SerializeField] Animator _fadeAnim;
    bool isFade = true;

    [Header("FX")]
    [SerializeField] AudioClip ski;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip kia;

    AudioSource source;


    private enum GAMESTATE
    {
        READY,
        PLAY,
        CLEAR,
        DEAD
    }

    GAMESTATE _state;

    public static PlayManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();

        _InitplayerPos = _playerRigi.GetComponent<Transform>().position;
        _InitplayerRot = _playerRigi.GetComponent<Transform>().rotation;

        StartCoroutine(IE_FadeIn());
        Set_GameState_Ready();
    }

    public void Set_GameState_Ready()
    {
        _state = GAMESTATE.READY;
        source.Stop();

        //UI
        _btnGameStart.SetActive(true);
        _btnRestart.SetActive(false);
        _btnLobby.SetActive(false);
        _btnNextStage.SetActive(false);

        //위치, 회전값 초기화
        //가중력 초기화, 키네틱 활성화
        _playerRigi.GetComponent<Transform>().position = _InitplayerPos;
        _playerRigi.GetComponent<Transform>().rotation = _InitplayerRot;
        _playerRigi.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _playerRigi.GetComponent<Rigidbody2D>().isKinematic = true;

        //아이템 갯수 초기화
        for(int i = 0; i < _items.Count; i++)
        {
            _items[i].currCount = _items[i].maxCount;
            _itemCountTexts[i].text = "x" + _items[i].currCount;
        }
    }

    public void Set_GameState_Play()
    {
        _state = GAMESTATE.PLAY;

        //source.clip = ski;
        //source.Play();
        //source.loop = true;

        //UI
        _btnGameStart.SetActive(false);

        //키네틱 비활성화
        _playerRigi.GetComponent<Rigidbody2D>().isKinematic = false;

        StartCoroutine(IE_Check_FlatStarting());
        StartCoroutine(IE_Set_SyncroResourceTr());
        StartCoroutine(IE_Check_ContinousMoving());
    }

    public void Set_GameState_Clear()
    {
        _state = GAMESTATE.CLEAR;

        GameManager.instance.Update_Cleared_HighStage();       
        _btnRestart.SetActive(false);
        //다음 씬이 존재한다면 해당 버튼 활성화
        if(!SceneManager.GetActiveScene().name.Equals("Main_6"))
            _btnNextStage.SetActive(true);
        _btnLobby.SetActive(true);
    }

    public void Set_GameState_Dead()
    {
        if (_state.Equals(GAMESTATE.CLEAR)) return;
        _state = GAMESTATE.DEAD;

        source.clip = explosion;
        source.Play();
        source.loop = false;

        _btnRestart.SetActive(true);
        _btnNextStage.SetActive(false);
        _btnLobby.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Set_GameState_Dead();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Set_GameState_Clear();
        }
    }

    private IEnumerator IE_Check_FlatStarting()
    {
        Rigidbody2D rigi2d = _playerRigi.GetComponent<Rigidbody2D>();
        float pushSpeed = 3f;

        //초기 velocity가 0이기 때문에 지연시간 부여.
        yield return new WaitForSeconds(0.1f);

        float fall = 3f;
        float landing = 1f;
        //3초 이내 낙하동안 Vector2.zero가 될 때. (완벽한 평면에 착지했을 경우)
        while(fall > 0)
        {
            fall -= Time.deltaTime;

            if (rigi2d.velocity == Vector2.zero)
            {
                //완전한 평면에서 1초간 머무를 경우. 별도의 힘을 준다.
                landing -= Time.deltaTime;
                if (landing < 0) break;
            }

            yield return null;
        }
        if (fall <= 0) yield break;

        if (rigi2d.velocity == Vector2.zero)
        {
            rigi2d.velocity = new Vector2(pushSpeed, 0);
        }
    }

    private IEnumerator IE_Set_SyncroResourceTr()
    {
        Rigidbody2D rigi2d = _playerRigi.GetComponent<Rigidbody2D>();
        //SpriteRenderer render = _playerRrcRoot.transform.GetChild(0).GetComponent<SpriteRenderer>();
        SpriteRenderer render = _playerRigi.GetComponent<SpriteRenderer>();

        while (_state == GAMESTATE.PLAY)
        {
            if (Mathf.Abs(rigi2d.velocity.x) > 0.3f)
            {
                if (rigi2d.velocity.x < 0.1f) render.flipX = true;
                else if (rigi2d.velocity.x > 0.1f) render.flipX = false;
            }
            //================================== /Rotation

            yield return null;
        }
    }

    IEnumerator IE_Check_ContinousMoving()
    {
        Rigidbody2D rigi2d = _playerRigi.GetComponent<Rigidbody2D>();
        float timer = 5f;
        while(_state == GAMESTATE.PLAY)
        {
            if (rigi2d.velocity == Vector2.zero) timer -= Time.deltaTime;
            else timer = 5f;

            if (timer <= 0f) Set_GameState_Dead();
            yield return null;
        }
    }

    public void UI_Button_GameStart()
    {
        if (isFade) return;

        Set_GameState_Play();
    }

    public void UI_Button_ReStart()
    {
        if (isFade) return;

        StartCoroutine(IE_FadeOut("ReStart"));
    }

    public void UI_Button_Lobby()
    {
        if (isFade) return;

        StartCoroutine(IE_FadeOut("Lobby"));
    }

    public void UI_Button_NextStage()
    {
        if (isFade) return;
        StartCoroutine(IE_FadeOut("NextStage"));
    }

    IEnumerator IE_FadeIn()
    {
        isFade = true;
        _fadeAnim.gameObject.SetActive(true);
        _fadeAnim.Play("FadeIn");

        yield return new WaitForSeconds(1f);

        isFade = false;
        _fadeAnim.gameObject.SetActive(false);
    }

    IEnumerator IE_FadeOut(string outCode)
    {
        isFade = true;
        _fadeAnim.gameObject.SetActive(true);
        _fadeAnim.Play("FadeOut");
        yield return new WaitForSeconds(1f);

        switch (outCode)
        {
            case "ReStart":
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case "Lobby":
                SceneManager.LoadScene("Lobby");
                break;
            case "NextStage":
                GameManager.instance.currentStageIndex++;
                SceneManager.LoadScene("Main_" + GameManager.instance.currentStageIndex);
                break;
        }
        isFade = false;
        //_fadeAnim.gameObject.SetActive(false);
    }

    public void Play_FX_KIA()
    {
        source.clip = kia;
        source.Play();
    }
}
