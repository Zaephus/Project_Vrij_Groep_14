using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour {

    private PlayerManager player;

    private IInteractable interactable;
    private IDropable dropable;
    private Vector3 itemPosition;

    private bool canInteract = false;
    private bool isHolding = false;

    public TMP_Text interactText;
    public Transform handIKTarget;
    public Transform holdTransform;

    public void OnStart(PlayerManager p) {
        player = p;
    }

    public void OnUpdate() {

        if(canInteract) {
            interactText.enabled = true;

            if(Input.GetButtonDown("Interact")) {
                player.animator.SetBool("IsHolding",false);
                interactable.Interact(player);
                canInteract = false;
            }
        }
        else {
            interactText.enabled = false;
        }

        if(isHolding && Input.GetKeyDown("h")) {
            dropable.DropItem();
            player.animator.SetBool("IsHolding",false);
        }

    }

    public void GrabItem() {
        handIKTarget.position = itemPosition;
        player.animator.SetTrigger("GrabbedItem");

    }

    public void GrabAndHoldItem() {
        handIKTarget.position = itemPosition;
        player.animator.SetTrigger("GrabbedItem");
        player.animator.SetBool("IsHolding",true);
        isHolding = true;
    }

    public void OnTriggerEnter(Collider other) {
        if(other.GetComponent<IInteractable>() != null) {
            canInteract = other.GetComponent<IInteractable>().CanInteract();
            interactable = other.GetComponent<IInteractable>();
            itemPosition = other.transform.position;
            if(other.GetComponent<IDropable>() != null) {
                dropable = other.GetComponent<IDropable>();
            }
        }
    }

    public void OnTriggerExit(Collider other) {
        if(other.GetComponent<IInteractable>() != null) {
            canInteract = false;
        }
    }

}