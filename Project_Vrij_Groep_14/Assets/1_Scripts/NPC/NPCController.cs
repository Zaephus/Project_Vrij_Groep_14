using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {

    public Animator animator;

    public float walkSpeed = 2f;
    public float rotateSpeed = 2f;

    public List<Transform> waypoints = new List<Transform>();
    private int currentWaypointIndex = -1;
    private Transform targetWaypoint;

    public float stoppingDistance;

    public void OnStart() {

        targetWaypoint = GetTargetWaypoint();
    }

    public void OnUpdate() {

        transform.position = Vector3.MoveTowards(transform.position,targetWaypoint.position,walkSpeed*Time.deltaTime);
        RotateTowardsTarget();

        animator.SetFloat("Forward",1);

        if(Vector3.Distance(transform.position,targetWaypoint.position) <= stoppingDistance) {
            targetWaypoint = GetTargetWaypoint();
        }
    }

    public Transform GetTargetWaypoint() {

        currentWaypointIndex++;

        if(currentWaypointIndex >= waypoints.Count) {
            currentWaypointIndex = 0;
        }

        return waypoints[currentWaypointIndex];

    }

    public void RotateTowardsTarget() {

        Vector3 lookPos = targetWaypoint.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation,rotateSpeed*Time.deltaTime);

    }

}