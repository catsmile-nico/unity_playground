using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stress_Player : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.InputField textInput;
    [SerializeField] float speed = 0;
    [SerializeField] float rectMultiplier = 0;

    [SerializeField] Transform textProj;

    public bool explosion = false;

    // Update is called once per frame
    void Update()    {
        if (Input.GetButtonDown("Jump")){
            Transform projGO = Instantiate(textProj,new Vector3(this.transform.position.x,this.transform.position.y+0.25f,0),Quaternion.identity);
            if (GameObject.Find("InputField") != null){
                string tv_text = GameObject.Find("InputField").gameObject.GetComponent<Stress_InputControl>().getText();
                projGO.gameObject.GetComponent<Stress_Projectile>().text_edit(tv_text);
            }
        }

        if (Input.GetButtonDown("Cancel"))
            explosion = true;
        if (Input.GetButtonUp("Cancel"))
            explosion = false;
        
    }

    void moveX(float x){
        transform.position = new Vector3(transform.position.x+x, transform.position.y, transform.position.z);
        textInput.GetComponent<Stress_InputControl>().moveX(transform.position.x*rectMultiplier);
    }

    private void FixedUpdate() {
        if (Input.GetAxisRaw("Horizontal") > 0 && this.transform.position.x < 7.4f && textInput.GetComponent<Stress_InputControl>().move)
            moveX(speed);
        else if (Input.GetAxisRaw("Horizontal") < 0 && this.transform.position.x > -7.4f && textInput.GetComponent<Stress_InputControl>().move)
            moveX(-speed);
            
    }

}
