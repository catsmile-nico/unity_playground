using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APen_Pen : MonoBehaviour
{   
    //Editor var
    [SerializeField] float bulletSpeed = 0;
    [SerializeField] AudioClip audioClip_pen = null;
    [SerializeField] AudioClip audioClip_ugh = null;

    //Start var
    Rect screenRect;
    Rigidbody2D rigidbodyGC;
    PolygonCollider2D polyColGC;

    //Game var
    bool rotate = false;
    int rotationMultiplier = 1;
    bool shot = false;
    bool stuck = false;
    // bool freeze = false;
    Vector3 mousePosition;
    
    void Start()    {
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
        rigidbodyGC = GetComponent<Rigidbody2D>();  
        polyColGC = GetComponent<PolygonCollider2D>();
        rotationMultiplier = Random.Range(1,3);
    }

    void Update()    {
        if (rotate)
            transform.Rotate(0,0,-Random.Range(10,10*rotationMultiplier));
        
        //SHOOT
        if (Input.GetMouseButtonDown(0) && !shot && !stuck){
            if (GameObject.Find("Scoreboard") != null)
                GameObject.Find("Scoreboard").GetComponent<APen_Scoring>().addShotCount();
            
            if (PlayerPrefs.GetInt("BGM") == 1)
                AudioSource.PlayClipAtPoint(audioClip_pen, Camera.main.transform.position, 0.5f);

            rigidbodyGC.AddForce(new Vector2(-bulletSpeed, 0f));
            shot = true;
        }

        //BOUNDARY DESTRUCTION
        if (transform.position.y < -3f || transform.position.x > 12 || transform.position.x < -12)
            Destroy(this.gameObject);       
    }

    void FixedUpdate() {
        // Follow mouse movement
        if (!shot && !stuck){
            float move = Mathf.Abs(transform.position.y - mousePosition.y);
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector3(transform.position.x, mousePosition.y, transform.position.z);
        }

        //Slow down bullet when reaching wall
        if (gameObject.tag == "AP_ApplePen" && transform.position.x < -7.5f){
            Destroy(rigidbodyGC);
        }
    }

    float collisionOffset(int angle){
        //270, 90 = 0
        //180, 0 = 0.14f
        //mod 180
        return (angle % 180) * 0.155f;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "AP_Apple"){
            
              
            // if (Mathf.Abs(this.transform.position.y+0.15f-col.gameObject.transform.position.y) < 0.3f){
            if (Mathf.Abs(col.gameObject.transform.position.y+
                            collisionOffset(Mathf.Abs(Mathf.RoundToInt(col.gameObject.transform.rotation.z)))
                            -this.transform.position.y) < 0.3f) {
                
                if (PlayerPrefs.GetInt("BGM") == 1)
                    AudioSource.PlayClipAtPoint(audioClip_ugh, Camera.main.transform.position, 0.20f);

                rigidbodyGC.velocity = new Vector2(-(bulletSpeed/100f), 0);

                this.transform.SetParent(col.gameObject.transform);
                gameObject.tag = "AP_ApplePen";
                polyColGC.enabled = false;
                stuck = true;
                
                col.gameObject.GetComponent<APen_Apple>().applePen(-(bulletSpeed/100f));
            } else 
                deadPen();

            if (GameObject.Find("Scoreboard") != null)
                GameObject.Find("Scoreboard").GetComponent<APen_Scoring>().addScore();
        } 

        if (col.gameObject.tag == "AP_Wall" || col.gameObject.tag == "AP_Walled")
            deadPen();
    }

    //Main final state
    public void applePen(){
        gameObject.tag = "AP_Walled";
        Destroy(rigidbodyGC);
        
        this.gameObject.GetComponent<SpriteRenderer>().material = new Material(Shader.Find("Sprites/Default"));
    }

    //Secondary final state
    public void deadPen(){
        gameObject.tag = "AP_Dead";

        rigidbodyGC.constraints = ~RigidbodyConstraints2D.FreezeAll;
        rigidbodyGC.velocity = new Vector2(0, 0);
        rigidbodyGC.AddForce(new Vector2(Random.Range(bulletSpeed/5,bulletSpeed/5*rotationMultiplier), Random.Range(bulletSpeed/5,bulletSpeed/5*rotationMultiplier)));    
        
        polyColGC.enabled = false;
        rotate = true;
    }

    // void freezeApplePen(){
    //     if (!freeze){
    //         rigidbodyGC.velocity = new Vector2(0, 0);
    //         rigidbodyGC.gravityScale = 0f;
    //         rigidbodyGC.constraints = RigidbodyConstraints2D.FreezeAll;
    //         rigidbodyGC.isKinematic = false;
    //         Destroy(rigidbodyGC);
    //         freeze = true;
    //     }
    // }
}
