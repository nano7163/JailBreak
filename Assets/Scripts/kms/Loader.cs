using UnityEngine;

public class Loader : MonoBehaviour
{
    public SaveLoad sv;

    private void Start()
    {
        Invoke("A", 0.2f);
    }

    public void A()
    {
        GM.Instance.saveLoad.LoadGameData();
    }
}
