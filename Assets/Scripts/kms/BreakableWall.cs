using TMPro;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] public TMP_Text durabilityText;
    public int durability = 500;

    void Start()
    {
        durability = 500;
        if (durability == 500)
        {
            durabilityText.text = "";
        }
    }
    public void SetDurability(int d)
    {
        durability = d;
        if (durability == 500)
        {
            durabilityText.text = "";
        }
        else
        {
            countDownDurability();
        }
    }
    public void countDownDurability()
    {
        if (durability > 0)
        {
            durability -= 1;
        }
        else
        {
            // 엔딩2
            Debug.Log("엔딩 2- 노가다 탈출");
            GM.Instance.saveLoad.Ending2();
        }
        Debug.Log(durability);
        GM.Instance.gameData.du = durability;
        durabilityText.text = ((durability / 500f) * 100).ToString() + "%";
    }



}
