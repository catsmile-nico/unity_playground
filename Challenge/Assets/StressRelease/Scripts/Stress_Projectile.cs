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
    bool collided = false;

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

        if(this.transform.position.y > 10 || textObj.text.Length == 0){
            Destroy(textGO.gameObject);
            Destroy(this.gameObject);
        }

        if (GameObject.Find("Player") != null){
            if (GameObject.Find("Player").GetComponent<Stress_Player>().explosion){
                StartCoroutine(explosion(false));
                gc_collider.enabled = false;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col) {
        //if (col.gameObject.name.Contains("Ceiling")){
        if (!collided){
            StartCoroutine(explosion(true));
            collided = true;
        }
        //}
    }

    IEnumerator explosion(bool setParent){
        int tv_explodeCount = textObj.text.Length;
        Transform tv_explodeCheck = null;
        for(int i = 0 ; i < tv_explodeCount ; i++){
            tv_explodeCheck = Instantiate(explosionPF,new Vector3(this.transform.position.x,this.transform.position.y+explosionOffset-(i*explosionOffsetMtp),0),Quaternion.identity);
            if (setParent)
                tv_explodeCheck.transform.SetParent(this.gameObject.transform);
            yield return new WaitForSeconds(0.1f);
            
        }
    }
}
