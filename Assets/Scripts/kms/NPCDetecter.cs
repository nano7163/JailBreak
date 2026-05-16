using System;
using BitWave_Labs.AnimatedTextReveal;
using Unity.VisualScripting;
using UnityEngine;

public class NPCDetecter : MonoBehaviour
{
    [SerializeField] private GameObject aIPanel;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.name == "MobMan")
        {
            if (!GM.Instance.bag.items[1 - 1].GetComponent<Items>().GetIsItemAcquired())
            {
                Debug.Log("강민구");
                GM.Instance.inputSubmitManager.nPCInfomation = @"### [NPC 정보]
*   **이름**: 교도소 동료 수감자 '강민구'
*   **성격**: 감옥에 갇힌 현실에서도 희망을 잃지 않고 긍정적임. 
*   **현재 상태**: 절도 전과 10범이라 수감됨. 출소 후에 절도 안하고 살고 있었는데 전과 탓에 범인으로 몰렸다. 실력 좋은 사설탐정의 소문을 듣고 항소를 위해 해당 사설탐정에 대해 수소문하는 중이다.
*   **단서 제공 조건**: 조건 없음, 말을 걸기만 하면 전설적인 사설 탐정에 대한 정보를 혹시 알고 있냐며 물어온다. '탐정에 대한 소문'을 알려준다.
*   **단서 게임 코드**: 001";
                AIinteracterSetActiveTrue();
            }
            else
            {// 컴플리트 민구
                Debug.Log("컴플리트 강민구");
                GM.Instance.inputSubmitManager.nPCInfomation = @"### [NPC 정보]
*   **이름**: 교도소 동료 수감자 '강민구'
*   **성격**: 감옥에 갇힌 현실에서도 희망을 잃지 않고 긍정적임. 
*   **현재 상태**: 절도 전과 10범이라 수감됨. 출소 후에 절도 안하고 살고 있었는데 전과 탓에 범인으로 몰렸다. 실력 좋은 사설탐정의 소문을 듣고 항소를 위해 해당 사설탐정에 대해 수소문하는 중이다.
*   **단서 제공 조건**: 이미 '탐정에 대한 소문'을 알려줬다. 단서 게임 코드는 '000'만을 출력한다.
*   **단서 게임 코드**: 000";
                AIinteracterSetActiveTrue();
            }
        }
        else if (collision.name == "RoomMate")
        {
            if (!GM.Instance.bag.items[2 - 1].GetComponent<Items>().GetIsItemAcquired())
            {
                Debug.Log("철수");
                GM.Instance.inputSubmitManager.nPCInfomation = @"### [NPC 정보]
*   **이름**: 같은방 수감자 '철수'
*   **성격**: 무기력하고 부정적임. 
*   **현재 상태**: 누명으로 감옥에 들어오고 과거 여러 시도를 해봤지만 결백 증명에 실패함. 체념하며 사는 중. '탐정'의 연락처를 알고 있음.
*   **단서 제공 조건**: 플레이어가 '탐정'에 대한 정보를 원할 때. 단서 '탐정의 연락처'를 알려준다.
*   **단서 게임 코드**: 003";
                AIinteracterSetActiveTrue();
            }
            else
            {
                // 컴플리트 철수
                Debug.Log("컴플리트 철수");
                GM.Instance.inputSubmitManager.nPCInfomation = @"### [NPC 정보]
*   **이름**: 같은방 수감자 '철수'
*   **성격**: 무기력하고 부정적임. 
*   **현재 상태**: 누명으로 감옥에 들어오고 과거 여러 시도를 해봤지만 결백 증명에 실패함. 체념하며 사는 중.
*   **단서 제공 조건**: 플레이어에게 이미 '탐정의 연락처'를 건네줬음. 게임 코드는 '000'만을 제공함. 
*   **단서 게임 코드**: 000";
                AIinteracterSetActiveTrue();
            }
        }
        else if (collision.name == "Detective")
        {
            if (!GM.Instance.gameData.gameCode003)
            {
                Debug.Log("탐정 의뢰 착수 전");
                GM.Instance.inputSubmitManager.nPCInfomation = @"### [NPC 정보]
*   **이름**: 사설 탐정
*   **성격**: 지적이고 자존감 높음. 전문적임. 흥미 돋는 사건을 쫓는걸 좋아함. 흥미가 없어도 돈을 많이 주면 의뢰를 착수함. 
*   **현재 상태**: 전화로 플레이어와 대화하고 있는 상태. 거액의 돈을 송금하지 않으면 의뢰를 맡을 생각 없음.
*   **단서 제공 조건**: 의뢰를 맡기고 싶다고 할 때, 거액의 송금을 요구한다. 게임 코드는 '004'을 제공함.
*   **단서 게임 코드**: 004";
                AIinteracterSetActiveTrue();
            }
            else if (!GM.Instance.gameData.gameCode004)
            {
                Debug.Log("탐정 송금 요구 중일 때");
                GM.Instance.inputSubmitManager.nPCInfomation = @"### [NPC 정보]
*   **이름**: 사설 탐정
*   **성격**: 지적이고 자존감 높음. 전문적임. 흥미 돋는 사건을 쫓는걸 좋아함. 흥미가 없어도 돈을 많이 주면 의뢰를 착수함. 
*   **현재 상태**: 전화로 플레이어와 대화하고 있는 상태. 거액의 돈을 송금하면 의뢰를 맡아준다고 한 상태. 아직 송금을 받지 못함.
*   **단서 제공 조건**: 의뢰비는 선불이라며 송금한 후에 다시 연락하라고 답변함. 게임 코드는 '000'만을 제공함.
*   **단서 게임 코드**: 000";
                AIinteracterSetActiveTrue();
            }
            else if (GM.Instance.gameData.gameCode004 && !GM.Instance.gameData.gameCode007)
            {//송금 받음. 의뢰 착수, 며칠 기다리기 전
                Debug.Log("탐정 의뢰 착수 며칠 기다리라 함");
                GM.Instance.inputSubmitManager.nPCInfomation = @"### [NPC 정보]
*   **이름**: 사설 탐정
*   **성격**: 지적이고 자존감 높음. 전문적임. 흥미 돋는 사건을 쫓는걸 좋아함. 흥미가 없어도 돈을 많이 주면 의뢰를 착수함. 
*   **현재 상태**: 전화로 플레이어와 대화하고 있는 상태. 거액의 돈을 송금받아 기뻐하며 의뢰를 받아줌. 
*   **단서 제공 조건**: 조사를 위해 며칠 기다리라고 요구함. 게임 코드는 '000'만을 제공함.
*   **단서 게임 코드**: 000";
                AIinteracterSetActiveTrue();
            }
            else if (GM.Instance.gameData.gameCode007)
            {// 며칠 기다렸음. 
                Debug.Log("탐정 의뢰 착수 후 며칠 지났음");
                GM.Instance.inputSubmitManager.nPCInfomation = @"### [NPC 정보]
*   **이름**: 사설 탐정
*   **성격**: 지적이고 자존감 높음. 전문적임. 흥미 돋는 사건을 쫓는걸 좋아함. 흥미가 없어도 돈을 많이 주면 의뢰를 착수함. 
*   **현재 상태**: 전화로 플레이어와 대화하고 있는 상태. 플레이어의 사건 조사하여 단서를 찾아냄.사건 당시 cctv 영상이 인위적으로 조작되어 있음을 분석해냄. 알리바이 증명에 사용할 수 없어 항소로 결백을 증명하기에 곤란한 상황.
*   **단서 제공 조건**: 무조건 단서 제공함. 'cctv 분석 결과'를 상세히 설명해줌. 알리바이 증명을 못해 항소에도 어려움이 있을 것이라고 상황을 설명해줌. 이 이상 도와줄 것이 없다는 통보를 해줌. 게임 코드 '008'을 제공함.
*   **단서 게임 코드**: 008";
                AIinteracterSetActiveTrue();
            }
        }
        else if (collision.name == "Toss")
        {
            if (!GM.Instance.gameData.gameCode006)
            {
                Debug.Log("별명 은행장");
                GM.Instance.inputSubmitManager.nPCInfomation = @"### [NPC 정보]
*   **이름**: 동료 수감수, 별명 '은행장'
*   **성격**: 돈을 밝힌다. 자존감 높음. 전문적임. 흥미 돋는 사건을 쫓는걸 좋아함. 흥미가 없어도 돈을 많이 주면 의뢰를 착수함. 
*   **현재 상태**: 불법대부업 하다가 감옥오게됨. 감옥에서 보통 영치금을 쓰지만 현금을 유통시켜 화폐로 사용하게 만든 장본인이다. 스스로가 감옥의 은행이 되어 현금을 받고 본인의 영치금으로 외부에 송금하거나, 반대로 현금을 인출해주기도 한다.
*   **단서 제공 조건**: 탐정에게 송금하고 싶다고 말할 때, 수수료는 알아서 떼간다고 하고 송금시켜준다. 이때 게임 코드 '006'을 제공함.
*   **단서 게임 코드**: 006";
                AIinteracterSetActiveTrue();
            }
            else
            {
                // 컴플리트 은행장
                Debug.Log("컴플리트 은행장");
                GM.Instance.inputSubmitManager.nPCInfomation = @"### [NPC 정보]
*   **이름**: 동료 수감수, 별명 '은행장'
*   **성격**: 돈을 밝힌다. 자존감 높음. 전문적임. 흥미 돋는 사건을 쫓는걸 좋아함. 흥미가 없어도 돈을 많이 주면 의뢰를 착수함. 
*   **현재 상태**: 불법대부업 하다가 감옥오게됨. 감옥에서 보통 영치금을 쓰지만 현금을 유통시켜 화폐로 사용하게 만든 장본인이다. 스스로가 감옥의 은행이 되어 현금을 받고 본인의 영치금으로 외부에 송금하거나, 반대로 현금을 인출해주기도 한다.
*   **단서 제공 조건**: 이미 플레이어의 외부로의 송금을 도와줬다. 다음에도 많은 이용 부탁한다고 말한다. 게임 코드 '000'만을 제공함.
*   **단서 게임 코드**: 000";
                AIinteracterSetActiveTrue();
            }
        }
        else if (collision.name == "KeyMan")
        {
            if (!GM.Instance.gameData.gameCode011)
            {
                Debug.Log("열쇠공, 비상금 없어져서 화난 상태");
                GM.Instance.inputSubmitManager.nPCInfomation = @"### [NPC 정보]
*   **이름**: 동료 수감수, 별명 '열쇠공'
*   **성격**: 자신이 바라는 미래를 망상하는걸 좋아한다. 터무니 없는 것을 많이 시도하려고 한다. 
*   **현재 상태**: 나무토막으로 열쇠를 만들어 탈출할 계획을 세우고 있었다. 나가서 사용할 현금도 확보해서 숨겨놨는데 최근 없어져서 극대노인 상태다. 말투에 화가 많이 섞여있고 왜 화났냐 물어보면 비상금이 사라졌다고 답한다.
*   **단서 제공 조건**: 플레이어가 '열쇠공'의 탈출 계획이 좋은 생각이라고 함께하고 싶다고 했을 때, '찰흙'을 건네준다. 이걸로 교도관의 허리춤에 있는 열쇠를 본떠오라고 시킨다.
*   **단서 게임 코드**: 010";
                AIinteracterSetActiveTrue();
            }
            else if (!GM.Instance.gameData.gameCode012)
            {//본뜬 상태일때
                Debug.Log("열쇠공, 분노 좀 없어짐. 계획 진행 성공에 신남");
                GM.Instance.inputSubmitManager.nPCInfomation = @"### [NPC 정보]
*   **이름**: 동료 수감수, 별명 '열쇠공'
*   **성격**: 자신이 바라는 미래를 망상하는걸 좋아한다. 터무니 없는 것을 많이 시도하려고 한다. 
*   **현재 상태**: 나무토막으로 열쇠를 만들어 탈출할 계획을 세우고 있었다. 플레이어가 열쇠들을 본떠오는데 성공하여 기뻐한다. 비상금이 사라진 일 때문에 최근 기분이 안좋았는데 이제 좀 괜찮아졌다.
*   **단서 제공 조건**: '열쇠공'이 자신의 감정 변화를 서술한다. '찰흙'을 토대로 열쇠를 제작해준다. '복제된 열쇠꾸러미'를 제공해준다. 다만 정작 중요한 출입구 열쇠는 없어서 '열쇠공'의 계획을 실패한 셈이라 아쉬워한다.
*   **단서 게임 코드**: 012";
                AIinteracterSetActiveTrue();
            }
            else
            {
                Debug.Log("컴플리트 열쇠공");
                GM.Instance.inputSubmitManager.nPCInfomation = @"### [NPC 정보]
*   **이름**: 동료 수감수, 별명 '열쇠공'
*   **성격**: 자신이 바라는 미래를 망상하는걸 좋아한다. 터무니 없는 것을 많이 시도하려고 한다. 
*   **현재 상태**: 나무토막으로 열쇠를 만들어 탈출할 계획을 세우고 있었다. 다만 교도관의 허리춤에 출입구 열쇠는 없어 계획이 실패했다. 그래도 계획을 도와준 플레이어에게 고마워하고 있다.
*   **단서 제공 조건**: 이미 모든 단서를 제공해주었다. 오직 게임 코드 '000'만을 제공한다.
*   **단서 게임 코드**: 000";
                AIinteracterSetActiveTrue();
            }
        }
    }
    private void AIinteracterSetActiveTrue()
    {
        GM.Instance.clock.PauseClock();
        GM.Instance.playerMove.enabled = false;
        aIPanel.SetActive(true);
    }
}
