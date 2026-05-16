using System;
using BitWave_Labs.AnimatedTextReveal;
using Unity.VisualScripting;
using UnityEngine;

public class ItemDetecter : MonoBehaviour
{
    [SerializeField] private AnimateText jTPanimateText;
    [SerializeField] private GameObject justTextPrinter;
    [SerializeField] private GameObject jTPPanel;
    [SerializeField] private BreakableWall breakableWall;
    [SerializeField] private GameObject myDiary;
    [SerializeField] private GameObject book;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.name == "11")
        {
            JustTextPrint("숟가락을 얻었다.");

            collision.gameObject.SetActive(false);
            GM.Instance.bag.items[11 - 1].GetComponent<Items>().SetIsItemAcquired(true);
        }
        else if (collision.name == "11-1")
        {
            if (GM.Instance.bag.items[11 - 1].GetComponent<Items>().GetIsItemAcquired())
            {
                breakableWall.countDownDurability();
            }
        }
        else if (collision.name == "Diary")
        {
            GM.Instance.clock.PauseClock();
            myDiary.SetActive(true);
            GM.Instance.playerMove.enabled = false;
        }
        else if (collision.name == "Money")
        {
            GM.Instance.gameData.gameCode005 = true;
            JustTextPrint("벽 틈에 돈이 들어있다!?");
            collision.gameObject.SetActive(false);
            GM.Instance.bag.items[3 - 1].GetComponent<Items>().SetIsItemAcquired(true);
        }
        else if (collision.name == "Telephone")
        {
            if (!GM.Instance.gameData.gameCode013)
            {
                JustTextPrint("휴대폰을 찾았다.");
                collision.gameObject.SetActive(false);
                GM.Instance.bag.items[7 - 1].GetComponent<Items>().SetIsItemAcquired(true);
                GM.Instance.gameData.gameCode013 = true;
                //해커 활성화

                //
            }
        }
        else if (collision.name == "Book")
        {
            GM.Instance.clock.PauseClock();
            book.SetActive(true);
            GM.Instance.playerMove.enabled = false;
            GM.Instance.bag.items[9 - 1].GetComponent<Items>().SetIsItemAcquired(true);
            GM.Instance.notice.SetActiveNotice(true);
        }
        else if (collision.name == "Jailer")
        {//교도관 허리춤
            if (!GM.Instance.gameData.gameCode011)
            {
                GM.Instance.gameData.gameCode011 = true;
                //본뜨기 성공
                JustTextPrint("찰흙에 몰래 열쇠들을 찍어냈다. 들키지 않았다.");
            }
        }
    }
    public void JustTextPrint(String str)
    {
        GM.Instance.clock.PauseClock();
        GM.Instance.playerMove.enabled = false;
        jTPanimateText.lines[0] = str;
        jTPPanel.SetActive(true);
        justTextPrinter.SetActive(true);
    }
}
