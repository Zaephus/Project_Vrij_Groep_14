using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : MonoBehaviour,IInteractable {
    public void Interact(PlayerManager p) {
        p.playerInteract.GrabItem();
        StartCoroutine(DestroyItem());
    }

    public IEnumerator DestroyItem() {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public bool CanInteract() {
        return true;
    }
}