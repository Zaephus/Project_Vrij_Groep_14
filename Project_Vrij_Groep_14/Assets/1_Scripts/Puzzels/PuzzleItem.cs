using System.Collections;
using UnityEngine;

public class PuzzleItem : MonoBehaviour,IInteractable,IDropable
{

    private PlayerManager player;
    [SerializeField] private Collider itemCollider;
    private Rigidbody body;

    private bool isHeld = false;
    public bool onScale = false;

    public void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        if(isHeld)
        {
            transform.position = player.playerInteract.holdTransform.position;
            transform.rotation = player.playerInteract.holdTransform.rotation;
            itemCollider.enabled = false;
            body.useGravity = false;
        }
        else 
        {
            itemCollider.enabled = true;
            body.useGravity = true;
        }
    }

    public void Interact(PlayerManager p) 
    {
        player = p;
        player.playerInteract.GrabAndHoldItem(this);
        isHeld = true;
    }

    public void DropItem() 
    {
        isHeld = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < FindObjectOfType<HavenPuzzle>().scaleColliders.Count; i++)
        {
            if (other == FindObjectOfType<HavenPuzzle>().scaleColliders[i])
            {
                onScale = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < FindObjectOfType<HavenPuzzle>().scaleColliders.Count; i++)
        {
            if (other == FindObjectOfType<HavenPuzzle>().scaleColliders[i])
            {
                onScale = false; ;
            }
        }
    }

    public bool CanInteract() 
    {
        return true;
    }
}
