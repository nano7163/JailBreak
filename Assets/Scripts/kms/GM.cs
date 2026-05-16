using System;
using BitWave_Labs.AnimatedTextReveal;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GM : MonoBehaviour
{
    public static GM Instance;

    public Gemini gemini;
    public AnimateText animateText;

    public bool doorLock = false;
    public String prompt;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform tpPoint;
    public Bag bag;
    public MoveMonster moveMonster1;
    public MoveMonster moveMonster2;
    public Transform centerPoint1;
    public Transform centerPoint2;
    public Transform defaultPoint1;
    public Transform defaultPoint2;
    public Clock clock;
    public Move playerMove;
    public Notification notice;
    public InputSubmitManager inputSubmitManager;
    public MyDiary myDiary;
    public GameData gameData;
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

    public void TeleportPlayer()
    {
        player.transform.position = tpPoint.position;
    }
    public void SetMoveMonsterStartDefault()
    {
        moveMonster1.transform.position = defaultPoint1.position;
        moveMonster1.SetLeft();
        moveMonster2.transform.position = defaultPoint2.position;
        moveMonster2.SetRight();
    }
    public void SetMoveMonsterStartCenter()
    {
        moveMonster1.transform.position = centerPoint1.position;
        moveMonster1.SetLeft();
        moveMonster2.transform.position = centerPoint2.position;
        moveMonster2.SetRight();
    }
}