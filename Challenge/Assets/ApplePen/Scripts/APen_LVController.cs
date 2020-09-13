using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APen_LVController : MonoBehaviour
{
    //Unity field
    [SerializeField] Transform apple;

    //Game var
    float fieldRange = 0; //-7~0
    
    void Start() {
        spawnApple();
    }

    void Update() {
        if (GameObject.FindGameObjectsWithTag("Apple").Length < 1)
            spawnApple();

    }

    void spawnApple(){
        Instantiate(apple, new Vector3(Random.Range(-7,0),-2,0f),Quaternion.identity);
    }
}
