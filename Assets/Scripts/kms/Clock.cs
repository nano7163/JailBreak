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
    [SerializeField] private GameObject black;
    [Header("Time Settings")]
    public float eventInterval = 600f; //600f 10분
    [SerializeField] private float elapsedTime = 0f;

    [Header("State")]
    [SerializeField] private bool isPaused = false; // 시간이 멈췄는지 확인하는 변수
    [SerializeField] private GameObject monsterGroup;
    public float Progress => elapsedTime / eventInterval;

    void Start()
    {
        timeBar.value = 0f;
        dayOrNightStatus = 0;
        dayOrNightImage.sprite = day;
        black.SetActive(false);
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
        GM.Instance.TeleportPlayer();
        if (dayOrNightStatus == 0){
            //낮이라면
            black.SetActive(false);
            GM.Instance.doorLock = false;
            monsterGroup.SetActive(false);
        }
        else
        {
            black.SetActive(true);
            //밤인 경우
            monsterGroup.SetActive(true);
            if (GM.Instance.bag.items[5].GetComponent<Items>().GetIsItemAcquired())
            {//복제 열쇠 꾸러미를 가지고 있는 경우
                GM.Instance.doorLock = false;
            }
            else
            {
                GM.Instance.doorLock = true;
            }
        }
    }
}
