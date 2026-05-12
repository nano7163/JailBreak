using UnityEngine;
using UnityEngine.Tilemaps;

public class Map_Visible : MonoBehaviour 
{
    public TilemapRenderer image;
    public TilemapRenderer[] doors; 

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) 
        {
            image.enabled = true;
            SetDoorsEnabled(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) 
        {
            image.enabled = false;
            SetDoorsEnabled(false);
        }
    }

    private void SetDoorsEnabled(bool state)
    {
        foreach (TilemapRenderer d in doors)
        {
            if (d != null) d.enabled = state;
        }
    }
}