using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StainedGlassHub : MonoBehaviour,IInteractable {

    private PlayerManager player;

    public List<Transform> tokenTransforms = new List<Transform>();
    public List<StainedGlassPiece> tokens = new List<StainedGlassPiece>();

    public event EventHandler OnFirstToken;
    public event EventHandler OnSecondToken;

    public void Start() {
        player = FindObjectOfType<PlayerManager>();
    }

    public void Interact(PlayerManager p) {

        if(tokens.Count < 1) {
            OnFirstToken?.Invoke(this,EventArgs.Empty);
        }
        else {
            OnSecondToken?.Invoke(this,EventArgs.Empty);
        }

        StainedGlassPiece token = p.playerInteract.holdItem.GetComponent<StainedGlassPiece>();
        p.playerInteract.isHolding = false;
        token.isHeld = false;
        token.isInWall = true;
        token.transform.SetParent(tokenTransforms[tokens.Count],false);
        token.transform.localPosition = Vector3.zero;
        token.transform.localRotation = Quaternion.identity;
        tokens.Add(token);

    }

    public bool CanInteract() {
        if(player.playerInteract.isHolding) {
            if(player.playerInteract.holdItem.GetComponent<StainedGlassPiece>() != null) {
                return true;
            }
        }
        return false;
    }

}