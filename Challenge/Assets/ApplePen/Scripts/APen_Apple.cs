using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APen_Apple : MonoBehaviour
{
    [SerializeField] float bounce = 0f;

    //Start var
    Rigidbody2D rigidbodyGC;

    //Game var
    float launchPower = 0; //300-700
    bool peak = false;
    bool rotate = true;
    bool penned = false;

    float fixPosY = 0;

    void Start()    {
        rigidbodyGC = GetComponent<Rigidbody2D>();   
        launchPower = 600; //Random.Range(300,700);
        launch();
    }

    void Update()    {
        // if (rotate)
        //     transform.Rotate(0,0,-10f);

        //fall out of screen
        if (transform.position.y < -3f)
            Destroy(this.gameObject);

        if (penned)
            transform.position = new Vector3(transform.position.x, fixPosY, transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.name.Contains("Pen")){
            fixPosY = this.transform.position.y;
            rotate = false;
            if (Mathf.Abs(col.gameObject.transform.position.y+0.15f-this.transform.position.y) < 0.25f) {
                rigidbodyGC.isKinematic = false;
                Debug.Log("hit " + (col.gameObject.transform.position.y+0.15f) + " " + this.transform.position.y);
                transform.position = new Vector3(transform.position.x, fixPosY, transform.position.z);
                this.transform.SetParent(col.gameObject.transform);
                penned = true;
            }
            else {
                rigidbodyGC.velocity = new Vector2(bounce, 0);
                Debug.Log("miss " + (col.gameObject.transform.position.y+0.15f) + " " + this.transform.position.y);
            } 
        }

        if (col.gameObject.name.Contains("Log") && penned){
            this.transform.parent.GetComponent<APen_Pen>().stopShoot();
            Debug.Log("logapple");
            Destroy(rigidbodyGC);
        }
    }

    void launch() {
        rigidbodyGC.AddForce(new Vector2(0f,launchPower));
    }
}
