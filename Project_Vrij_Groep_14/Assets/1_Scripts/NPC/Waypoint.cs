using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
    [Tooltip("Leave at 0 for no timer.")]
    public float waitTimer;
    public bool endPoint = false;
}