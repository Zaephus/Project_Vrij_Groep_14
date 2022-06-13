using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StainedGlassPiece : MonoBehaviour,IInteractable,IDropable {

    private PlayerManager player;

    private Collider itemCollider;
    private Rigidbody body;

    private bool isHeld = false;

    public void Start() {
        itemCollider = GetComponent<Collider>();
        body = GetComponent<Rigidbody>();
    }

    public void Update() {
        if(isHeld) {
            transform.position = player.playerInteract.holdTransform.position;
            transform.rotation = player.playerInteract.holdTransform.rotation;
            itemCollider.enabled = false;
            body.useGravity = false;
        }
        else {
            itemCollider.enabled = true;
            body.useGravity = true;
        }
    }

    public void Interact(PlayerManager p) {
        player = p;
        player.playerInteract.GrabAndHoldItem(this);
        isHeld = true;
    }

    public void DropItem() {
        isHeld = false;
    }

    public bool CanInteract() {
        return true;
    }
    
}