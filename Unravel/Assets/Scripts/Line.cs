using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;
    private LineRenderer lr;

    public bool iSTrans = true;

    void Start(){
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0,p1.transform.position);
        lr.SetPosition(1,p2.transform.position);

    }
    void Update(){
        lr.SetPosition(0,p1.transform.position);
        lr.SetPosition(1,p2.transform.position);
    }
}
