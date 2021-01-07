using System.Collections.Generic;
using UnityEngine;

public class GraphSC : MonoBehaviour
{
    public GameObject pointPrefab;
    public int ElCount = 10;
    public float[] GrF = new float[10];
    private float[] GrF2;
    public List<GameObject> points;
    public GameObject parent;
    public string[] gdata;
    public string[] s;

    public void Clear()
    {
        for (int j = 0; j < points.Count; j++)
        {
            Destroy(points[j]);
        }
        points.Clear();
    }

    public void Change(string data)
    {
        gdata = data.Split('|');
        ElCount = gdata.Length;
        float[] GrF = new float[ElCount+1];
        for (int ii = 0; ii <= gdata.Length; ii++)
        {
            s = gdata[ii].Split(',');
            if (s != null) {
                GrF[ii] = float.Parse(s[0])/20;
            }
        }
        int i = 0;
        while (++i < 100 * ElCount-1)
        {
            points.Add(Instantiate(pointPrefab));
            points[points.Count - 1].transform.SetParent(parent.transform);
            Transform point = points[points.Count - 1].transform;
            point.gameObject.SetActive(true);
            point.localPosition = new Vector3(parent.transform.localPosition.z, parent.transform.localPosition.y + (float)(GrF[i / 100] * (0.5 * Mathf.Cos((float)(i % 100) / 100 * Mathf.PI) + 0.5) + GrF[i / 100 + 1] * (-0.5 * Mathf.Cos((float)(i % 100) / 100 * Mathf.PI) + 0.5)) * 20+4, parent.transform.localPosition.x - ((float)i - 50 * ElCount) / 200 * 10-10);
            point.localScale = pointPrefab.transform.localScale;
        }
    }
}