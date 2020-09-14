using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APen_Pen : MonoBehaviour
{   
    //Editor var
    [SerializeField] float bulletSpeed = 0;
    [SerializeField] AudioClip audioClip_pen;
    [SerializeField] AudioClip audioClip_ugh;

    //Start var
    Rect screenRect;
    Rigidbody2D rigidbodyGC;
    PolygonCollider2D polyColGC;

    //Game var
    bool rotate = false;
    bool shot = false;
    bool stuck = false;
    Vector3 mousePosition;
    
    void Start()    {
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
        rigidbodyGC = GetComponent<Rigidbody2D>();  
        polyColGC = GetComponent<PolygonCollider2D>();
    }

    void Update()    {
        if (rotate)
            transform.Rotate(0,0,-10);
        
        //SHOOT
        if (Input.GetMouseButtonDown(0) && !shot && !stuck){
            AudioSource.PlayClipAtPoint(audioClip_pen, Camera.main.transform.position);
            rigidbodyGC.AddForce(new Vector2(-bulletSpeed, 0f));
            shot = true;
        }

        //BOUNDARY DESTRUCTION
        if (transform.position.y < -3f || transform.position.x > 12 || transform.position.x < -12)
            Destroy(this.gameObject);       

        //Slow down bullet when reaching wall
        if (stuck && transform.position.x < -7f){
            rigidbodyGC.velocity = new Vector2(0, 0);
            rigidbodyGC.constraints = RigidbodyConstraints2D.FreezeAll;
        }
            
    }

    void FixedUpdate() {
        // Follow mouse movement
        if (!shot && !stuck){
            float move = Mathf.Abs(transform.position.y - mousePosition.y);
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector3(transform.position.x, mousePosition.y, transform.position.z);
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "AP_Apple"){
            if (Mathf.Abs(this.transform.position.y+0.15f-col.gameObject.transform.position.y) < 0.3f){
                AudioSource.PlayClipAtPoint(audioClip_ugh, Camera.main.transform.position, 0.50f);

                rigidbodyGC.velocity = new Vector2(rigidbodyGC.velocity.x, 0);
                // rigidbodyGC.AddForce(new Vector2(-bulletSpeed/10, 0));  

                this.transform.SetParent(col.gameObject.transform);
                gameObject.tag = "AP_ApplePen";
                
                polyColGC.enabled = false;
                stuck = true;
                
                col.gameObject.GetComponent<APen_Apple>().applePen(rigidbodyGC.velocity.x);
            } else 
                deadPen();
        } 

        if (col.gameObject.tag == "AP_Wall" || col.gameObject.tag == "AP_Walled"){
            deadPen();
        }
    }

    //Main final state
    public void applePen(){
        gameObject.tag = "AP_Walled";

        rigidbodyGC.velocity = new Vector2(0, 0);
        rigidbodyGC.constraints = RigidbodyConstraints2D.FreezeAll;
        
        this.gameObject.GetComponent<SpriteRenderer>().material = new Material(Shader.Find("Sprites/Default"));
    }

    //Secondary final state
    public void deadPen(){
        gameObject.tag = "AP_Dead";

        rigidbodyGC.constraints = ~RigidbodyConstraints2D.FreezeAll;
        rigidbodyGC.velocity = new Vector2(0, 0);
        rigidbodyGC.AddForce(new Vector2(Random.Range(bulletSpeed/10,bulletSpeed/5), Random.Range(bulletSpeed/10,bulletSpeed/5)));    
        
        polyColGC.enabled = false;
        rotate = true;
    }
}
