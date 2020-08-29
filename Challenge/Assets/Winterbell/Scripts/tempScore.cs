using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempScore : MonoBehaviour
{
    [SerializeField] float aliveTime = 2f;
    // Start is called before the first frame update
    void Start() {
        Destroy(gameObject, aliveTime);
    }

}
