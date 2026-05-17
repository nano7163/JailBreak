using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class continueSaveLoad : MonoBehaviour
{
    [SerializeField] private Button loadButton;
    [SerializeField] private GameObject saveButton;
    [SerializeField] private TMP_Text endingText;
    [SerializeField] private GameObject endingPanel;
    [SerializeField] private GameObject newGameButton;
    [SerializeField] private GameObject continueButton;
    private string savePath;
    public GameData gameData;
    private InputSubmitManager inputSubmitManager;
    private void Start()
    {
        gameData = GM.Instance.gameData;
        inputSubmitManager = GM.Instance.inputSubmitManager;
        // 각 플랫폼(PC, 모바일 등)에 맞는 안전한 저장 경로 설정 (파일 이름: gameData.json)
        savePath = Path.Combine(Application.persistentDataPath, "gameData.json");
        // 게임 시작 시 기존 데이터 불러오기
        //LoadGameData();
        if (!File.Exists(savePath))//파일이 없으면,
        {
            loadButton.interactable = false;
        }
        else// 파일이 있으면
        {
            loadButton.interactable = true;
        }
        //Title();
        LoadGameData();
    }
    public void Title()
    {
        //GM.Instance.clock.PauseClock();
        endingText.text = "Jail Break";
        Debug.Log(endingText.text);
        endingText.gameObject.SetActive(true);
        endingPanel.SetActive(true);
        newGameButton.SetActive(true);
        continueButton.SetActive(true);
        saveButton.SetActive(false);
    }
    public void UnTitle()
    {
        GM.Instance.clock.ResumeClock();
        endingText.gameObject.SetActive(false);
        endingPanel.SetActive(false);
        newGameButton.SetActive(false);
        continueButton.SetActive(false);
        saveButton.SetActive(true);
    }
    public void NewGame()
    {
        string currentSceneName = "Demo1";
        SceneManager.LoadScene(currentSceneName);
        Debug.Log("" + currentSceneName);
    }
    public void continueGame()
    {
        LoadGameData();
        UnTitle();
    }
    // [데이터 저장하기]
    public void SaveGameData()
    {
        try
        {
            // 1. 객체를 JSON 문자열로 변환 (true를 넣으면 가독성 좋게 줄바꿈 정렬됨)
            string json = JsonUtility.ToJson(gameData, true);

            // 2. 파일로 저장
            File.WriteAllText(savePath, json);
            Debug.Log($"데이터 저장 성공! 경로: {savePath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"데이터 저장 실패: {e.Message}");
        }
    }
    // [데이터 불러오기]
    public void LoadGameData()
    {
        // 기존 저장 파일이 존재하는지 확인
        if (File.Exists(savePath))
        {
            try
            {
                // 1. 파일에서 JSON 문자열 읽어오기
                string json = File.ReadAllText(savePath);

                // 2. JSON 문자열을 다시 GameData 객체로 변환
                JsonUtility.FromJsonOverwrite(json, gameData);
                Debug.Log("데이터 불러오기 성공!");
                Load();
            }
            catch (System.Exception e)
            {
                Debug.LogError($"데이터 불러오기 실패: {e.Message}");
            }
        }
        else
        {
            Debug.Log("저장된 파일이 없습니다.");
        }
    }

    public void Load()
    {
        StartCoroutine(LoadGame());
    }

    private IEnumerator LoadGame()
    {
        WaitForSeconds delay = new WaitForSeconds(0.2f);
        if (gameData.gameCode001)
        {
            inputSubmitManager.gameCode = "001";
            inputSubmitManager.ProgressByGameCode();
            yield return delay;
        }
        if (gameData.gameCode002)
        {
            inputSubmitManager.gameCode = "002";
            inputSubmitManager.ProgressByGameCode();
            yield return delay;
        }
        if (gameData.gameCode003)
        {
            inputSubmitManager.gameCode = "003";
            inputSubmitManager.ProgressByGameCode();
            yield return delay;
        }
        if (gameData.gameCode004)
        {
            inputSubmitManager.gameCode = "004";
            inputSubmitManager.ProgressByGameCode();
            yield return delay;
        }
        if (gameData.gameCode005)
        {
            inputSubmitManager.gameCode = "005";
            inputSubmitManager.ProgressByGameCode();
            yield return delay;
        }
        if (gameData.gameCode006)
        {
            inputSubmitManager.gameCode = "006";
            inputSubmitManager.ProgressByGameCode();
            yield return delay;
        }
        if (gameData.gameCode007)
        {
            inputSubmitManager.gameCode = "007";
            inputSubmitManager.ProgressByGameCode();
            yield return delay;
        }
        if (gameData.gameCode008)
        {
            inputSubmitManager.gameCode = "008";
            inputSubmitManager.ProgressByGameCode();
            yield return delay;
        }
        if (gameData.gameCode009)
        {
            inputSubmitManager.gameCode = "009";
            inputSubmitManager.ProgressByGameCode();
            yield return delay;
        }
        if (gameData.gameCode010)
        {
            inputSubmitManager.gameCode = "010";
            inputSubmitManager.ProgressByGameCode();
            yield return delay;
        }
        if (gameData.gameCode011)
        {
            inputSubmitManager.gameCode = "011";
            inputSubmitManager.ProgressByGameCode();
            yield return delay;
        }
        if (gameData.gameCode012)
        {
            inputSubmitManager.gameCode = "012";
            inputSubmitManager.ProgressByGameCode();
            yield return delay;
        }
        if (gameData.gameCode013)
        {
            inputSubmitManager.gameCode = "013";
            inputSubmitManager.ProgressByGameCode();
            yield return delay;
        }
        if (gameData.gameCode014)
        {
            inputSubmitManager.gameCode = "014";
            inputSubmitManager.ProgressByGameCode();
            yield return delay;
        }
        if (gameData.gameCode015)
        {
            inputSubmitManager.gameCode = "015";
            inputSubmitManager.ProgressByGameCode();
            yield return delay;
        }
        if (gameData.gameCode016)
        {
            inputSubmitManager.gameCode = "016";
            inputSubmitManager.ProgressByGameCode();
            yield return delay;
        }
        if (gameData.gameCode017)
        {
            inputSubmitManager.gameCode = "017";
            inputSubmitManager.ProgressByGameCode();
            yield return delay;
        }
        if (gameData.gameCode018)
        {
            inputSubmitManager.gameCode = "018";
            inputSubmitManager.ProgressByGameCode();
            yield return delay;
        }
        if (gameData.gameCode019)
        {
            inputSubmitManager.gameCode = "019";
            inputSubmitManager.ProgressByGameCode();
            yield return delay;
        }
        GM.Instance.bw.SetDurability(gameData.du);
    }
}
