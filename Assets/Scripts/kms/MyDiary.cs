using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BitWave_Labs.AnimatedTextReveal;
using NUnit.Framework.Constraints;

public class MyDiary : MonoBehaviour
{
    [SerializeField] private GameObject closeBtn;
    [SerializeField] private GameObject writeBtn;
    [SerializeField] private GameObject toLeftPage;
    [SerializeField] private GameObject toRightPage;
    [SerializeField] private TMP_Text pageNumber;
    [SerializeField] private TMP_Text leftText;
    [SerializeField] private TMP_Text rightText;
    [SerializeField] private GameObject diaryLeftWriter;
    [SerializeField] private GameObject diaryRightWriter;
    private int currentPage = 1;
    private int maxPage = 6;
    [SerializeField] private int allowedPage = 1;
    private int writtenPage = 1;
    // 왼쪽 페이지 내용들
    private List<string> leftPageContents = new List<string>();

    // 오른쪽 페이지 내용들
    private List<string> rightPageContents = new List<string>();

    void Awake()
    {
        leftPageContents = new List<string>()
        {
//1
@"1쪽: 나 살인범. 근데 기억이 안남. 뭔가 억울함. 내가 진짜 살인을 저지른 걸까.",
//2
@"3쪽: 전설적인 사설 탐정이라, 이 사람이면 내 막막한 상황을 해결해 줄 수 있을지도 몰라.",
//3
"5쪽: 사설탐정을 고용했다. ",
//4
"7쪽: 조작된 cctv, 누가? 왜 나를",
//5
"9쪽: 휴대폰 위치정보를 누가 인위적으로 지웠다. 의심은 커져간다.",
//6
"11쪽: 교도소장의 일기장 내용은 충격적이었다. 나는 살인범이 아니었다."

        };

        rightPageContents = new List<string>()
        {
//1
"2쪽: ",
//2
"4쪽: 그 빛은 사실 오래된 등불의 잔상이었습니다.",
//3
"6쪽: 기다리라고는 하는데 하루지만 오늘 밤은 잘을 잘 못잘 것 같다. 기대가 크다.",
//4
@"8쪽: 억울해. 난 살인범이 아닐지 모른다는 의심이 더욱 강해지고 있다. 진실을 알고 싶다.
하지만 탐정은 이제 도와줄 것이 없다고 했는데
내가 다른 단서를 찾는다면 탐정이 조사할 것이 더 생길지 몰라.
영치품 창고에 있는 나의 휴대폰을 확보해보는건 어떨까.
위험한 생각이지만 떠오르는 방법이 이거밖에 없어.",
//5
"10쪽: .",
//6
"12쪽: 그 일기장을 챙기고 싶었으나 그런 중요한 물건이 사라지면 들킬지도 모른다. 일단 탐정에게 상담해보자"
        };
    }
    void OnEnable()
    {
        ShowPage();
        if (writtenPage < allowedPage)
        {
            writeBtn.SetActive(true);
        }
        else
        {
            writeBtn.SetActive(false);
        }
    }

    public void SetIntAllowedPage(int num)
    {
        allowedPage = num;
    }

    public void ToNextPage()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            if (currentPage > writtenPage)
            {
                pageNumber.text = currentPage.ToString() + " / " + maxPage.ToString();
                leftText.text = "";
                rightText.text = "";
            }
            else
            {
                ShowPage();
            }
        }
    }

    public void ToPreviousPage()
    {
        if (1 < currentPage)
        {
            currentPage--;
            pageNumber.text = currentPage.ToString() + " / " + maxPage.ToString();
            ShowPage();
        }
    }
    private void ShowPage()
    {
        if (currentPage <= writtenPage)
        {
            pageNumber.text = currentPage.ToString() + " / " + maxPage.ToString();
            leftText.text = leftPageContents[currentPage - 1];
            rightText.text = rightPageContents[currentPage - 1];
        }
    }
    public void WritePage()
    {
        if (allowedPage == 2)
        {
            GM.Instance.gameData.gameCode002 = true;
        }
        else if (allowedPage == 3)
        {
            GM.Instance.gameData.gameCode019 = true;
        }
        else if (allowedPage == 4)
        {
            GM.Instance.inputSubmitManager.gameCode = "009";// cctv 단서 회득 직후 열쇠공 활성화
            GM.Instance.inputSubmitManager.ProgressByGameCode();
        }
        else if (allowedPage == 5)
        {
            //            GM.Instance.gameData.gameCode015 = true;
            //교도소장실 일기장 활성화가 아래에서 이루어진다.
            GM.Instance.inputSubmitManager.gameCode = "015";
            GM.Instance.inputSubmitManager.ProgressByGameCode();
        }
        else if (allowedPage == 6)
        {
            GM.Instance.gameData.gameCode017 = true;
        }
        Debug.Log("쓰기 실행");
        leftText.text = "";
        rightText.text = "";
        WriteMode();
        currentPage = allowedPage;
        writtenPage++;
        pageNumber.text = currentPage.ToString() + " / " + maxPage.ToString();
        Debug.Log(leftPageContents[currentPage - 1]);
        diaryLeftWriter.GetComponent<DiaryLeftWriter>().lines = new List<string> { leftPageContents[currentPage - 1] };
        diaryLeftWriter.SetActive(true);
    }
    public void WriteRightPage()
    {
        diaryRightWriter.GetComponent<DiaryRightWriter>().lines = new List<string> { rightPageContents[currentPage - 1] };
        diaryRightWriter.SetActive(true);
    }
    private void WriteMode()
    {
        closeBtn.SetActive(false);
        writeBtn.SetActive(false);
        toLeftPage.SetActive(false);
        toRightPage.SetActive(false);
    }
    public void WriteModeCompleted()
    {
        closeBtn.SetActive(true);
        //writeBtn.SetActive(true);
        toLeftPage.SetActive(true);
        toRightPage.SetActive(true);
    }
}
