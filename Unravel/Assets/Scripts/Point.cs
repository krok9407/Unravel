using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public bool isDrag = false;
    public bool isDown = false;

    void Awake(){
        for (int i = 0; i < 5; i++){
            Vector3 pos = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-2f, 2.5f), transform.position.z);
            this.transform.position = pos;
        }
    }

    void OnMouseUp()
    {
        if (isDown)
        {
            isDrag = false;
            isDown = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos2D = Input.mousePosition;
            mousePos2D.z = -Camera.main.transform.position.z;
            Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos3D, transform.forward, 30);
            foreach (RaycastHit2D elem in hits)
            {
                if (elem.collider.gameObject.tag == "_point"){
                    elem.collider.gameObject.GetComponent<Point>().isDrag = true;
                    elem.collider.gameObject.GetComponent<Point>().isDown = true;
                }
            }
        }
        if(isDrag){
            Drag();
        }
    }

    void Drag()
    {
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        pos.y = mousePos3D.y;
        this.transform.position = pos;
    }
}