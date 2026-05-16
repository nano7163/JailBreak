using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System;

public class Gemini : MonoBehaviour // 클래스 이름은 유니티 연동을 위해 기존 그대로 유지합니다.
{
    private string apiKey;
    // Groq 표준 엔드포인트 주소로 변경
    private string url = "https://api.groq.com/openai/v1/chat/completions";

    // 1. Groq(OpenAI 표준) 요청 데이터를 위한 구조체로 변경
    public class GroqRequest
    {
        public string model { get; set; }
        public List<GroqMessage> messages { get; set; }
    }

    public class GroqMessage
    {
        public string role { get; set; }
        public string content { get; set; }
    }

    // 2. Groq 응답 데이터를 받기 위한 구조체로 변경
    public class GroqResponse
    {
        public List<Choice> choices { get; set; }
    }

    public class Choice
    {
        public GroqMessage message { get; set; }
    }

    private void Awake()
    {
        // Awake에서 주소를 Groq 주소로 고정합니다.
        url = "https://api.groq.com/openai/v1/chat/completions";
        
        // 파일에서 키를 읽어오는 기존 로직 그대로 유지 (StreamingAssets/apikey.txt 안에 Groq 키를 넣으시면 됩니다)
        string path = Application.streamingAssetsPath + "/apikey.txt";
        if (File.Exists(path))
        {
            apiKey = File.ReadAllText(path).Trim();
            Debug.Log("Groq API 키 읽기 성공.");
        }
        else
        {
            Debug.LogError("API 키 파일이 없습니다! 파일을 생성하세요.");
        }
    }

    // 3. 기존 API 호출 메소드 그대로 유지
    public void SendRequest(string prompt)
    {
        StartCoroutine(PostRequest(prompt));
    }

    private IEnumerator PostRequest(string prompt)
    {
        // Groq 규격에 맞게 데이터 재구성
        var requestData = new GroqRequest
        {
            //model = "llama-3.3-70b-versatile", // 추천하는 무료 고성능 모델 (혹은 "llama-3.1-8b-instant")
            model = "openai/gpt-oss-120b",
            messages = new List<GroqMessage> {
                new GroqMessage { role = "user", content = prompt }
            }
        };

        string jsonBody = JsonConvert.SerializeObject(requestData);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        // [변경 포인트] Groq는 URL 뒤에 키를 붙이지 않고, 헤더(Header)에 보냅니다.
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            
            // 필수 헤더 설정
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + apiKey); // 인증 헤더 추가

            yield return request.SendWebRequest();
            
            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseJson = request.downloadHandler.text;
                // Groq 응답 구조로 디serialize
                GroqResponse response = JsonConvert.DeserializeObject<GroqResponse>(responseJson);

                // 기존 체크 로직과 동일하게 Groq 구조 검사
                if (response != null && response.choices != null && response.choices.Count > 0)
                {
                    var choice = response.choices[0];

                    if (choice.message != null && !string.IsNullOrEmpty(choice.message.content))
                    {
                        // 텍스트 추출 완료
                        string answer = choice.message.content;
                        Debug.Log("Groq 응답: " + answer);
                        
                        // 기존에 만들어두신 인게임 매니저 연동 로직 100% 동일하게 유지
                        GM.Instance.animateText.lines = ConvertStringToList(answer);
                        GM.Instance.inputSubmitManager.gameCode = GM.Instance.animateText.lines[^1];
                        GM.Instance.animateText.gameObject.SetActive(true);
                    }
                    else
                    {
                        Debug.LogWarning("응답은 왔으나 내용이 없습니다.");
                    }
                }
                else
                {
                    Debug.LogWarning("Groq로부터 답변을 찾을 수 없습니다.");
                }
            }
            else
            {
                if (request.responseCode == 503)
                {
                    Debug.LogError("서버가 현재 혼잡합니다(503). 잠시 후 다시 시도해주세요.");
                    GM.Instance.animateText.lines = new List<string> { "서버가 현재 혼잡합니다(503). 잠시 후 다시 시도해주세요." };
                    GM.Instance.animateText.gameObject.SetActive(true);
                }
                else
                {
                    Debug.LogError("오류 발생: " + request.error + "\n응답 내용: " + request.downloadHandler.text);
                    GM.Instance.animateText.lines = new List<string> { "응답 오류 발생" };
                    GM.Instance.animateText.gameObject.SetActive(true);
                }
            }
        }
    }

    public List<string> ConvertStringToList(string answer)
    {
        return new List<string>(answer.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries));
    }
}