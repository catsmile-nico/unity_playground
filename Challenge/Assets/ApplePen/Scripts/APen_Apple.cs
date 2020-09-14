using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APen_Apple : MonoBehaviour
{
    //Editor var
    [SerializeField] float bounce = 0f;
    [SerializeField] AudioClip audioClip_applePen;

    //Start var
    Rigidbody2D rigidbodyGC;
    CircleCollider2D circleColGC;

    //Game var
    float launchPower = 0; //300-700
    bool rotate = true;
    bool penned = false;

    void Start()    {
        rigidbodyGC = GetComponent<Rigidbody2D>();   
        circleColGC = GetComponent<CircleCollider2D>();

        launchPower = Random.Range(300,700); //600 for test height
        rigidbodyGC.AddForce(new Vector2(0f,launchPower));
    }

    void Update()    {
        if (rotate)
            transform.Rotate(0,0,-10f);

        //fall out of screen
        if (transform.position.y < -3f || transform.position.x > 12 || transform.position.x < -12)
            Destroy(this.gameObject);

        if (penned && transform.position.x < -7.9f){
            rigidbodyGC.velocity = new Vector2(0, 0);
            rigidbodyGC.constraints = RigidbodyConstraints2D.FreezeAll;
        }
            
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "AP_Pen")
            if (Mathf.Abs(col.gameObject.transform.position.y+0.15f-this.transform.position.y) > 0.3f) 
                rigidbodyGC.velocity = new Vector2(rigidbodyGC.velocity.x, bounce);

        if (col.gameObject.tag == "AP_Wall" && penned){
            AudioSource.PlayClipAtPoint(audioClip_applePen, Camera.main.transform.position, 0.50f);
            gameObject.tag = "AP_Walled";

            circleColGC.enabled = false;
            rigidbodyGC.constraints = RigidbodyConstraints2D.FreezeAll;

            this.gameObject.GetComponent<SpriteRenderer>().material = new Material(Shader.Find("Sprites/Default"));
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<APen_Pen>().applePen();
            StartCoroutine(destroyApple());
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
        
        rigidbodyGC.isKinematic = false;
        rigidbodyGC.velocity = new Vector2(velocityX, 0);
    }
}
