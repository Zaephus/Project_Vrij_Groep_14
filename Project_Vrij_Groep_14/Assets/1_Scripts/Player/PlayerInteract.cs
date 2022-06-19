using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour {

    private PlayerManager player;

    private IInteractable interactable;
    [HideInInspector] public IDropable dropable;
    private Vector3 itemPosition;

    [HideInInspector] public bool canInteract = false;
    [HideInInspector] public bool isHolding = false;

    public TMP_Text interactText;
    public Transform handIKTarget;
    public Transform holdTransform;


    [Header("Audio")]
    [SerializeField] AudioManager audio;

    public void OnStart(PlayerManager p) {
        player = p;
    }

    public void OnUpdate() {

        player.animator.SetBool("IsHolding",isHolding);

        if(canInteract) {
            interactText.enabled = true;

            if(Input.GetButtonDown("Interact")) {
                interactable.Interact(player);
                canInteract = false;
            }
        }
        else {
            interactText.enabled = false;
        }

        if(isHolding && Input.GetKeyDown("h")) {
            dropable.DropItem();
            isHolding = false;
            player.animator.SetBool("IsHolding",false);
        }

    }

    public void GrabItem() {

        audio.Play("Pickup");

        if(isHolding) {
            dropable?.DropItem();
            isHolding = false;
        }
        handIKTarget.position = itemPosition;
        player.animator.SetTrigger("GrabbedItem");

    }

    public void GrabAndHoldItem(IDropable d) {
        if(isHolding) {
            dropable?.DropItem();
        }
        dropable = d;
        handIKTarget.position = itemPosition;
        player.animator.SetTrigger("GrabbedItem");
        isHolding = true;
    }

    public void OnTriggerEnter(Collider other) {
        if(other.GetComponent<IInteractable>() != null) {
            canInteract = other.GetComponent<IInteractable>().CanInteract();
            interactable = other.GetComponent<IInteractable>();
            itemPosition = other.transform.position;
            // if(other.GetComponent<IDropable>() != null) {
            //     dropable = other.GetComponent<IDropable>();
            // }
        }
    }

    public void OnTriggerExit(Collider other) {
        if(other.GetComponent<IInteractable>() != null) {
            canInteract = false;
        }
    }

}