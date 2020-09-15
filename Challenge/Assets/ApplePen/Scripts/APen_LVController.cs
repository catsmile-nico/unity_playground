using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class APen_LVController : MonoBehaviour
{
    //Editor var
    [SerializeField] Transform apple = null;
    [SerializeField] Transform pen = null;
    
    void Start() {
        spawnApple();
    }

    void Update() {
        if (!GameObject.Find("Scoreboard").GetComponent<APen_Scoring>().getGameState()) {
            if (GameObject.FindGameObjectsWithTag("AP_Apple").Length < 1 && GameObject.FindGameObjectsWithTag("AP_ApplePen").Length < 1)
                spawnApple();
            if (GameObject.FindGameObjectsWithTag("AP_Pen").Length < 1)
                spawnPen();           
        } else {
            if (GameObject.FindGameObjectsWithTag("AP_Apple").Length < 10)
                spawnApple();
        }   
    }

    void spawnApple(){
        Instantiate(apple, new Vector3(Random.Range(-3,2),-1.44f,0f),Quaternion.identity);
    }

    void spawnPen(){
        Instantiate(pen, new Vector3(7.5f,4f,0f),Quaternion.identity);
    }

}
