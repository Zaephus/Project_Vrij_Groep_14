using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportPlayer : MonoBehaviour, IInteractable
{
    public Transform targetLocation;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact(PlayerManager p)
    {
        MovePlayer(p.gameObject.transform);
    }

    void MovePlayer(Transform playerPosition)
    {
        playerPosition.position = targetLocation.position;
    }
}
