using System;
using BitWave_Labs.AnimatedTextReveal;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public static GM Instance;

    [SerializeField] public Gemini gemini;
    [SerializeField] public AnimateText animateText;

    public String prompt = @"
    
    ";
 

    
    public List<string> npcInfo = new List<string>
    {
//1번 npc        
    @"
    ",
//2번 npc
    @""

    };

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