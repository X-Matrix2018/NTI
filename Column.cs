using UnityEngine;

public class Column : MonoBehaviour
{
    public float ValS;
    public void Change()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, ValS, gameObject.transform.localScale.z);
        gameObject.transform.localPosition = new Vector3(this.transform.localPosition.x, 0.6f + this.transform.localScale.y / 2, this.transform.localPosition.z);
    }
}