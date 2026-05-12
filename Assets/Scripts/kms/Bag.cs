using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Bag : MonoBehaviour
{
    [SerializeField] private TMP_Text itemInfo;
    [SerializeField] private GameObject textPanel;
    [SerializeField] public List<GameObject> items = new List<GameObject>();
    private List<string> itemInfos = new List<string>();
    void Awake()
    {
        itemInfos = new List<string>()
        {
//1
@"탐정에 대한 소문-

실력이 좋은 사설탐정인 거 같다.
거의 전설적인 존재라는 듯하다.",
//2
@"탐정의 연락처-

010-xxxx-xxxx",
//3
@"누군가의 비상금-

벽 틈에서 발견한 상당한 거금,
써도 써도 다 못쓸것만 같다.
누구 것인지는 모른다.",
//4
@"CCTV 분석 결과-

탐정에 의해 조작된 영상임이 밝혀졌다.
항소에서 무죄를 입증하기 위해
사용하긴 어려울 거 같다.",
//5
@"찰흙-

열쇠공에게 받은 찰흙,
이곳에 열쇠를 꾹 눌러 본을 뜰 수 있다.",
//6
@"복제 열쇠 꾸러미-

수감실은 물론 교도소 닫힌 곳 대부분을 열 수 있다.",
//7
@"휴대폰-

감옥에 갇히면서 영치품 창고에 보관되고 있었던 휴대폰",
//8
@"휴대폰 위치정보기록-

해커 출신 수감자의 도움을 받아 얻어낸
휴대폰 내 위치정보기록,
범행 당시 위치정보만이 인위적으로 지워진 것 같다.
알리바이를 입증하기 어려워 보인다.",
//9
@"충격적인 사실-

빅데이터 상으로 범죄자가 될 가능성이 높은 자들을 선별해
사회와 격리시킨다는 프로젝트의 존재",
//10
@"교도소장의 일기장-

모든 게 담긴 문서",
//11
@"숟가락-

식당에서 빼돌린 숟가락, 감옥 안에서 그나마 단단한 물건이다.
무언가를 파낼 수 있을지도"
        };
    }

    public void BagClose()
    {
        textPanel.SetActive(false);
        this.gameObject.SetActive(false);
    }
    public void BagOpen()
    {
        textPanel.SetActive(false);
        this.gameObject.SetActive(true);
        for (int j = 0; j < items.Count; j++)
        {
            bool tf = items[j].GetComponent<Items>().GetIsItemAcquired();
            items[j].SetActive(tf); 
        }
    }

    public void SetTextPanel(int itemNum)
    {
        textPanel.SetActive(true);
        itemInfo.text = itemInfos[itemNum - 1];
    }
}
