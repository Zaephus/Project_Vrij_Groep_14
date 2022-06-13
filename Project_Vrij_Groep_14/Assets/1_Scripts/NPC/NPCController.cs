using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour,IInteractable {

    public Animator animator;

    public float walkSpeed = 2f;
    public float rotateSpeed = 2f;

    private NavMeshAgent navMeshAgent;

    public List<Waypoint> waypoints = new List<Waypoint>();
    private int currentWaypointIndex = -1;
    private Waypoint targetWaypoint;

    private bool isStopped;

    public float stoppingDistance;

    public DialogueOption dialogueOption;
    public string npcName;
    public bool canInteract;

    public event EventHandler OnInteract;

    public void OnStart() {

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = true;

        if(waypoints.Count != 0) {
            targetWaypoint = GetTargetWaypoint();
            navMeshAgent.isStopped = false;
        }

        if(dialogueOption == null) {
            canInteract = false;
        }

    }

    public void OnUpdate() {

        if(waypoints.Count !=0 && !navMeshAgent.isStopped) {

            //transform.position = Vector3.MoveTowards(transform.position,targetWaypoint.transform.position,walkSpeed*Time.deltaTime);
            navMeshAgent.SetDestination(targetWaypoint.transform.position);
            //RotateTowardsTarget();

            animator.SetFloat("Forward",1);

            if(Vector3.Distance(transform.position,targetWaypoint.transform.position) <= stoppingDistance) {
                navMeshAgent.isStopped = true;
                if(!targetWaypoint.endPoint) {
                    if(targetWaypoint.waitTimer > 0) {
                        StartCoroutine(ContinueWalking(targetWaypoint.waitTimer));
                    }
                    else {
                        targetWaypoint = GetTargetWaypoint();
                        navMeshAgent.isStopped = false;
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
        navMeshAgent.isStopped = false;
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
        OnInteract?.Invoke(this,EventArgs.Empty);
        Manager.instance.StartDialogue(dialogueOption,npcName);
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