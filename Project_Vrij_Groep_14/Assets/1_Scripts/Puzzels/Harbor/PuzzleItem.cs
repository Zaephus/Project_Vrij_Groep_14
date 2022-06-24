using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleItem : MonoBehaviour,IInteractable,IDropable
{

    private PlayerManager player;
    public List<Collider> overworldItemColliders = new List<Collider>();
    //public List<Collider> cameraWorldItemColliders = new List<Collider>();
    public Rigidbody body;

    public float mass = 1;

    [HideInInspector] public bool isHeld = false;
    public bool onScale = false;

    public void Start() {}

    public void Update()
    {
        if(isHeld)
        {
            transform.position = player.playerInteract.holdTransform.position;
            transform.rotation = player.playerInteract.holdTransform.rotation;
            foreach(Collider c in overworldItemColliders) {
                c.enabled = false;
            }
            body.useGravity = false;
        }
        else if(onScale)
        {
            foreach(Collider c in overworldItemColliders) {
                c.enabled = false;
            }
            body.useGravity = false;
            body.velocity = Vector3.zero;
        }
        else 
        {
            foreach(Collider c in overworldItemColliders) {
                c.enabled = true;
            }
            body.useGravity = true;
        }
    }

    public void Interact(PlayerManager p) 
    {
        player = p;
        player.playerInteract.GrabAndHoldItem(this);
        player.playerInteract.holdItem = this.gameObject;
        isHeld = true;
    }

    public void DropItem() 
    {
        isHeld = false;
    }

    public bool CanInteract() 
    {
        return true;
    }
}
