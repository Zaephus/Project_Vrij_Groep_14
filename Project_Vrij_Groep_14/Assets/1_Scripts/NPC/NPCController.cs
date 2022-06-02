using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour,IInteractable {

    public Animator animator;

    public float walkSpeed = 2f;
    public float rotateSpeed = 2f;

    public List<Waypoint> waypoints = new List<Waypoint>();
    private int currentWaypointIndex = -1;
    private Waypoint targetWaypoint;

    private bool isStopped;

    public float stoppingDistance;

    public DialogueOption dialogueOption;
    public bool canInteract;

    public void OnStart() {

        if(waypoints.Count != 0) {
            targetWaypoint = GetTargetWaypoint();
        }

        if(dialogueOption == null) {
            canInteract = false;
        }

    }

    public void OnUpdate() {

        if(waypoints.Count !=0 && !isStopped) {

            transform.position = Vector3.MoveTowards(transform.position,targetWaypoint.transform.position,walkSpeed*Time.deltaTime);
            RotateTowardsTarget();

            animator.SetFloat("Forward",1);

            if(Vector3.Distance(transform.position,targetWaypoint.transform.position) <= stoppingDistance) {
                isStopped = true;
                if(!targetWaypoint.endPoint) {
                    if(targetWaypoint.waitTimer > 0) {
                        StartCoroutine(ContinueWalking(targetWaypoint.waitTimer));
                    }
                    else {
                        targetWaypoint = GetTargetWaypoint();
                        isStopped = false;
                    }
                }
            }
        }
        else {
            animator.SetFloat("Forward",0);
        }

    }

    public IEnumerator ContinueWalking(float timer) {
        yield return new WaitForSeconds(timer);
        targetWaypoint = GetTargetWaypoint();
        isStopped = false;
    }

    public Waypoint GetTargetWaypoint() {

        currentWaypointIndex++;

        if(currentWaypointIndex >= waypoints.Count) {
            currentWaypointIndex = 0;
        }

        return waypoints[currentWaypointIndex];

    }

    public void RotateTowardsTarget() {

        Vector3 lookPos = targetWaypoint.transform.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation,rotateSpeed*Time.deltaTime);

    }

    public void Interact(PlayerManager p) {
        Manager.instance.StartDialogue(dialogueOption);
    }

    public bool CanInteract() {
        if(canInteract) {
            return true;
        }
        else {
            return false;
        }
    }

}