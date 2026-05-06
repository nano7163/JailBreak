using UnityEngine;

public class Notification : MonoBehaviour
{
    [SerializeField] private float delay;
    public void SetActiveNotice(bool tf)
    {
        if (tf)
        {
            this.gameObject.SetActive(true);
            Invoke("SetActiveFalseNotice", delay);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    public void SetActiveFalseNotice()
    {
        SetActiveNotice(false);
    }
}
