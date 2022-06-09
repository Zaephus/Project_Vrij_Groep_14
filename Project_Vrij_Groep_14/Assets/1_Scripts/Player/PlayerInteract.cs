using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour {

    private PlayerManager player;

    private IInteractable interactable;
    private Vector3 interactablePosition;

    private bool canInteract = false;

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

    }

    public void GrabItem() {
        handIKTarget.position = interactablePosition;
        player.animator.SetTrigger("GrabbedItem");

    }

    public void GrabAndHoldItem() {
        handIKTarget.position = interactablePosition;
        player.animator.SetTrigger("GrabbedItem");
        player.animator.SetBool("IsHolding",true);
    }

    public void OnTriggerEnter(Collider other) {
        if(other.GetComponent<IInteractable>() != null) {
            canInteract = other.GetComponent<IInteractable>().CanInteract();
            interactable = other.GetComponent<IInteractable>();
            interactablePosition = other.transform.position;
        }
    }

    public void OnTriggerExit(Collider other) {
        if(other.GetComponent<IInteractable>() != null) {
            canInteract = false;
        }
    }

}