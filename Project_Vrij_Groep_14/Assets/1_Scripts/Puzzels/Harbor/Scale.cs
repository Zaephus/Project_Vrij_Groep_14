using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour {

    public ScaleBucket bucketOne;
    public ScaleBucket bucketTwo;

    public Transform beam;
    public Rigidbody body;

    private float torque;

    public event EventHandler IsBalanced;

    public void Start() {
        
    }

    public void Update() {
        torque = bucketTwo.weight - bucketOne.weight;
        if(torque == 0 && Mathf.Abs(beam.rotation.z) <= 0.001f) {

            beam.localRotation = Quaternion.identity;
            body.angularVelocity = Vector3.zero;

            if(bucketOne.items.Count != 0 && bucketTwo.items.Count != 0) {
                IsBalanced?.Invoke(this,EventArgs.Empty);
            }
        }
    }

    public void FixedUpdate() {
        body.AddRelativeTorque(Vector3.forward * torque);
    }

}