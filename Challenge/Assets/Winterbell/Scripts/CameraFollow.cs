using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    private void FixedUpdate() {
        if (target.position.y > 0)
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        else
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }   

}
