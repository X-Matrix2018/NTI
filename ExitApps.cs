using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ExitApps : MonoBehaviour
{
    public void AppExit()
    {
        StartCoroutine(Quit());
    }

    public IEnumerator Quit()
    {
        string Url = "http://haritonov-av.ru/_unity.php?action=exit&code=" + EnterApp.code;
        UnityWebRequest request = UnityWebRequest.Get(Url);
        yield return request.SendWebRequest();
        Application.Quit();
    }

}
