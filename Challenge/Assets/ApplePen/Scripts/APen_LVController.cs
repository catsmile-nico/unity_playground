using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APen_LVController : MonoBehaviour
{
    //Editor var
    [SerializeField] Transform apple = null;
    [SerializeField] Transform pen = null;
    
    void Start() {
        spawnApple();
    }

    void Update() {
        if (GameObject.FindGameObjectsWithTag("AP_Apple").Length < 1)
            spawnApple();
        if (GameObject.FindGameObjectsWithTag("AP_Pen").Length < 1)
            spawnPen();
    }

    void spawnApple(){
        Instantiate(apple, new Vector3(Random.Range(-3,2),-2,0f),Quaternion.identity);
    }

    void spawnPen(){
        Instantiate(pen, new Vector3(7.5f,4f,0f),Quaternion.identity);
    }
}
