using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : MonoBehaviour,IInteractable {

    private PlayerManager player;
    private BoxCollider boxCollider;

    private bool isHeld = false;

    public void Start() {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void Update() {
        if(isHeld) {
            transform.position = player.playerInteract.holdTransform.position;
            transform.rotation = player.playerInteract.holdTransform.rotation;
            boxCollider.enabled = false;
        }
        else {
            boxCollider.enabled = true;
        }
    }
    public void Interact(PlayerManager p) {
        player = p;
        player.playerInteract.GrabAndHoldItem();
        isHeld = true;
        //p.playerInteract.GrabItem();
        //StartCoroutine(DestroyItem());
    }

    public IEnumerator DestroyItem() {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public bool CanInteract() {
        return true;
    }
}