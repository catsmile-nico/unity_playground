using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stress_Projectile : MonoBehaviour
{
    [SerializeField] float explosionOffset = 0;
    [SerializeField] float explosionOffsetMtp = 0;
    [SerializeField] float rectMultiplier = 0;
    [SerializeField] float bulletSpeed = 0;

    [SerializeField] Transform explosionPF;

    //Text obj
    GameObject textGO;
    Text textObj;
    RectTransform textTransform;

    //Projectile
    Rigidbody2D gc_rigidbody;
    Collider2D gc_collider;
    bool exploded = false;
    bool explodeAll = false;
    bool collided = false;
    float projX = 0;
    float projY = 0;

    private void createProjText() {
        textGO = new GameObject("ProjectileText");
        if (GameObject.Find("GUI") != null)
            textGO.transform.SetParent(GameObject.Find("GUI").gameObject.transform);

        // Text settings
        textObj = textGO.AddComponent<Text>();
        textTransform = textObj.GetComponent<RectTransform>();
        textTransform.anchoredPosition = new Vector2(-1000,-1000);
        textTransform.sizeDelta = new Vector2(10, 150);
        textObj.text = "PEW";
        textObj.fontStyle = FontStyle.Bold;
        textObj.fontSize = 17;
        textObj.color = new Color32(48,96,121,255); //32,64,81 //https://colorhunt.co/palette/196240
        textObj.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        textObj.alignment = TextAnchor.UpperCenter;
        textObj.horizontalOverflow = HorizontalWrapMode.Wrap;
    }

    public void text_edit(string fv_text){
        textObj.text = fv_text;
    }

    void text_move(float x, float y){
        textTransform.anchoredPosition = new Vector2(x, y-20f);
    }

    void Awake()    {
        createProjText();
        gc_rigidbody = GetComponent<Rigidbody2D>();  
        gc_collider = GetComponent<CapsuleCollider2D>();
    }
    void Update()    {
        text_move(this.transform.position.x * rectMultiplier, this.transform.position.y * rectMultiplier);
        gc_rigidbody.velocity = new Vector3(0, bulletSpeed, 0);

        //Set projectile lifetime when exit ceiling; or when text is empty
        if(this.transform.position.y > 10 || textObj.text.Length == 0){
            Destroy(textGO.gameObject);
            Destroy(this.gameObject);
        }

        //Explode all command
        if (GameObject.Find("Player") != null){
            //Conditions to explode: Collided, Not exploded (by ExplodeAll command), Played called ExplodeAll command
            if (collided && !exploded && GameObject.Find("Player").GetComponent<Stress_Player>().explosion)
                explodeAll = true;
        }

        
    }

    void OnCollisionEnter2D(Collision2D col) {
        //Used to explode at current position(instead of following projectile) in ExplodeAll command
        if (!explodeAll){
            projX = this.transform.position.x;
            projY = this.transform.position.y;
        }
        
        //First collision
        if (!collided){
            StartCoroutine(explosion(true, 0.1f));
            collided = true;
        }
    }

    //On explode all command, explode starting from top
    void OnCollisionStay2D(Collision2D col) {
        //Condition to explode: Collided, Not exploded (by ExplodeAll command), Explode All command is true, colliding with ceiling
        if(collided && !exploded && explodeAll && col.gameObject.name.Contains("Ceiling")){
            bulletSpeed /= 4;
            gc_collider.enabled = false;
            exploded = true; //prevent multiple explosion
            StartCoroutine(explosion(false, 0.3f));
        }
    }

    IEnumerator explosion(bool setParent, float explodeDelay){
        int tv_explodeCount = textObj.text.Length;
        Transform tv_explodeCheck = null;
        for(int i = 0 ; i < tv_explodeCount ; i++){
            tv_explodeCheck = Instantiate(explosionPF,new Vector3(projX,projY+explosionOffset-(i*explosionOffsetMtp),0),Quaternion.identity);
            if (setParent) {
                tv_explodeCheck.transform.SetParent(this.gameObject.transform);
                yield return new WaitForSeconds(explodeDelay);
            } 
            // this state only happens on "Explode all command"
            else { 
                float tv_randomDelay = Random.Range(0.1f, explodeDelay);
                yield return new WaitForSeconds(tv_randomDelay - (tv_randomDelay % 0.01f)); //crash prevention on explode all
            }
                
        }
    }
}
