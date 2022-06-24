using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HavenPuzzle : Puzzle {

    public GameObject stainedGlassPiecePrefab;

    public Transform solvedTargetTransform;

    public DialogueOption balancedPrompt;
    public DialogueOption placedTokenPrompt;

    public Scale scale;

    public event EventHandler IsSolved;

    private void OnEnable() {
        scale.IsBalanced += OnScaleBalanced;
        FindObjectOfType<StainedGlassHub>().OnSecondToken += OnPlaceToken;
    }

    private void Update() {
        
    }

    void OnScaleBalanced(object sender,System.EventArgs e) {

        FindObjectOfType<PhotoCapture>().OnTakePicture += OnResolvePuzzle;
        scale.IsBalanced -= OnScaleBalanced;
        
        StartCoroutine(StartPrompt(balancedPrompt,""));

    }

    void OnResolvePuzzle(object sender,System.EventArgs e) {
        PlayerManager player = FindObjectOfType<PlayerManager>();
        Vector3 dir = solvedTargetTransform.position - player.transform.position;
        if(Quaternion.Angle(player.transform.rotation,Quaternion.LookRotation(dir)) <= 10) {
            FindObjectOfType<PhotoCapture>().firstTimePicture = false;
            GameObject stainedGlassPuck = Instantiate(stainedGlassPiecePrefab,player.playerInteract.holdTransform.position,player.playerInteract.holdTransform.rotation);
            player.playerInteract.canInteract = false;
            stainedGlassPuck.GetComponent<StainedGlassPiece>().isHeld = true;
            player.playerInteract.holdItem = stainedGlassPuck;
            player.playerInteract.dropable = stainedGlassPuck.GetComponent<IDropable>();
            player.playerInteract.isHolding = true;
            player.playerInteract.canInteract = false;

            FindObjectOfType<PhotoCapture>().OnTakePicture -= OnResolvePuzzle;

        }

    }

    void OnPlaceToken(object sender,System.EventArgs e) {
        StartCoroutine(StartPrompt(placedTokenPrompt,"Nun"));
        IsSolved?.Invoke(this,EventArgs.Empty);
        FindObjectOfType<StainedGlassHub>().OnSecondToken -= OnPlaceToken;
    }

    public IEnumerator StartPrompt(DialogueOption prompt,string name) {
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<DialogueSystem>().Initialize(prompt,name);
    }

}