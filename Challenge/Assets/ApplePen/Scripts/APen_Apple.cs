using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APen_Apple : MonoBehaviour
{
    //Editor var
    [SerializeField] float bounce = 0f;
    [SerializeField] AudioClip audioClip_applePen = null;

    //Start var
    Rigidbody2D rigidbodyGC;
    CircleCollider2D circleColGC;

    //Game var
    float launchPower = 0; //300-700
    bool rotate = true;
    float rotateDirection = 0;
    bool penned = false;
    // bool freeze = false;

    void Start()    {
        rigidbodyGC = GetComponent<Rigidbody2D>();   
        circleColGC = GetComponent<CircleCollider2D>();

        launchPower = Random.Range(300,700); //600 for test height
        rigidbodyGC.AddForce(new Vector2(0f,launchPower));
        if (Random.Range(0,2) == 1)
            rotateDirection = 10f;
        else 
            rotateDirection = -10f;
    }

    void Update()    {
        if (rotate)
            transform.Rotate(0,0,rotateDirection);

        //fall out of screen
        if (transform.position.y < -3f || transform.position.x > 12 || transform.position.x < -12)
            Destroy(this.gameObject);
    }

    // void FixedUpdate() {
    //     if (gameObject.tag == "AP_ApplePen" && transform.position.x < -8f){
    //         freezeApplePen();
    //     }   
    // }

    float collisionOffset(int angle){
        //270, 90 = 0
        //180, 0 = 0.14f
        //mod 180
        return (angle % 180) * 0.155f;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "AP_Pen")
            if (Mathf.Abs(col.gameObject.transform.position.y+
                            collisionOffset(Mathf.Abs(Mathf.RoundToInt(this.transform.rotation.z)))
                            -this.transform.position.y) > 0.3f) 
                rigidbodyGC.velocity = new Vector2(rigidbodyGC.velocity.x, bounce);

        if (col.gameObject.tag == "AP_Wall"){
            if (penned){

                if (PlayerPrefs.GetInt("BGM") == 1)
                    AudioSource.PlayClipAtPoint(audioClip_applePen, Camera.main.transform.position, 0.20f);
                gameObject.tag = "AP_Walled";
                circleColGC.enabled = false;
                Destroy(rigidbodyGC);

                this.gameObject.GetComponent<SpriteRenderer>().material = new Material(Shader.Find("Sprites/Default"));
                if (this.gameObject.transform.transform.childCount >= 1)
                    this.gameObject.transform.GetChild(0).GetComponent<APen_Pen>().applePen();
                
                if (GameObject.Find("Scoreboard") != null)
                    GameObject.Find("Scoreboard").GetComponent<APen_Scoring>().multiplyScore(GameObject.FindGameObjectsWithTag("AP_Walled").Length);
                
                StartCoroutine(destroyApple());
            } else 
                rigidbodyGC.velocity = new Vector2(bounce/10, bounce/10);
        }
    }

    IEnumerator destroyApple(){
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

    public void applePen(float velocityX){
        gameObject.tag = "AP_ApplePen";

        rotate = false;
        penned = true;
        
        rigidbodyGC.velocity = new Vector2(velocityX, 0);
        rigidbodyGC.constraints = RigidbodyConstraints2D.FreezePositionY;
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
