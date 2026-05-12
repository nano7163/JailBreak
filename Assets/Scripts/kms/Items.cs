using Unity.VisualScripting;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private Bag bag;

    [SerializeField] private bool isItemAcquired = false;

    public void SetIsItemAcquired(bool tf)
    {
        isItemAcquired = tf;
    }

    public bool GetIsItemAcquired()
    {
        return isItemAcquired;
    }

    public void ClickItem()
    {
        bag.SetTextPanel(int.Parse(this.name));
    }
}
