using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class InputSubmitManager : MonoBehaviour
{

    [SerializeField] private TMP_Text text;
    private TMP_InputField inputField;
    // 연타로 인한 버그 방지를 위한 플래그
    private bool _isProcessing = false;
    [SerializeField] private GameObject ButtonEndConversation;
    public String nPCInfomation = "";
    public String gameCode = "000";

    [SerializeField] private GameObject detective;
    [SerializeField] private GameObject jailer;
    [SerializeField] private GameObject toss;
    [SerializeField] private GameObject keyman;
    [SerializeField] private GameObject hacker;
    [SerializeField] private GameObject book;
    [SerializeField] private GameObject broker;
    [SerializeField] private GameObject money;
    // 인스펙터에서 이 메서드를 OnEndEdit 이벤트에 연결하세요 (Dynamic string)
    void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
    }
    public void OnInputEndEdit(string str)
    {
        // 1. 이미 처리 중이라면 무시 (연타 방지)
        if (_isProcessing) return;

        // 2. 텍스트가 비어있다면 무시 (의미 없는 엔터 방지)
        if (string.IsNullOrWhiteSpace(text.text)) return;

        // 3. 코루틴 시작
        StartCoroutine(ProcessSubmit(text.text));
        ButtonEndConversation.SetActive(false);
    }

    private IEnumerator ProcessSubmit(string processedText)
    {
        _isProcessing = true;
        // [핵심] 한 프레임 대기 (InputField 내부 처리가 완전히 끝날 시간을 줌)
        yield return null;
        // [핵심] 텍스트를 지우되, OnValueChanged 이벤트를 다시 발생시키지 않음
        text.text = "";
        inputField.SetTextWithoutNotify("");
        //위에 절대 건들지 말것

        // 로그 출력 (전달받은 text 인자를 사용합니다)

        Debug.Log("입력된 값: " + processedText);

        GM.Instance.gemini.SendRequest(
            @"# Role
너는 게임 속 NPC이자 대화 시스템이야. 대화 흐름을 보고 '단서 코드'를 줄지 스스로 판단해.

# 지시 사항
1. [NPC 정보] 내의 '단서 제공 조건'을 플레이어의 발언과 비교해.
2. 조건이 충족되었다고 판단하면: 대사 마지막 줄에 [단서_게임_코드]를 3자리 숫자로만 출력해.
3. 아직 조건이 충족되지 않았다면: 대사 마지막 줄에 반드시 '000'을 출력해.
4. 대사와 숫자 코드 사이에는 반드시 줄바꿈(엔터)을 한 번 넣어줘.
5. 모두 한국어를 사용해야해.
---
"
+
nPCInfomation
+
@"
### 플레이어의 현재 발언

"
+ processedText
        );

        // 선택 해제
        EventSystem.current.SetSelectedGameObject(null);

        _isProcessing = false;
    }
    public void NPCInfomationClear()
    {
        nPCInfomation = "";
    }
    public void GameCodeClear()
    {
        gameCode = "000";
    }
    public void ProgressByGameCode()
    {
        Debug.Log(gameCode + " 진행");
        if (gameCode == "000")
        {
            // 아무일도 일어나지 않는다
        }
        else if (gameCode == "001")
        {//탐정에 대한 소문
            GM.Instance.gameData.gameCode001 = true;
            GM.Instance.notice.SetActiveNotice(true);
            GM.Instance.bag.items[1 - 1].GetComponent<Items>().SetIsItemAcquired(true);
            GM.Instance.myDiary.SetIntAllowedPage(2);
        }
        else if (gameCode == "002")
        {
            //아무일도 일어나지 않는다. 일기 2장 썼다.
            GM.Instance.gameData.gameCode002 = true;
        }
        else if (gameCode == "003")
        {
            // 탐정의 연락처를 받는다.
            GM.Instance.gameData.gameCode003 = true;
            //탐정 npc 게임 오브젝트 활성화
            detective.SetActive(true);
            //
            GM.Instance.notice.SetActiveNotice(true);
            GM.Instance.bag.items[2 - 1].GetComponent<Items>().SetIsItemAcquired(true);
        }
        else if (gameCode == "004")
        {
            //송금 요구당함.
            GM.Instance.gameData.gameCode004 = true;
            //돈 활성화
            money.SetActive(true);
        }
        else if (gameCode == "005")
        {
            //돈 주웠음.
            GM.Instance.gameData.gameCode005 = true;
            GM.Instance.bag.items[3 - 1].GetComponent<Items>().SetIsItemAcquired(true);
            //은행장 활성화
            toss.SetActive(true);
            //
        }
        else if (gameCode == "006")
        {

            //송금 완료
            GM.Instance.gameData.gameCode006 = true;
        }
        else if (gameCode == "007")
        {
            GM.Instance.myDiary.SetIntAllowedPage(3);
            //탐정 조사 착수 후 며칠 기다림. 다이어리 3페이지 씀.
            GM.Instance.gameData.gameCode007 = true;
        }
        else if (gameCode == "008")
        {
            //조작된 cctv 진실을 알게됨.
            GM.Instance.gameData.gameCode008 = true;
            GM.Instance.bag.items[4 - 1].GetComponent<Items>().SetIsItemAcquired(true);
            GM.Instance.myDiary.SetIntAllowedPage(4);
        }
        else if (gameCode == "009")
        {
            //일기 3장 씀.
            GM.Instance.gameData.gameCode009 = true;
            //열쇠공 등장 
            keyman.SetActive(true);
            //
        }
        else if (gameCode == "010")
        {
            GM.Instance.gameData.gameCode010 = true;
            GM.Instance.bag.items[5 - 1].GetComponent<Items>().SetIsItemAcquired(true);//찰흙

            //교도관 활성화
            jailer.SetActive(true);
            //
        }
        else if (gameCode == "011")
        {
            GM.Instance.gameData.gameCode011 = true;// 본뜨기 성공
        }
        else if (gameCode == "012")
        {
            GM.Instance.gameData.gameCode012 = true;// 복제 열쇠 꾸러미 획득
            GM.Instance.bag.items[6 - 1].GetComponent<Items>().SetIsItemAcquired(true);
        }
        else if (gameCode == "013")
        {
            GM.Instance.gameData.gameCode013 = true;//휴대폰 획득
            GM.Instance.bag.items[7 - 1].GetComponent<Items>().SetIsItemAcquired(true);
            //해커 활성화
            hacker.SetActive(true);
            //
        }
        else if (gameCode == "014")
        {
            GM.Instance.gameData.gameCode014 = true; //해커한테 휴대폰 분석 받음
            GM.Instance.bag.items[8 - 1].GetComponent<Items>().SetIsItemAcquired(true);
            GM.Instance.myDiary.SetIntAllowedPage(5);
        }
        else if (gameCode == "015")
        {
            GM.Instance.gameData.gameCode015 = true;
            // 일기 4장

            //교도소장실 일기장 활성화
            book.SetActive(true);
            //
        }
        else if (gameCode == "016")
        {
            GM.Instance.gameData.gameCode016 = true;
            GM.Instance.bag.items[9 - 1].GetComponent<Items>().SetIsItemAcquired(true);// 충격적인 사실 획득
            GM.Instance.myDiary.SetIntAllowedPage(6);
        }
        else if (gameCode == "017")
        {
            GM.Instance.gameData.gameCode017 = true;//일기 5장 씀
        }
        else if (gameCode == "018")
        {
            GM.Instance.gameData.gameCode018 = true;//탐정이 미션 줌. 교도소장 일기장 훔쳐서 브로커에게 전달하라.
            //브로커 활성화 (1엔딩으로 가는 item형식)
            broker.SetActive(true);
            //
        }else if(gameCode == "019")
        {
            GM.Instance.gameData.gameCode019 = true;
        }

        GameCodeClear();//일 끝났으면 초기화
    }
}