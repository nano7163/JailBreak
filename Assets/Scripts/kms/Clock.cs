using NUnit.Framework.Internal.Commands;
using UnityEngine;
using UnityEngine.UI;
public class Clock : MonoBehaviour
{
    [SerializeField] private Slider timeBar;
    [SerializeField] private Sprite day;
    [SerializeField] private Sprite night;
    [SerializeField] private Image dayOrNightImage;
    [SerializeField] private int dayOrNightStatus;
    [Header("Time Settings")]
    public float eventInterval = 600f; //600f 10분
    [SerializeField] private float elapsedTime = 0f;

    [Header("State")]
    [SerializeField] private bool isPaused = false; // 시간이 멈췄는지 확인하는 변수

    public float Progress => elapsedTime / eventInterval;

    void Start()
    {
        timeBar.value = 0f;
        dayOrNightStatus = 0;
        dayOrNightImage.sprite = day;
    }
    void Update()
    {
        // 만약 일시 정지 상태라면 아래 로직을 실행하지 않고 리턴합니다.
        if (isPaused) return;

        elapsedTime += Time.deltaTime;

        if (timeBar != null)
        {
            timeBar.value = Progress;
        }

        if (elapsedTime >= eventInterval)
        {
            OnIntervalReached();
        }
    }

    // --- 외부에서 호출할 메소드들 ---

    /// <summary>
    /// 카운트를 정지시킵니다.
    /// </summary>
    public void PauseClock()
    {
        isPaused = true;
        Debug.Log("시계가 정지되었습니다.");
    }

    /// <summary>
    /// 카운트를 다시 시작합니다.
    /// </summary>
    public void ResumeClock()
    {
        isPaused = false;
        Debug.Log("시계가 다시 흐릅니다.");
    }

    /// <summary>
    /// 현재 상태에 따라 정지/재개를 전환(Toggle)합니다.
    /// </summary>
    public void ToggleClock()
    {
        isPaused = !isPaused;
        Debug.Log(isPaused ? "정지" : "재개");
    }

    /// <summary>
    /// 시간을 0으로 초기화하고 싶을 때 사용합니다.
    /// </summary>
    public void ResetClock()
    {
        elapsedTime = 0f;
        if (timeBar != null) timeBar.value = 0f;
        
    }

    void OnIntervalReached()
    {
        SkipDayOrNightCycle();
    }
    public void SetTimeBarValue(float value)
    {
        timeBar.value = value;
    }
    public float GetTimeBarValue()
    {
        return timeBar.value;
    }
    public void SetDayOrNight(int don)//0이면 낮, 1이면 밤
    {
        dayOrNightStatus = don;
        if (don == 1)
        {
            dayOrNightImage.sprite = night;
        }
        else if (don == 0)
        {
            dayOrNightImage.sprite = day;
        }
        else
        {
            Debug.Log("DON error");
        }
    }
    public int GetDayOrNigjtStatus()
    {
        return dayOrNightStatus;
    }
    private void ChangeCycle()
    {
        if (dayOrNightStatus == 0)
        {
            dayOrNightStatus = 1;
        }
        else if (dayOrNightStatus == 1)
        {
            dayOrNightStatus = 0;
        }
    }
    public void SkipDayOrNightCycle()
    {
        ResetClock();
        ChangeCycle();
        SetDayOrNight(dayOrNightStatus);
    }
}
