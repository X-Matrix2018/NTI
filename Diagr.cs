using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Diagr : MonoBehaviour
{
    public Canvas canv;
    public Canvas canv2;
    public GameObject Sphere;
    public GameObject Diag;
    public GameObject Scripts;
    public List <GameObject> columns;
    public GameObject axis;
    public TextMesh digits;
    public Text tn;
    public GameObject Graph;
    public static int cur_graph;
    public static int ready;
    public static string data;
    public string[] gdata;
    public int z;
    public Color col;
    public Color32 coll;

    void Start()
    {
        cur_graph = 1;
    }
        
    public void OnClickStat()
    {
        StartCoroutine(ViewDiagr());
    }

    public IEnumerator ViewDiagr()
    {
        axis.transform.localPosition = new Vector3(-0.17f,0.08f,-0.17f);
        Sphere.gameObject.SetActive(false);
        columns[0].SetActive(false);
        axis.gameObject.SetActive(true);
        Diag.GetComponent<DiagrammScript>().Clear();
        canv.gameObject.SetActive(true);
        canv2.gameObject.SetActive(false);
        string Url = "https://haritonov-av.ru/_unity.php?action_get=viewdiagr&code" + EnterApp.code + "&pict_name=" + Test.cur_pict+ "&cur_graph="+ cur_graph;
        UnityWebRequest request = UnityWebRequest.Get(Url);
        yield return request.SendWebRequest();
        
        
        //Debug.Log(request.downloadHandler.text);

        if (Graph.GetComponent<GraphSC>().points.Count > 0)
        {
           Graph.GetComponent<GraphSC>().Clear();
        }

        if (columns.Count > 1)
        {
            while (columns.Count>1)
            {
                Destroy(columns[1]);
                columns.RemoveAt(1);
            }
            columns[0].SetActive(false);
        }

        if (cur_graph == 1) {
            digits.text = "";
            tn.text = "Распределение по возрасту";
            data = request.downloadHandler.text;
            Graph.GetComponent<GraphSC>().Change(data);
        }

        if (cur_graph == 2) {
            tn.text = "Распределение по полу";
            digits.text = "М          Ж";
            columns[0].GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
            gdata = request.downloadHandler.text.Split(',');
            columns[0].SetActive(true);
            columns[0].transform.localScale = new Vector3(5.5f, 3.5f, 4.5f);
            columns.Add(Instantiate(columns[0], columns[0].transform.localPosition, columns[0].transform.rotation, columns[0].transform.parent));
            columns[0].transform.localPosition = new Vector3(0, 0.45f, -2);
            columns[0].GetComponent<Column>().ValS = float.Parse(gdata[0]);
            columns[0].GetComponent<Column>().Change();
            columns[1].transform.localPosition = new Vector3(0, 1.15f, -15);
            columns[1].GetComponent<Column>().ValS = float.Parse(gdata[1]);
            columns[1].GetComponent<Column>().Change();
            columns[1].GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
        }

         if (cur_graph == 3) {
            tn.text = "Распределение по любимым цветам";
            digits.text = "";
            gdata = request.downloadHandler.text.Split(',');
            columns[0].SetActive(true);
            columns[0].transform.localScale = new Vector3(1.5f, 3.5f, 2.5f);
            columns.Add(Instantiate(columns[0], columns[0].transform.localPosition, columns[0].transform.rotation, columns[0].transform.parent));
            columns[0].transform.localPosition = new Vector3(0, 0.45f, 3);
            columns[0].GetComponent<Column>().ValS = float.Parse(gdata[0]);
            columns[0].GetComponent<Column>().Change();
            columns[0].GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
            z = 3;
            for(int ii = 1; ii <= 6; ii++)
            {
                columns.Add(Instantiate(columns[0], columns[0].transform.localPosition, columns[0].transform.rotation, columns[0].transform.parent));
                if (ii == 1){col = new Color32(255, 106, 0, 0); }
                if (ii == 2){col = Color.yellow;}
                if (ii == 3){col = Color.green;}
                if (ii == 4){col = Color.cyan;}
                if (ii == 5){col = Color.blue;}
                if (ii == 6){col = Color.magenta;}
                columns[ii].GetComponent<MeshRenderer>().material.SetColor("_Color", col);
                columns[ii].transform.localPosition = new Vector3(0, 0.45f, 3-z);
                columns[ii].GetComponent<Column>().ValS = float.Parse(gdata[ii]);
                columns[ii].GetComponent<Column>().Change();
                z = z + 3;
            }
        }

        if (cur_graph == 4)
        {
            tn.text = "Распределение по умению рисования";
            digits.text = "ДА       НЕТ";
            gdata = request.downloadHandler.text.Split(',');
            columns[0].SetActive(true);
            columns[0].GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
            columns[0].transform.localScale = new Vector3(5.5f, 3.5f, 4.5f);
            columns.Add(Instantiate(columns[0], columns[0].transform.localPosition, columns[0].transform.rotation, columns[0].transform.parent));
            columns[0].transform.localPosition = new Vector3(0, 0.45f, -2);
            columns[0].GetComponent<Column>().ValS = float.Parse(gdata[0]);
            columns[0].GetComponent<Column>().Change();
            columns[1].transform.localPosition = new Vector3(0, 1.15f, -15);
            columns[1].GetComponent<Column>().ValS = float.Parse(gdata[1]);
            columns[1].GetComponent<Column>().Change();
            columns[1].GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
        }
    }
}
