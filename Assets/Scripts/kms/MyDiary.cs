using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BitWave_Labs.AnimatedTextReveal;

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
    private int maxPage = 5;
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
"1쪽: 모험의 시작. 서쪽 마을에서 이야기가 시작됩니다.",
//2
@"3쪽: 숲속 깊은 곳에서 신비한 빛을 발견했습니다.\n
숲속 깊은 곳에서 신비한 빛을 발견했습니다.\n +
숲속 깊은 곳에서 신비한 빛을 발견했습니다.\n +
숲속 깊은 곳에서 신비한 빛을 발견했습니다.",
//3
"5쪽: 고대 유적의 문이 서서히 열리기 시작합니다.",
//4
"7쪽: 잊혀진 전설에 따르면 영웅은 다시 돌아옵니다.",
//5
"9쪽: 마지막 장. 모든 여정은 여기서 끝을 맺습니다."
        };

        rightPageContents = new List<string>()
        {
//1
"2쪽: 마을 사람들은 모두 친절해 보였습니다.",
//2
"4쪽: 그 빛은 사실 오래된 등불의 잔상이었습니다.",
//3
"6쪽: 유적 안에는 차가운 공기가 가득했습니다.",
//4
"8쪽: 하늘에서 내려온 빛이 길을 안내합니다.",
//5
"10쪽: 에필로그. 새로운 전설이 시작될 준비를 마쳤습니다."
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
