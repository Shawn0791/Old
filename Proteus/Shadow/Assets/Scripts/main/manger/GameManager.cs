using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameSpeed
{
    Stop,
    Slow,
    Normal,
    Fast,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Image imgDie;

    public Image imgWin;

    public Character Player;

    public float EffectTime = 2.5f;

    private float _effectTimer = -1;

    public Transform BornPoint;

    public static bool IsGameRunning = true;

    public CameraController Cam;
    private string _currentSceneName;

    public MainUI m_MainUI;
    public GameSpeed m_CurGameSpeed { get; private set; }
    public GameSpeed m_CacheGameSpeed { get; private set; }


    /**
    * 获得合法的近处平面的Z坐标
    * */
    public float GetValidNearPlane()
    {
        return 0;
    }

    /**
     * 获得合法的远方平面的Z坐标
     * */
    public float GetValidFarPlane()
    {
        return 7;
    }

    /**
     * 获得合法的另一个平面的Z坐标
     * */
    public float GetOtherPlane()
    {
        float playerZ = Player.transform.position.z;
        if (Mathf.Abs(playerZ - GetValidNearPlane()) < 3.5f)
        {
            return GetValidFarPlane();
        }

        if (Mathf.Abs(playerZ - GetValidFarPlane()) < 3.5f)
        {
            return GetValidNearPlane();
        }

        return GetValidFarPlane();
    }

    public void Awake()
    {
        Instance = this;
        m_CurGameSpeed = GameSpeed.Normal;
        Mgr_AssetBundle.Instance.Init();
        Mgr_UI.Instance.Init();
        Mgr_Sound.Instance.Init();
        m_MainUI.Init();
        //Mgr_UI.Instance.ToUI<UI_Start>(UIName.UI_Start);
    }

    public void Start()
    {
        Player.transform.position = BornPoint.position;
        IsGameRunning = true;
        _currentSceneName = SceneManager.GetActiveScene().name;
    }

    public void OnPlayWin()
    {
        if (!IsGameRunning)
        {
            return;
        }

        _effectTimer = EffectTime;
        imgWin.gameObject.SetActive(true);
        Player.Move.MoveEnabled = false;
        IsGameRunning = false;
        Cam.LooseTarget();
    }

    public void OnPlayDie()
    {
        if (!IsGameRunning)
        {
            return;
        }

        _effectTimer = EffectTime;
        imgDie.gameObject.SetActive(true);
        IsGameRunning = false;
        Cam.LooseTarget();
    }

    public void Update()
    {
        //return;
        if (_effectTimer > -1)
        {
            _effectTimer -= Time.deltaTime;

            if (_effectTimer >= 0)
            {
                float f = 1 - _effectTimer / EffectTime;
                if (imgWin.gameObject.activeSelf)
                {
                    Color color1 = imgWin.color;
                    color1.a = f;
                    imgWin.color = color1;
                }
                if (imgDie.gameObject.activeSelf)
                {
                    Color color1 = imgDie.color;
                    color1.a = f;
                    imgDie.color = color1;
                }
            }

            if (_effectTimer <= -1)
            {
                if (imgWin.gameObject.activeSelf)
                {
                    return;
                }

                if (imgDie.gameObject.activeSelf)
                {
                    SceneManager.LoadScene(_currentSceneName);
                    return;
                }

            }
        }
    }

    public void SetGameRunningSpeed(GameSpeed speed)
    {
        if (!m_CurGameSpeed.Equals(GameSpeed.Stop))
            m_CacheGameSpeed = m_CurGameSpeed;
        switch (speed)
        {
            case GameSpeed.Fast:
                Time.timeScale = 1.5f;
                break;
            case GameSpeed.Normal:
                Time.timeScale = 1f;
                break;
            case GameSpeed.Slow:
                Time.timeScale = 0.5f;
                break;
            case GameSpeed.Stop:
                Time.timeScale = 0;
                break;
        }
        m_CurGameSpeed = speed;
    }

    public void ToggleGamePause()
    {
        if (m_CurGameSpeed.Equals(GameSpeed.Stop))
        {
            SetGameRunningSpeed(m_CacheGameSpeed);
        }
        else SetGameRunningSpeed(GameSpeed.Stop);
    }
}
