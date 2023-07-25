using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

// ReSharper disable InconsistentNaming
public static class ChatGPT
{
    private const string APIKey = "";

    public static async UniTask<string[]> SendAsync(string message, int n = 1) {
        string body = JsonUtility.ToJson(new ChatGPTRequest {
            model = "gpt-3.5-turbo",
            messages = new[] { new ChatGPTMessage { role = "user", content = message } },
            temperature = 1f,
            n = n,
            max_tokens = 100
        });

        using var request = UnityWebRequest.Post("https://api.openai.com/v1/chat/completions", body, "application/json");
        request.SetRequestHeader("Authorization", $"Bearer {APIKey}");
        await request.SendWebRequest();

        string[] result = new string[n];
        if (request.result == UnityWebRequest.Result.Success) {
            var response = JsonUtility.FromJson<ChatGPTResponse>(request.downloadHandler.text);
            Debug.Log(request.downloadHandler.text);
            for (int i = 0; i < n; i++) {
                result[i] = response.choices[i].message.content.Replace("\n", "");
            }
        }
        return result;
    }

    #region DTO

    [Serializable]
    public struct ChatGPTRequest
    {
        public string model;
        public ChatGPTMessage[] messages;
        public float temperature;
        public int n;
        public int max_tokens;
    }

    [Serializable]
    public struct ChatGPTResponse
    {
        public string id;
        public Choice[] choices;
        public Usage usage;

        [Serializable]
        public struct Choice
        {
            public int index;
            public ChatGPTMessage message;
        }

        [Serializable]
        public struct Usage
        {
            public int prompt_tokens;
            public int completion_tokens;
            public int total_tokens;
        }
    }

    [Serializable]
    public struct ChatGPTMessage
    {
        public string role;
        public string content;
    }

    #endregion
}
