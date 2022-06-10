using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable {
    public void Interact(PlayerManager p);
    public bool CanInteract();
}

public interface IDropable {
    public void DropItem();
}