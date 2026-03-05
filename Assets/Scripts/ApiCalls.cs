using System;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public static class ApiCalls
{
    public static IEnumerator PostScore(string pathUserData, int levelNumber, int totalPoints  )
    {
        //Data data = new Data();
        string json = File.ReadAllText(pathUserData);
        Data data = JsonUtility.FromJson<Data>(json);

        PostData postData= new PostData();
        postData.name = levelNumber+"_"+data.username;
        //postData.email = data.email;
        postData.puntuacion = totalPoints;
        string jsonHS = JsonUtility.ToJson(postData);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonHS);

        var request = new UnityWebRequest("https://phpstack-1076337-5399863.cloudwaysapps.com/api/classification");
        request.method = UnityWebRequest.kHttpVerbPOST;
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        Debug.Log("Status Code: " + request.responseCode);
    }

    public static IEnumerator VerifyValoration(string pathUserData,Action<bool> callback)
    {
        string json = File.ReadAllText(pathUserData);
        Data data = JsonUtility.FromJson<Data>(json);

        PostData postData= new PostData();

        postData.name = data.username;
        postData.email = data.email;

        string jsonHS = JsonUtility.ToJson(postData);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonHS);

        var request = new UnityWebRequest("https://phpstack-1076337-5399863.cloudwaysapps.com/api/verify");
        request.method = UnityWebRequest.kHttpVerbPOST;
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        //Debug.Log(request.uploadHandler.ToString());
        //Debug.Log("Status Code: " + request.responseCode);
        //Debug.Log("Has Rated:" + request.downloadHandler.text);

        var replyjson = JsonUtility.FromJson<PostData>(request.downloadHandler.text);
       
        callback(replyjson.rated);
    }

    public static IEnumerator PostValoration(string pathUserData, float generalSlider, float jugabilitatSlider, float dificultatSlider, float graficsSlider, float concordanciaSlider)
    {
        Data data = new Data();
        string json = File.ReadAllText(pathUserData);
        data = JsonUtility.FromJson<Data>(json);
        PostData dataToRate = new PostData();

        dataToRate.email = data.email;
        dataToRate.name = data.username;
        dataToRate.general = (int)generalSlider;
        dataToRate.jugabilitat = (int)jugabilitatSlider;
        dataToRate.dificultat = (int)dificultatSlider;
        dataToRate.grafics = (int)graficsSlider;
        dataToRate.concordnacia = (int)concordanciaSlider;

        string jsonHS = JsonUtility.ToJson(dataToRate);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonHS);

        //Debug.Log(jsonHS);

        var request = new UnityWebRequest("https://phpstack-1076337-5399863.cloudwaysapps.com/api/rateGame");
        request.method = UnityWebRequest.kHttpVerbPOST;
            
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log(request.responseCode);
    }

    public static IEnumerator GetLeaderBoard(string url, Action<string> callback)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Accept","application/json");
        request.SetRequestHeader("Content-Type","application/json");

        yield return request.SendWebRequest();

        Debug.Log(request.result);
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Respuesta: " + request.downloadHandler.text);
            callback(request.downloadHandler.text);
            yield return request.downloadHandler.text;
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }
}
