using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HavenPuzzle : Puzzle {

    public GameObject stainedGlassPiecePrefab;

    public Transform solvedTargetTransform;

    public Scale scale;

    public event EventHandler IsSolved;

    private void OnEnable() {
        scale.IsBalanced += OnScaleBalanced;
    }

    private void Update() {
        
    }

    void OnScaleBalanced(object sender,System.EventArgs e) {
        FindObjectOfType<PhotoCapture>().OnTakePicture += OnResolvePuzzle;
        scale.IsBalanced -= OnScaleBalanced;
    }

    void OnResolvePuzzle(object sender,System.EventArgs e) {
        PlayerManager player = FindObjectOfType<PlayerManager>();
        Vector3 dir = solvedTargetTransform.position - player.transform.position;
        if(Quaternion.Angle(player.transform.rotation,Quaternion.LookRotation(dir)) <= 10) {
            FindObjectOfType<PhotoCapture>().firstTimePicture = false;
            GameObject stainedGlassPuck = Instantiate(stainedGlassPiecePrefab,player.playerInteract.holdTransform.position,player.playerInteract.holdTransform.rotation);
            stainedGlassPuck.GetComponent<StainedGlassPiece>().isHeld = true;
            player.playerInteract.holdItem = stainedGlassPuck;
            player.playerInteract.dropable = stainedGlassPuck.GetComponent<IDropable>();
            player.playerInteract.isHolding = true;
            player.playerInteract.canInteract = false;
            FindObjectOfType<PhotoCapture>().OnTakePicture -= OnResolvePuzzle;
        }

    }

}