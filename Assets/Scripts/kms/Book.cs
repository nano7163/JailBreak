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
    private int maxPage = 5;
    // 왼쪽 페이지 내용들
    private List<string> leftPageContents = new List<string>();

    // 오른쪽 페이지 내용들
    private List<string> rightPageContents = new List<string>();

    void Awake()
    {
        leftPageContents = new List<string>()
        {
//1
"1쪽: 교도소장일기",
//2
@"3쪽:교도소장일기
교도소장일기
교도소장일기",
//3
"5쪽: 교도소장일기",
//4
"7쪽: 교도소장일기잊혀진 전설에 따르면 영웅은 다시 돌아옵니다.",
//5
"9쪽: 마지막 장.교도소장일기교도소장일기 모든 여정은 여기서 끝을 맺습니다."
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
