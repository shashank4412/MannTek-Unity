using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// Place it on Controller
public class HTTPClientController : MonoBehaviour
{
  public void IsInternetAvailable(Action successAction, Action failAction = null)
  {
    Get(Literal.LMS_URL, response =>
    {
      if (response != null) successAction?.Invoke();
      else failAction?.Invoke();
    });
  }


  public void Get(string url, Action<string> onCompleteAction)
  {
    Debug.Log(("GET Url: " + url).Colorify(Color.blue));//(Literal.BLUE_HEX));
    StartCoroutine(GetRequest(url, onCompleteAction));
  }
  private IEnumerator GetRequest(string url, Action<string> onCompleteAction)
  {
    UnityWebRequest request = UnityWebRequest.Get(url);
    yield return request.SendWebRequest();
    if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
    {
      Debug.Log(("GET Error, responseCode: " + request.responseCode).Colorify(Color.red));//(Literal.RED_HEX));
      onCompleteAction?.Invoke(null);
    }
    else
    {
      Debug.Log("GET Success".Colorify(Color.green));//(Literal.GREEN_HEX));
      onCompleteAction?.Invoke(request.downloadHandler.text);
    }

    request.Dispose();
  }


  public void Post(string url, string json, Action<string> onCompleteAction, KeyValuePair<string, string>[] customHeaders = null)
  {
    Debug.Log(("POST Url: " + url + " request: " + json).Colorify(Color.blue));//(Literal.BLUE_HEX));
    StartCoroutine(PostRequest(url, json, onCompleteAction, customHeaders));
  }
  private IEnumerator PostRequest(string url, string json, Action<string> onCompleteAction, KeyValuePair<string, string>[] customHeaders = null)
  {
    var request = new UnityWebRequest(url, "POST");
    byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
    request.uploadHandler = new UploadHandlerRaw(jsonToSend);
    request.downloadHandler = new DownloadHandlerBuffer();

    if (customHeaders == null) request.SetRequestHeader("Content-Type", "application/json");
    else customHeaders.ForEach((header) => request.SetRequestHeader(header.Key, header.Value));

    yield return request.SendWebRequest();
    if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
    {
      Debug.Log(("POST Error, responseCode: " + request.responseCode).Colorify(Color.red));//(Literal.RED_HEX));
      onCompleteAction?.Invoke(null);
    }
    else
    {
      Debug.Log(("POST Success, response: " + request.downloadHandler.text).Colorify(Color.green));//(Literal.GREEN_HEX));
      onCompleteAction?.Invoke(request.downloadHandler.text);
    }
    request.Dispose();
  }


  public void DownloadTexture(string url, Action<Texture> onCompleteAction)
  {
    Debug.Log(("DOWNLOAD TEXTURE Url: " + url).Colorify(Color.blue));//(Literal.BLUE_HEX));
    StartCoroutine(LoadFromWeb(url, onCompleteAction));
  }
  private IEnumerator LoadFromWeb(string url, Action<Texture> onCompleteAction)
  {
    UnityWebRequest webRequest = new(url);
    DownloadHandlerTexture textureDownloader = new(true);
    webRequest.downloadHandler = textureDownloader;
    yield return webRequest.SendWebRequest();
    if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
    {
      Debug.Log(("DOWNLOAD TEXTURE Error, responseCode: " + webRequest.responseCode).Colorify(Color.red));//(Literal.RED_HEX));
      onCompleteAction?.Invoke(null);
    }
    else
    {
      Debug.Log("DOWNLOAD TEXTURE Success".Colorify(Color.green));//(Literal.GREEN_HEX));
      onCompleteAction?.Invoke(textureDownloader.texture);
    }
  }


  public int GetVersionCode()
  {
    return FetchVersionCode();
  }
  public static int FetchVersionCode()
  {
    try
    {
      AndroidJavaClass contextCls = new("com.unity3d.player.UnityPlayer");
      AndroidJavaObject context = contextCls.GetStatic<AndroidJavaObject>("currentActivity");
      AndroidJavaObject packageMngr = context.Call<AndroidJavaObject>("getPackageManager");
      string packageName = context.Call<string>("getPackageName");
      AndroidJavaObject packageInfo = packageMngr.Call<AndroidJavaObject>("getPackageInfo", packageName, 0);
      return packageInfo.Get<int>("versionCode");
    }
    catch (Exception e)
    {
      Debug.Log(("VERSION error: " + e).Colorify(Color.red));//(Literal.RED_HEX));
      return -1;
    }
  }
}