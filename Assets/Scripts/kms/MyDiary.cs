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
@"내가 살인범이라니. 아무리 기억을 더듬어보려 해도 그날의 기억은 거대한 공백으로 남아있을 뿐이다. 가슴속 깊은 곳에서 억울함이 치밀어 오른다. 정말 내가 그런 끔찍한 짓을 저지른 걸까? 내 손에 정말 피를 묻혔단 말인가? 아무것도 확신할 수가 없다.",
//2
@"우연히 소문으로만 듣던 전설적인 사설 탐정에 대한 이야기를 접했다. 그 사람이라면 베일에 싸인 내 과거와 이 막막한 상황을 해결해 줄 수 있을지도 모른다. 벼랑 끝에 선 심정으로, 그에게 마지막 희망을 걸어보기로 했다.",
//3
@"우여곡절 끝에 드디어 사설 탐정을 고용하는 데 성공했다. 이제 주사위는 던져졌다. 그가 내 무죄를 입증할 단서를 찾아내 주기를 간절히 바랄 뿐이다.",
//4
@"탐정이 가져온 중간 조사 결과는 충격적이었다. 사건 당시의 CCTV가 조작되어 있었다. 대체 누가, 무슨 이유로 나를 범인으로 몰아가기 위해 이런 짓을 꾸민 걸까? 나를 향한 거대한 음모가 느껴진다.",
//5
@"간신히 확인한 내 휴대폰의 위치 정보는 누군가에 의해 인위적으로 삭제되어 있었다. 조작된 CCTV에 이어 위치 정보 말소까지. 배후에 있는 자들에 대한 의심과 공포가 걷잡을 수 없이 커져간다.",
//6
@"우연히 보게 된 교도소장의 일기장 속 내용은 가히 충격적이었다. 그 안에는 추악한 진실이 담겨 있었다. 내 예상이 맞았다. 나는 살인범이 아니었다. 누군가의 필요에 의해 만들어진 가짜 범인일 뿐이었다."

        };

        rightPageContents = new List<string>()
        {
//1
@"기억의 파편을 붙잡으려 밤새 눈을 붙이지 못했다. 벽에 비친 희미한 그림자가 마치 날 비웃는 감시자처럼 보였다.",
//2
@"창문 너머로 흘러든 그 빛은 사실 오래된 등불의 잔상에 불과했다. 내 마음이 다급하니 별것 아닌 조명에도 심장이 덜컥 내려앉는다. 정신을 바짝 차려야 한다.",
//3
@"탐정은 일단 나에게 침착하게 기다리라고 당부했다. 고작 하루라는 시간일 뿐인데, 왜 이렇게 길게만 느껴지는지 모르겠다. 기대와 불안이 뒤섞여 오늘 밤은 쉽게 잠을 이루지 못할 것 같다.",
//4
@"너무나도 억울하다. 내가 살인범이 아닐지도 모른다는 의심은 이제 확신으로 바뀌고 있다. 난 반드시 진실을 알아내야만 한다. 
하지만 탐정은 지금의 단서만으로는 더 이상 도와줄 방법이 없다며 난색을 표했다. 내가 새로운 단서를 더 찾아내기만 한다면 탐정도 다시 조사를 시작할 수 있을 텐데…. 
위험한 생각이지만, 교도소 영치품 창고에 보관되어 있는 내 휴대폰을 어떻게든 확보해보면 어떨까? 들키면 끝장이라는 걸 알지만, 지금 내 머릿속에 떠오르는 방법은 이것밖에 없다.",
//5
@"사방이 막힌 이 숨 막히는 공간에서, 나는 점점 진실의 꼬리를 붙잡아가고 있다. 조금만 더 하면 된다.",
//6
@"마음 같아서는 당장 그 일기장을 품에 챙겨 나오고 싶었지만, 그렇게 중요한 물건이 사라지면 금방 들통나고 말 것이다. 일단 흥분을 가라앉히고, 이 결정적인 단서에 대해 탐정에게 먼저 연락해 상담을 요청해 봐야겠다."
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
