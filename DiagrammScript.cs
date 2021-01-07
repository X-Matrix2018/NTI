using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagrammScript : MonoBehaviour
{
    public float Val_;
    public float MaxVal_;
    public GameObject Dpart;
    private int mv = 0;
    public List<GameObject> clones;
    
    void Start()
    {
        clones.Add(Instantiate(Dpart));
    }
        
    public void Change()
    {
        for (int i = 0; i < clones.Count; i++)
        {
            Destroy(clones[i]);
        }
        clones.Clear();
        mv = -(int)(Val_ / MaxVal_ * 360);
        for (int i = 0; i >= mv; i--)
        {
            clones.Add(Instantiate(Dpart));
            Transform clone = clones[clones.Count - 1].transform;
            clone.localRotation = Quaternion.Euler(0, 0, i);
            clone.localPosition = new Vector3(gameObject.transform.position.x + Mathf.Cos((i + 90) * Mathf.PI / 180) * clone.localScale.y / 2, gameObject.transform.position.y + Mathf.Sin((i + 90) * Mathf.PI / 180) * clone.localScale.y / 2, gameObject.transform.position.z);
        }
    }

    public void Clear()
    {
        for (int i = 0; i < clones.Count; i++)
        {
            Destroy(clones[i]);
        }
        clones.Clear();
    }
}
