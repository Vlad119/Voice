using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class WebHandler : MonoBehaviour
{
    public static WebHandler Instance;
    public delegate UnityWebRequest RequestCall();

    public string servAddress;

    public string actionEndpoint;



    private void Awake()
    {
        Instance = this;
        //SingletonImplementation();
    }

    #region Requests



    private async Task GetRequest(string url, UnityAction<string> DoIfSuccess = null)
    {
        var endUrl = servAddress + url;
        var req = await IRequestSend(() =>
        {
            var request = new UnityWebRequest(endUrl, "GET");
            request.downloadHandler = new DownloadHandlerBuffer();
            return request;
        });

      //  Debug.Log("Request sent");
        Debug.Log("Status code: " + req.responseCode);
        DoIfSuccess?.Invoke(req.downloadHandler.text);
    }



    public async Task<UnityWebRequest> IRequestSend(RequestCall data)
    {
        UnityWebRequest request;//= data();

            request = data();

            await request.SendWebRequest();
            Debug.Log(request.isNetworkError);
            
            return request;
    }
    #endregion





    public async Task ActionWraper(string param, UnityAction<string> afterfinish)
    {

        await GetRequest(actionEndpoint
            + param
            , afterfinish);
    }



   

    private void SingletonImplementation()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        else if (Instance != this)
            Destroy(this);
    }
}
