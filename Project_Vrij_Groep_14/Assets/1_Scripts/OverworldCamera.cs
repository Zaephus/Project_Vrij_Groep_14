using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldCamera : MonoBehaviour, IInteractable
{
    public event EventHandler OnPhotoCameraPickedUp;

    public GameObject playerCameraHandheld;

    public bool canInteract = false;

    public void Interact(PlayerManager p)
    {
        p.playerInteract.GrabItem();
        p.hasCamera = true;
        playerCameraHandheld.SetActive(true);
        OnPhotoCameraPickedUp?.Invoke(this, EventArgs.Empty);Destroy(gameObject);

        StartCoroutine(DestroyItem());
    }

    public IEnumerator DestroyItem() {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public bool CanInteract() 
    {
        return canInteract;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
