using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBucket : MonoBehaviour,IInteractable {

    private PlayerManager player;

    public List<Transform> bucketTransforms = new List<Transform>();
    public List<PuzzleItem> items = new List<PuzzleItem>();

    public float weight = 0;

    public void Start() {
        player = FindObjectOfType<PlayerManager>();
    }

    public void Update() {
        foreach(PuzzleItem item in items) {
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
        }
    }

    public void Interact(PlayerManager p) {

        if(player.playerInteract.isHolding) {
            if(player.playerInteract.holdItem.GetComponent<PuzzleItem>() != null) {

                PuzzleItem item = player.playerInteract.holdItem.GetComponent<PuzzleItem>();
                p.playerInteract.isHolding = false;
                p.animator.SetBool("IsHolding",false);
                item.isHeld = false;
                item.onScale = true;
                item.transform.SetParent(bucketTransforms[items.Count],false);
                item.transform.localPosition = Vector3.zero;
                item.transform.localRotation = Quaternion.identity;
                
                weight += item.mass;
                
                items.Add(item);

            }
        }
        else if(items.Count != 0) {
            PuzzleItem item = items.Last();
            item.onScale = false;
            item.transform.SetParent(null,true);
            item.Interact(p);

            weight -= item.mass;

            items.Remove(item);
        }

    }

    public bool CanInteract() {
        return (player.playerInteract.isHolding && player.playerInteract.holdItem.GetComponent<PuzzleItem>() != null) || items.Count != 0;
    }

}