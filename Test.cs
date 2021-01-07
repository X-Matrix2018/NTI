using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using System.Collections;
using UnityEngine.Networking;
public class Test : MonoBehaviour, ITrackableEventHandler
{
    string te,tte,pict_name,rs;
    private TrackableBehaviour mTrackableBehaviour;
    public Text My_Text;
    public static string cur_pict;
    public Canvas canvDiagram;
    public Canvas canvDiagr;
    public GameObject Diag;
    public GameObject btnStat;
    public GameObject axis;
    public static int see;

    void Start()
    {
        Diagr.ready = 0;
        canvDiagram.gameObject.SetActive(false);
        canvDiagr.gameObject.SetActive(false);
        axis.gameObject.SetActive(false);
        btnStat.gameObject.SetActive(false);
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }
        
    public void OnTrackableStateChanged(
      TrackableBehaviour.Status previousStatus,
      TrackableBehaviour.Status newStatus)
    {
        if (Diagr.ready == 1)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }
    }
    private void OnTrackingFound()
    {
        see = 1;
       
        var kk = GetComponent<ImageTargetBehaviour>();
        pict_name = kk.ImageTarget.Name;
        cur_pict = kk.ImageTarget.Name;
        StartCoroutine(TFound());
    }
     private void OnTrackingLost()
    {
        see = 2;
        StartCoroutine(TLost());
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 130, 190), te);
        GUI.Label(new Rect(10, 140, 130, 390), tte);
    }

    public IEnumerator TFound()
    {
        //Diagr.cur_graph = 0;
        canvDiagram.gameObject.SetActive(true);
        axis.transform.SetParent(gameObject.transform,true);
        axis.transform.localRotation = Quaternion.Euler(0, -90, -90);
        axis.transform.localPosition = new Vector3(0, 0, 0);
        btnStat.gameObject.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("REQUEST_METHOD", "POST");
        form.AddField("pict_name", pict_name);
        form.AddField("code", EnterApp.code);
        form.AddField("action", "targetfound");

        using (UnityWebRequest www = UnityWebRequest.Post("https://haritonov-av.ru/_unity.php", form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string[] subs = www.downloadHandler.text.Split('|');
                My_Text.text = subs[0]+" / "+subs[1];
                Diag.GetComponent<DiagrammScript>().Val_ = System.Convert.ToInt32(subs[0]);
                Diag.GetComponent<DiagrammScript>().MaxVal_ = System.Convert.ToInt32(subs[1]);
                Diag.GetComponent<DiagrammScript>().Change();
            }
        }
    }

    private void OnApplicationQuit()
    {
        StartCoroutine(ExitServer());
    }
    
    public IEnumerator ExitServer()
    {
        WWWForm form = new WWWForm();
        form.AddField("REQUEST_METHOD", "POST");
        form.AddField("code", EnterApp.code);
        form.AddField("action", "exit");
        using (UnityWebRequest www = UnityWebRequest.Post("https://haritonov-av.ru/_unity.php", form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
        }
    }

    public IEnumerator TLost()
    {
        Diag.GetComponent<DiagrammScript>().Clear();
        canvDiagram.gameObject.SetActive(false);
        canvDiagr.gameObject.SetActive(false);
        axis.gameObject.SetActive(false);
        btnStat.gameObject.SetActive(false);
        WWWForm form = new WWWForm();
        form.AddField("REQUEST_METHOD", "POST");
        form.AddField("code", EnterApp.code);
        form.AddField("action", "targetlost");
        using (UnityWebRequest www = UnityWebRequest.Post("https://haritonov-av.ru/_unity.php", form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                My_Text.text = www.downloadHandler.text;
            }
        }
        My_Text.text = "";
    }

}