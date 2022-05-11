using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossLines : MonoBehaviour
{
    [SerializeField] private GameObject parentLines;
    private Line[] lines;
     private DigitalRuby.LightningBolt.LightningBoltScript[] lightnings;
     [SerializeField] Renderer[] _materials; 
     public Material fixMaterial;
     public Material brokenMaterial;
  
    void Start()
    {
        lines = parentLines.GetComponentsInChildren<Line>();
        lightnings = parentLines.GetComponentsInChildren<DigitalRuby.LightningBolt.LightningBoltScript>();
        _materials = new Renderer[lines.Length];
        for(int i=0; i<lines.Length; i++){
            _materials[i] = lines[i].gameObject.GetComponent<Renderer>();
        }
    }

    private bool[] _isCross;
    void LateUpdate()
    {
        _isCross = new bool[lines.Length];
           for(int i = 0; i < lines.Length; i++){
            _isCross[i] = false;
        }
       
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines.Length; j++)
            { 
                if (checkIntersectionOfTwoLineSegments(lines[i].p1.transform.position,lines[i].p2.transform.position, lines[j].p1.transform.position,lines[j].p2.transform.position))
                {
                    _isCross[i] = true;
                    _isCross[j] = true;
                }
            }
        }
         ChangeLine();
    }

    void ChangeLine(){
        for(int i = 0; i < lines.Length; i++){
            if(_isCross[i]){
                lightnings[i].gameObject.SetActive(true);
                _materials[i].material = brokenMaterial;
            }else{
                lightnings[i].gameObject.SetActive(false);
                _materials[i].material = fixMaterial;
            }

        }
    }
    private bool checkPoints (Vector3 pointA, Vector3 pointB)
	{
		return (pointA.x == pointB.x && pointA.y == pointB.y);
	}

    //метод, проверяющий пересекаются ли 2 отрезка [p1, p2] и [p3, p4]
    private bool checkIntersectionOfTwoLineSegments(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4) {
                if (checkPoints(p1,p3)  ||  checkPoints (p2,p3)  ||  checkPoints (p1,p4) || checkPoints (p2,p4)) return false;

        //сначала расставим точки по порядку, т.е. чтобы было p1.x <= p2.x
        if (p2.x < p1.x) {

            Vector3 tmp = new Vector3(p1.x,p1.y,p1.z);
            p1 =  new Vector3(p2.x,p2.y,p2.z);
            p2 = tmp;
        }
        //и p3.x <= p4.x
        if (p4.x < p3.x) {

            Vector3 tmp = new Vector3(p3.x,p3.y,p3.z);
            p3 = new Vector3(p4.x,p4.y,p4.z);
            p4 = tmp;
        }

        //проверим существование потенциального интервала для точки пересечения отрезков
        if (p2.x < p3.x) {
            return false; //ибо у отрезков нету взаимной абсциссы
        }

        //если оба отрезка вертикальные
        if((p1.x - p2.x == 0) && (p3.x - p4.x == 0)) {

            //если они лежат на одном X
            if(p1.x == p3.x) {

                //проверим пересекаются ли они, т.е. есть ли у них общий Y
                //для этого возьмём отрицание от случая, когда они НЕ пересекаются
                if (!((Mathf.Max(p1.y, p2.y) < Mathf.Min(p3.y, p4.y)) ||
                        (Mathf.Min(p1.y, p2.y) > Mathf.Max(p3.y, p4.y)))) {

                    return true;
                }
            }

            return false;
        }

        //найдём коэффициенты уравнений, содержащих отрезки
        //f1(x) = A1*x + b1 = y
        //f2(x) = A2*x + b2 = y

        //если первый отрезок вертикальный
        if (p1.x - p2.x == 0) {

            //найдём Xa, Ya - точки пересечения двух прямых
            float Xa = p1.x;
            float A2 = (p3.y - p4.y) / (p3.x - p4.x);
            float b2 = p3.y - A2 * p3.x;
            float Ya = A2 * Xa + b2;

            if (p3.x <= Xa && p4.x >= Xa && Mathf.Min(p1.y, p2.y) <= Ya &&
                    Mathf.Max(p1.y, p2.y) >= Ya) {

                return true;
            }

            return false;
        }

        //если второй отрезок вертикальный
        if (p3.x - p4.x == 0) {

            //найдём Xa, Ya - точки пересечения двух прямых
            float Xa2 = p3.x;
            float A1 = (p1.y - p2.y) / (p1.x - p2.x);
            float b1 = p1.y - A1 * p1.x;
            float Ya = A1 * Xa2 + b1;

            if (p1.x <= Xa2 && p2.x >= Xa2 && Mathf.Min(p3.y, p4.y) <= Ya &&
                    Mathf.Max(p3.y, p4.y) >= Ya) {

                return true;
            }

            return false;
        }

        //оба отрезка невертикальные
        
        float A1_ = (p1.y - p2.y) / (p1.x - p2.x);
        float A2_ = (p3.y - p4.y) / (p3.x - p4.x);
        float b1_ = p1.y - A1_ * p1.x;
        float b2_ = p3.y - A2_ * p3.x;

        if (A1_ == A2_) {
            return false; //отрезки параллельны
        }

        //Xa - абсцисса точки пересечения двух прямых
        float Xa3 = (b2_ - b1_) / (A1_ - A2_);

        if ((Xa3 < Mathf.Max(p1.x, p3.x)) || (Xa3 > Mathf.Min( p2.x, p4.x))) {
            return false; //точка Xa находится вне пересечения проекций отрезков на ось X 
        }
        else {
            return true;
        }
    }
}
