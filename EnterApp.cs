using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;

public class EnterApp : MonoBehaviour
{
    public static int code;
    public InputField text_v;
    public Dropdown text_p;
    public Dropdown text_v1;
    public Dropdown text_v2;
    public Canvas canv;

    public class MyClass
    {
        public int level;
        public float timeElapsed;
        public string playerName;
    }

    void Start()
    {
        code = Random.Range(1000000, 1000000000);
    }

    public void OnClickBtn()
    {
        
       StartCoroutine(EnterApplication());


    }

    public IEnumerator EnterApplication()
    {


        string v, p, v1, v2;
        p = "";
        v1 = "";
        v2 = "";
        switch (text_p.options[text_p.value].text)
        {
            case "Мужской":
                p = "m";
                break;
            case "Женский":
                p = "w";
                break;
        }

        switch (text_v1.options[text_v1.value].text)
        {
            case "Красный":
                v1 = "red";
                break;
            case "Оранжевый":
                v1 = "orange";
                break;
            case "Жёлтый":
                v1 = "yellow";
                break;
            case "Зелёный":
                v1 = "green";
                break;
            case "Голубой":
                v1 = "lblue";
                break;
            case "Синий":
                v1 = "blue";
                break;
            case "Фиолетовый":
                v1 = "purple";
                break;
        }

        switch (text_v2.options[text_v2.value].text)
        {
            case "Да":
                v2 = "y";
                break;
            case "Нет":
                v2 = "n";
                break;
        }
        v = text_v.text;
        WWWForm form = new WWWForm();
        form.AddField("REQUEST_METHOD", "POST");
        form.AddField("v", v);
        form.AddField("p", p);
        form.AddField("v1", v1);
        form.AddField("v2", v2);
        form.AddField("code", code);
        form.AddField("action", "enter");
        
        using (UnityWebRequest www = UnityWebRequest.Post("https://haritonov-av.ru/_unity.php", form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Diagr.ready = 1;
                canv.gameObject.SetActive (false);
                
            }
        }
   

    }
}
