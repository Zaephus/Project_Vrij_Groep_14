using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportPlayer : MonoBehaviour, IInteractable
{
    public Transform targetLocation;

    [Header("Audio")]
    [SerializeField] AudioManager audio;

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
        audio.Play("Deur");
        playerPosition.position = targetLocation.position;
    }
}
