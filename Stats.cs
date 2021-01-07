using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Stats : MonoBehaviour
{
     public void OnClickBtn()
    {
        StartCoroutine(EnterStats());
    }
    public IEnumerator EnterStats()
    {
        string Url = "http://haritonov-av.ru/_unity.php?action=enter&date=&code=" + EnterApp.code;
        UnityWebRequest request = UnityWebRequest.Get(Url);
        yield return request.SendWebRequest();
    }

}
