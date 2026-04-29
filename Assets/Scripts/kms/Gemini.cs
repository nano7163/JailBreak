using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Generic;

public class Gemini : MonoBehaviour
{
    private string apiKey;
    private string url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent";

    // 1. 요청 데이터를 위한 구조체
    [System.Serializable]
    public class GeminiRequest
    {
        public List<Content> contents;
    }

    [System.Serializable]
    public class Content
    {
        public List<Part> parts;
    }

    [System.Serializable]
    public class Part
    {
        public string text;
    }

    // 2. 응답 데이터를 받기 위한 구조체
    public class GeminiResponse
    {
        public List<Candidate> candidates;
    }

    public class Candidate
    {
        public Content content;
    }

    private void Awake()
    {
        // 파일에서 키를 읽어옵니다.
        string path = Application.streamingAssetsPath + "/apikey.txt";
        if (File.Exists(path))
        {
            apiKey = File.ReadAllText(path).Trim();
            Debug.Log("API 키 읽기 성공.");

        }
        else
        {
            Debug.LogError("API 키 파일이 없습니다! 파일을 생성하세요.");
        }
    }
/*     void Start()
    {
        SendRequest("서버가 감자서버여 왜이렇게 응답을 못해");
    } */
    // 3. API 호출 메소드
    public void SendRequest(string prompt)
    {
        StartCoroutine(PostRequest(prompt));
    }

    private IEnumerator PostRequest(string prompt)
    {
        // 데이터 구성
        var requestData = new GeminiRequest
        {
            contents = new List<Content> {
                new Content { parts = new List<Part> { new Part { text = prompt } } }
            }
        };

        string jsonBody = JsonConvert.SerializeObject(requestData);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        // 요청 전송
        string fullUrl = $"{url}?key={apiKey}";
        using (UnityWebRequest request = new UnityWebRequest(fullUrl, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseJson = request.downloadHandler.text;
                GeminiResponse response = JsonConvert.DeserializeObject<GeminiResponse>(responseJson);

                // 1. 응답 구조가 비어있는지 체크
                if (response != null && response.candidates != null && response.candidates.Count > 0)
                {
                    var candidate = response.candidates[0];

                    // 2. 내용(Content)과 부분(Parts)이 있는지 체크
                    if (candidate.content != null && candidate.content.parts != null && candidate.content.parts.Count > 0)
                    {
                        string answer = candidate.content.parts[0].text;
                        Debug.Log("Gemini 응답: " + answer);
                        GM.Instance.animateText.lines = ConvertStringToList(answer);
                        GM.Instance.animateText.gameObject.SetActive(true);
                    }
                    else
                    {
                        Debug.LogWarning("응답은 왔으나 내용이 없습니다. (필터링 확인)");
                    }
                }
                else
                {
                    Debug.LogWarning("Gemini로부터 후보군을 찾을 수 없습니다.");
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
        // \n뿐만 아니라 \r\n(윈도우 스타일 줄바꿈)도 함께 처리하는 것이 안전합니다.
        // StringSplitOptions.RemoveEmptyEntries를 넣으면 빈 줄(공백)은 리스트에서 제외됩니다.
        return new List<string>(answer.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries));
    }
}