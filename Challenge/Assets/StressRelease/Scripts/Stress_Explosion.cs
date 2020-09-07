using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stress_Explosion : MonoBehaviour
{
    public AudioClip audioClip;

    Animator anim;

    // Start is called before the first frame update
    void Start()    {
        anim = GetComponent<Animator>();
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            Destroy(this.gameObject);
    }
}
