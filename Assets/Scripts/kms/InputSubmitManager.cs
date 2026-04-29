using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InputSubmitManager : MonoBehaviour
{
    
    [SerializeField] private TMP_Text text;
    private TMP_InputField inputField;
    // 연타로 인한 버그 방지를 위한 플래그
    private bool _isProcessing = false;

    // 인스펙터에서 이 메서드를 OnEndEdit 이벤트에 연결하세요 (Dynamic string)
    void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
    }
    public void OnInputEndEdit(string str)
    {
        // 1. 이미 처리 중이라면 무시 (연타 방지)
        if (_isProcessing) return;

        // 2. 텍스트가 비어있다면 무시 (의미 없는 엔터 방지)
        if (string.IsNullOrWhiteSpace(text.text)) return;

        // 3. 코루틴 시작
        StartCoroutine(ProcessSubmit(text.text));
    }

    private IEnumerator ProcessSubmit(string processedText)
    {
        _isProcessing = true;
        // [핵심] 한 프레임 대기 (InputField 내부 처리가 완전히 끝날 시간을 줌)
        yield return null;
        // [핵심] 텍스트를 지우되, OnValueChanged 이벤트를 다시 발생시키지 않음
        text.text = "";
        inputField.SetTextWithoutNotify("");
        //위에 절대 건들지 말것

        // 로그 출력 (전달받은 text 인자를 사용합니다)
        
        Debug.Log("입력된 값: " + processedText);
        GM.Instance.gemini.SendRequest(processedText);

        // 선택 해제
        EventSystem.current.SetSelectedGameObject(null);

        _isProcessing = false;
    }
}