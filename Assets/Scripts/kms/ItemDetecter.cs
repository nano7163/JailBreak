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
            GM.Instance.notice.SetActiveNotice(true);
            collision.gameObject.SetActive(false);
            GM.Instance.bag.items[11 - 1].GetComponent<Items>().SetIsItemAcquired(true);
            GM.Instance.gameData.item11 = true;
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
            GM.Instance.inputSubmitManager.gameCode = "005";
            GM.Instance.inputSubmitManager.ProgressByGameCode();
            JustTextPrint("벽 틈에 돈이 들어있다!?");
            GM.Instance.notice.SetActiveNotice(true);
            collision.gameObject.SetActive(false);
        }
        else if (collision.name == "Telephone")
        {
            if (!GM.Instance.gameData.gameCode013)
            {
                JustTextPrint("휴대폰을 찾았다.");
                collision.gameObject.SetActive(false);
                GM.Instance.notice.SetActiveNotice(true);
                GM.Instance.inputSubmitManager.gameCode = "013";
                GM.Instance.inputSubmitManager.ProgressByGameCode();
            }
        }
        else if (collision.name == "Book")
        {
            if (!GM.Instance.gameData.gameCode016)
            {
                GM.Instance.gameData.gameCode016 = true;
                GM.Instance.notice.SetActiveNotice(true);
                GM.Instance.bag.items[9 - 1].GetComponent<Items>().SetIsItemAcquired(true);
                GM.Instance.myDiary.SetIntAllowedPage(6);
                GM.Instance.clock.PauseClock();
                book.SetActive(true);
                GM.Instance.playerMove.enabled = false;
                GM.Instance.bag.items[9 - 1].GetComponent<Items>().SetIsItemAcquired(true);
                GM.Instance.notice.SetActiveNotice(true);
            }
            else if (GM.Instance.gameData.gameCode018)
            {
                GM.Instance.gameData.gameCode020 = true;
                GM.Instance.notice.SetActiveNotice(true);
                GM.Instance.bag.items[10 - 1].GetComponent<Items>().SetIsItemAcquired(true);
                collision.gameObject.SetActive(false);
            }

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
        else if (collision.name == "Broker")
        {//교도관 허리춤
            if (GM.Instance.bag.items[10 - 1].GetComponent<Items>().GetIsItemAcquired())
            {
                GM.Instance.saveLoad.Ending1();
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
