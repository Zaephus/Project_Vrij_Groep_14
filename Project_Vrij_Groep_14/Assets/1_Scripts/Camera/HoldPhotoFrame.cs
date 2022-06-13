using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldPhotoFrame : MonoBehaviour,IDropable {

    private PlayerManager player;

    public Image photoHoldDisplayArea;

    public void Start() {
        player = FindObjectOfType<PlayerManager>();
    }

    public void Update() {
        transform.position = player.playerInteract.holdTransform.position;
        transform.rotation = player.playerInteract.holdTransform.rotation;
    }

    public void DropItem() {
        Destroy(this.gameObject);
    }

}