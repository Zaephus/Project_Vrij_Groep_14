using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StainedGlassPiece : MonoBehaviour,IInteractable,IDropable {

    private PlayerManager player;

    private Collider itemCollider;
    [HideInInspector] public Rigidbody body;

    [HideInInspector] public bool isHeld = false;
    [HideInInspector] public bool isInWall = false;

    public void Start() {
        player = FindObjectOfType<PlayerManager>();
        player.playerInteract.canInteract = false;
        player.playerInteract.isHolding = true;
        itemCollider = GetComponent<Collider>();
        body = GetComponent<Rigidbody>();
        isHeld = true;
    }

    public void Update() {
        if(isHeld) {
            transform.position = player.playerInteract.holdTransform.position;
            transform.rotation = player.playerInteract.holdTransform.rotation;
            itemCollider.enabled = false;
            body.useGravity = false;
        }
        else if(isInWall) {
            itemCollider.enabled = false;
            Destroy(body);
        }
        else {
            itemCollider.enabled = true;
            body.useGravity = true;
        }
        
    }

    public void Interact(PlayerManager p) {
        player = p;
        player.playerInteract.GrabAndHoldItem(this);
        player.playerInteract.holdItem = this.gameObject;
        isHeld = true;
    }

    public void DropItem() {
        isHeld = false;
        //isInWall = false;
    }

    public bool CanInteract() {
        return true;
    }

}