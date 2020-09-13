using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APen_Pen : MonoBehaviour
{   
    [SerializeField] float bulletSpeed = 0;

    bool shot = false;

    // Start is called before the first frame update
    void Start()    {
        
    }

    // Update is called once per frame
    void Update()    {
        if (Input.GetMouseButtonDown(0) && !shot){
            shot = true;
        }
        if (transform.position.x < -10){
             shot = false;
             transform.position = new Vector3(6.5f,4f,0f);
        }
    }

    void FixedUpdate() {
        if (shot){
            shoot();
        }   
    }

    public void stopShoot(){
       shot = false; 
    }

    void shoot(){
        transform.position = transform.position + new Vector3(-1f,0,0) * bulletSpeed * Time.deltaTime;
    }
}
