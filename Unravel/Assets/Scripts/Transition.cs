using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public List<GameObject> Line = new List<GameObject>();
    int x = 0;
    int delay = 1;
    bool null_ = false;

    void LateUpdate(){
        for (int i = 0; i < Line.Count; i++){
            if (Line[i].GetComponent<Line>().iSTrans){
                x++;   
            }
        }
        if (x == 8 && null_){
            StartCoroutine("GetRigidbody");
        }
        else if (null_ == false){
            null_ = true;
        }
        x = 0;
    }
    public IEnumerator GetRigidbody(){
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(8);
    }
}
