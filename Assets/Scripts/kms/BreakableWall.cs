using TMPro;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] public TMP_Text durabilityText;
    public int durability = 5000;

    void Start()
    {
        durability = 5000;
        if (durability == 5000)
        {
            durabilityText.text = "";
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
        }
        Debug.Log(durability);
        durabilityText.text = ((durability / 5000f) * 100).ToString() + "%";
    }



}
