using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager s_instance;
    public static GameManager instance


    {
        get
        {
            if (!s_instance)
            {
                s_instance = FindObjectOfType(typeof(GameManager)) as GameManager;
                if (!s_instance)
                {
                    Debug.LogError("GameManager s_instance null");
                    return null;
                }
            }

            return s_instance;
        }
    }

    AudioSource source;

    [Header("FX")]
    public static AudioClip kia;

    [SerializeField] Dictionary<string, AudioClip> clipFX = new Dictionary<string, AudioClip>();
    [SerializeField] Dictionary<string, AudioClip> clipBGM = new Dictionary<string, AudioClip>();


    void Awake()
    {
        InitFXList();
        InitBGMList();

        if (s_instance == null)
        {
            s_instance = this;

            DontDestroyOnLoad(this);

        }
        else if (this != s_instance)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public int currentStageIndex = 0;
    [SerializeField] ParticleSystem _goalEffect;

    public void InitFXList()
    {
        clipFX.Add("Kia", kia);
    }

    public void InitBGMList()
    {
        
    }

    public AudioClip GetFX(string key)
    {
        return clipFX[key];
    }

    public AudioClip GetBGM(string key)
    {
        return clipBGM[key];
    }

    public void Initialize_Cleared_HighStage()
    {
        PlayerPrefs.SetInt("Cleared_HighStage", 0);
    }

    public int Get_Cleared_HighStage()
    {
        if (!PlayerPrefs.HasKey("Cleared_HighStage")) Initialize_Cleared_HighStage();
        int result = PlayerPrefs.GetInt("Cleared_HighStage");
        return result;
    }

    public void Update_Cleared_HighStage()
    {
        PlayerPrefs.SetInt("Cleared_HighStage", currentStageIndex);
    }

    public void Play_GoalEffect(Vector2 pos)
    {
        _goalEffect.transform.position = pos;
        _goalEffect.Play();
    }
}
