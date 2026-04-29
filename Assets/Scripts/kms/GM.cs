using BitWave_Labs.AnimatedTextReveal;
using UnityEngine;

public class GM : MonoBehaviour
{
    public static GM Instance;

    [SerializeField] public Gemini gemini;
    [SerializeField] public AnimateText animateText;
    private void Awake()
    {
        // 싱글톤 초기화
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지되길 원하면 활성화
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    } 
}