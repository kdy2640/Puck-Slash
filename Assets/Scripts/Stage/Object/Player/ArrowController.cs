using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ArrowController : MonoBehaviour
{
    public GameObject Mid;
    public GameObject Edge;
    public GameObject ChargeEdge;
    public Gradient gradient;

    [SerializeField] Renderer ChargeRend1;

    [SerializeField] Renderer ChargeRend2;
    private void Start()
    {
        SetArrow(Vector2.zero, new Vector2(100,-50),90.0f);

        ChargeRend1 = ChargeEdge.transform.GetChild(0).GetComponent<Renderer>();
        ChargeRend2 = ChargeEdge.transform.GetChild(1).GetComponent<Renderer>();
    }

    public void SetArrow(Vector2 StartPos, Vector2 lookatPos, float length)
    {
        Vector3 MidPosition = new Vector3(length / 2.0f,0,0);
        Vector3 MidScale = new Vector3(length, 1, 1);
        Vector3 EdgePosition = new Vector3(length, 0, 0);
        //radian
        float theta = Mathf.Atan2(-(lookatPos.y - StartPos.y), lookatPos.x - StartPos.x);
   
        Mid.transform.localPosition = MidPosition;
        Mid.transform.localScale = MidScale;
        Edge.transform.localPosition = EdgePosition;
        transform.rotation = Quaternion.Euler(0f, theta * Mathf.Rad2Deg, 0f);
        transform.position = new Vector3(StartPos.x, 5, StartPos.y) ;
    }
    /// <summary>
    /// Visualization Charging
    /// </summary>
    /// <param name="value"> 0 ~ 1</param>
    public void SetChargeArrow(float value)
    {
        value = Mathf.Clamp01(value);

        float chargeX = value * Edge.transform.localPosition.x;

        ChargeEdge.transform.localPosition = new Vector3(chargeX, 0, 0);

        SetChargeArrowColor(value);
    }


    private void SetChargeArrowColor(float value)
    {
        value = Mathf.Clamp01(value);

        Color nowColor = gradient.Evaluate(value);

        ChargeRend1.material.color = nowColor;
        ChargeRend2.material.color = nowColor;
    }

}
