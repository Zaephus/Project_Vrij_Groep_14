using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldCamera : MonoBehaviour, IInteractable
{
    public event EventHandler OnPhotoCameraPickedUp;

    public GameObject playerCameraHandheld;

    public void Interact(PlayerManager p)
    {
        playerCameraHandheld.SetActive(true);
        OnPhotoCameraPickedUp?.Invoke(this, EventArgs.Empty);

        Destroy(gameObject);
    }

    public bool CanInteract() 
    {
        return true;
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
