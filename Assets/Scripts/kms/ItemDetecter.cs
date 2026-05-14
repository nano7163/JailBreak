using Unity.VisualScripting;
using UnityEngine;

public class ItemDetecter : MonoBehaviour
{
    [SerializeField] private BreakableWall breakableWall;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.name == "11")
        {
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
    }
}
