using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : MonoBehaviour,IInteractable,IDropable {

    private PlayerManager player;
    private BoxCollider boxCollider;
    private Rigidbody body;

    private bool isHeld = false;

    public void Start() {
        boxCollider = GetComponent<BoxCollider>();
        body = GetComponent<Rigidbody>();
    }

    public void Update() {
        if(isHeld) {
            transform.position = player.playerInteract.holdTransform.position;
            transform.rotation = player.playerInteract.holdTransform.rotation;
            boxCollider.enabled = false;
            body.useGravity = false;
        }
        else {
            boxCollider.enabled = true;
            body.useGravity = true;
        }
    }
    
    public void Interact(PlayerManager p) {
        player = p;
        player.playerInteract.GrabAndHoldItem(this);
        isHeld = true;
        //p.playerInteract.GrabItem();
        //StartCoroutine(DestroyItem());
    }

    public void DropItem() {
        isHeld = false;
    }

    public IEnumerator DestroyItem() {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public bool CanInteract() {
        return true;
    }
}