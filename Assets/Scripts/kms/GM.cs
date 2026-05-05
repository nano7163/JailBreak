using System;
using BitWave_Labs.AnimatedTextReveal;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GM : MonoBehaviour
{
    public static GM Instance;

    [SerializeField] public Gemini gemini;
    [SerializeField] public AnimateText animateText;

    public String prompt;

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

    void Start()
    {
        string path = Application.streamingAssetsPath + "/prompt.txt";
        if (File.Exists(path))
        {
            prompt = File.ReadAllText(path).Trim();
            Debug.Log("prompt 읽기 성공.");
        }
        else
        {
            Debug.LogError("prompt 파일이 없습니다! 파일을 생성하세요.");
        }
    }
}