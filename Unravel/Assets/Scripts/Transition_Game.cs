using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition_Game : MonoBehaviour
{
    public void SceneLoad(int index){
        SceneManager.LoadScene(index);
    }
}
