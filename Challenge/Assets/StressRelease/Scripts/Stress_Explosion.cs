using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stress_Explosion : MonoBehaviour
{
    public AudioClip audioClip;

    Animator animGC;
    void Start()    {
        animGC = GetComponent<Animator>();
        //Explosion noise control //Screen explosion limit 1200, WEBGL Explosion noise limit 60, Unity editor noise limit 120
        if (Random.Range(1,Mathf.Abs(GameObject.FindGameObjectsWithTag("Explosion").Length/30)) == 1)
            AudioSource.PlayClipAtPoint(audioClip, transform.position, 1.0f);
    
    }

    void Update()    {
        if (animGC.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            Destroy(this.gameObject);
    }
}
