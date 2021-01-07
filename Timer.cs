using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public GameObject g;
    void Start()
    {
        InvokeRepeating("Message", 0, 2);
    }

    void Message()
    {
        if (Test.see == 1 && Diagr.cur_graph == 0) {
            StartCoroutine(g.GetComponent<Test>().TFound());
        }
    }
}
