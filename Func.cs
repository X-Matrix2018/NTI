using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func : MonoBehaviour
{
    public GameObject axis;
    public GameObject d;
    public Canvas canv;
    public GameObject Graph;

    public void ButtonExitStat()
    {
        axis.gameObject.SetActive(false);
    }

    public void ButtonLeft()
    {
        Diagr.cur_graph = Diagr.cur_graph - 1;
        if (Diagr.cur_graph < 1)
        {
            Diagr.cur_graph = 4;
        }
        Debug.Log(Diagr.cur_graph);
        d.GetComponent<Diagr>().OnClickStat();
        
    }

    public void ButtonRight()
    {
        Diagr.cur_graph = Diagr.cur_graph + 1;
        if (Diagr.cur_graph > 4)
        {
            Diagr.cur_graph = 1;
        }
        Debug.Log(Diagr.cur_graph);
        d.GetComponent<Diagr>().OnClickStat();
      
    }

    public void ExStat()
    {
        canv.gameObject.SetActive(false);
        axis.SetActive(false);
        Diagr.cur_graph = 0;
        if (Graph.GetComponent<GraphSC>().points.Count > 0)
        {
            Graph.GetComponent<GraphSC>().Clear();
        }
    }

}