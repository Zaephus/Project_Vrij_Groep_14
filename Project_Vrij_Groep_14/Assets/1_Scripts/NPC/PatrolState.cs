using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : BaseState {

    private NPCController npc;
    private NavMeshAgent navMeshAgent;

    public List<Transform> waypoints = new List<Transform>();
    [SerializeField] private int currentWaypointIndex = -1;
    [SerializeField] private Transform targetWaypoint;

    public override void OnEnter() {

        npc = GetComponent<NPCController>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.isStopped = false;

        currentWaypointIndex = (currentWaypointIndex+1) % waypoints.Count;
        targetWaypoint = waypoints[currentWaypointIndex];

    }

    public override void OnUpdate() {

        if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
            if(Vector3.Distance(navMeshAgent.destination,targetWaypoint.position) <= 0.1) {
                owner.SwitchState(typeof(IdleState));
            }
        }

        navMeshAgent.SetDestination(targetWaypoint.position);

    }

    public override void OnExit() {
        navMeshAgent.isStopped = true;
    }

}