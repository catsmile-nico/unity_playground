using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TD_Enemy : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public Transform enemyGFX;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
        
    }

    void UpdatePath(){
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p){
        if (!p.error){
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()    {
        //check if we have a path
        if (path == null)
            return;

        //check if have waypoint to move
        if (currentWaypoint >= path.vectorPath.Count){
            Debug.Log("GG");
            reachedEndOfPath = true;
            return;
        } else {
            reachedEndOfPath = false;
        }

        //move bird
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);

        //dist to waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
            currentWaypoint++;

        if (rb.velocity.x >= 0.01f)
            enemyGFX.localScale = new Vector3(-1,1,1);
        else if (rb.velocity.x <= -0.01f)
            enemyGFX.localScale = new Vector3(1,1,1);
    }
}
