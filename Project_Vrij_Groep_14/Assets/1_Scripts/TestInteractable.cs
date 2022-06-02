using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : MonoBehaviour,IInteractable {
    public void Interact(PlayerManager p) {
        Destroy(this.gameObject);
    }

    public bool CanInteract() {
        return true;
    }
}