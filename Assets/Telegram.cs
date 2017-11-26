using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Telegram : MonoBehaviour
{
    public string chat_id = "0000000"; // ID (you can know your id via @userinfobot)
    public string TOKEN = "00000:aaaaaa"; // bot token (@BotFather)
    public string API_URL
    {
        get
        {
            return string.Format("https://api.telegram.org/bot{0}/", TOKEN);
        }
    }

    public void GetMe()
    {
        WWWForm form = new WWWForm();
        UnityWebRequest www = UnityWebRequest.Post(API_URL + "getMe", form);
        StartCoroutine(SendRequest(www));
    }

    public void SendFile(byte[] bytes, string filename, string caption = "")
    {
        WWWForm form = new WWWForm();
        form.AddField("chat_id", chat_id);
        form.AddField("caption", caption);
        form.AddBinaryData("document", bytes, filename, "filename");
        UnityWebRequest www = UnityWebRequest.Post(API_URL + "sendDocument?", form);
        StartCoroutine(SendRequest(www));
    }

    public void SendPhoto(byte[] bytes, string filename, string caption = "")
    {
        WWWForm form = new WWWForm();
        form.AddField("chat_id", chat_id);
        form.AddField("caption", caption);
        form.AddBinaryData("photo", bytes, filename, "filename");
        UnityWebRequest www = UnityWebRequest.Post(API_URL + "sendPhoto?", form);
        StartCoroutine(SendRequest(www));
    }

    public new void SendMessage(string text)
    {
        WWWForm form = new WWWForm();
        form.AddField("chat_id", chat_id);
        form.AddField("text", text);
        UnityWebRequest www = UnityWebRequest.Post(API_URL + "sendMessage?", form);
        StartCoroutine(SendRequest(www));
    }

    IEnumerator SendRequest(UnityWebRequest www)
    {
        yield return www.SendWebRequest();
        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            var w = www;
            Debug.Log("Success!\n" + www.downloadHandler.text);
        }
    }


}