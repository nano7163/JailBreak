using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BitWave_Labs.AnimatedTextReveal;

public class Book : MonoBehaviour
{
    [SerializeField] private GameObject closeBtn;
    [SerializeField] private GameObject toLeftPage;
    [SerializeField] private GameObject toRightPage;
    [SerializeField] private TMP_Text pageNumber;
    [SerializeField] private TMP_Text leftText;
    [SerializeField] private TMP_Text rightText;
    private int currentPage = 1;
    private int maxPage = 2;
    // 왼쪽 페이지 내용들
    private List<string> leftPageContents = new List<string>();

    // 오른쪽 페이지 내용들
    private List<string> rightPageContents = new List<string>();

    void Awake()
    {
        maxPage = 2;
        leftPageContents = new List<string>()
        {
//1
@"[기밀 문서: 프로젝트 '클린 소사이어티(Clean Society)' 운영 일지]
202X년 3월 14일
상부로부터 최종 승인이 떨어졌다. 예비 범죄자들을 격리하여 사회의 범죄율을 0%로 만들겠다는 위대한 실험, 일명 '프로젝트 클린 소사이어티'가 마침내 우리 교도소에서 본격적으로 가동된다.
빅데이터와 인공지능 시스템이 정밀하게 선정한 '잠재적 강력 범죄자' 리스트가 확보되었다. 이들은 아직 죄를 짓지 않았거나, 혹은 사소한 징후만 보였을 뿐이지만 그대로 두면 머지않아 사회를 좀먹을 괴물들이 될 자들이다. 이들을 미리 사회에서 격리하는 것만이 무고한 시민들을 지키고 범죄를 원천 차단하는 유일한 방법이다.",
//2
@"202X년 11월 07일
새로운 실험체(플레이어)가 입소했다. 이번 실험체 역시 완벽하게 기억이 말소된 상태로, 자신이 살인범이라는 위조된 죄목을 그대로 받아들인 듯하다.
이 감옥은 단순한 형벌의 공간이 아니다. 잠재적 범죄 인자를 가진 인간들이 기억을 잃은 상태에서 어떻게 행동하는지 관찰하고 통제하는 거대한 연구소다. 이미 외부 세계의 범죄율은 우리 프로젝트 덕분에 눈에 띄게 감소하고 있다. 이 통제 시스템이 완성되어 전국으로 확대된다면, 인류는 범죄가 존재하지 않는 완벽한 유토피아를 맞이하게 될 것이다.",
        };

        rightPageContents = new List<string>()
        {
//1
@"202X년 5월 22일
실험체들을 이곳으로 이송하는 작업이 순조롭게 진행 중이다. 시스템이 선정한 대상자들에게 가상의 강력 범죄(살인, 방화 등) 혐의를 씌운 뒤, 정교하게 조작된 CCTV와 허위 증거를 배치해 사법부를 속였다. 어차피 사회의 안전을 위한 작은 희생일 뿐이다.
가장 중요한 단계는 이송 직전 실행되는 '기억 말소' 작업이다. 대상자들이 ""나는 죄를 짓지 않았다""며 저항하거나 자아 혼란을 일으키면 실험의 통제력을 잃게 된다. 사건 당일을 전후한 기억을 완벽히 지워버림으로써, 그들은 자신이 정말로 살인을 저질렀을지도 모른다는 죄책감과 의구심 속에서 통제에 따르게 된다.",
//2
@"202X년 12월 19일
최근 일부 실험체들 사이에서 부작용으로 기억의 파편이 되살아나거나, 외부와 접촉을 시도하려는 수상한 움직임이 포착되었다. 특히 영치품 창고나 위치 정보 기록에 접근하려는 자가 있다면 즉시 격리해야 한다.
정보가 새어나가선 안 된다. 만약 대중이 '죄를 짓지도 않은 이들을 모아 기억을 지우고 수감하는 거대한 시스템'의 존재를 알게 된다면, 이 위대한 범죄 예방 프로젝트는 무산될 것이다. 무슨 일이 있어도 이 일지와 상부 보고용 파일은 사수해야 한다.",
        };
    }
    void OnEnable()
    {
        ShowPage();
    }

    public void ToNextPage()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            ShowPage();
        }
    }

    public void ToPreviousPage()
    {
        if (1 < currentPage)
        {
            currentPage--;
            ShowPage();
        }
    }
    private void ShowPage()
    {
        pageNumber.text = currentPage.ToString() + " / " + maxPage.ToString();
        leftText.text = leftPageContents[currentPage - 1];
        rightText.text = rightPageContents[currentPage - 1];
    }
}
