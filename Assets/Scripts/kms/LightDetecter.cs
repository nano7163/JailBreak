using UnityEngine;

public class LightDetecter : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        GM.Instance.saveLoad.BadEnding();
    }
}
