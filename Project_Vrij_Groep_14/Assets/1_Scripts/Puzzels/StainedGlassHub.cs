using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StainedGlassHub : MonoBehaviour,IInteractable {

    private PlayerManager player;

    public List<Transform> tokenTransforms = new List<Transform>();
    public List<StainedGlassPiece> tokens = new List<StainedGlassPiece>();

    public void Start() {
        player = FindObjectOfType<PlayerManager>();
    }

    public void Interact(PlayerManager p) {

        StainedGlassPiece token = p.playerInteract.holdTransform.GetChild(0).GetComponent<StainedGlassPiece>();
        p.playerInteract.isHolding = false;
        p.animator.SetBool("IsHolding",false);
        token.isHeld = false;
        token.isInWall = true;
        token.transform.SetParent(tokenTransforms[tokens.Count],false);
        //token.transform.position = Vector3.zero;
        tokens.Add(token);
    }

    public bool CanInteract() {
        if(player.playerInteract.isHolding) {
            if(player.playerInteract.holdTransform.GetChild(0).GetComponent<StainedGlassPiece>() != null) {
                return true;
            }
        }
        return false;
    }

}