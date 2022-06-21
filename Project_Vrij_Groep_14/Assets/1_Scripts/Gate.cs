using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Animator animator;

    [Header("Audio")]
    [SerializeField] AudioManager audioManager;
    public void Open()
    {
        audioManager.Play("Open");
        animator.SetBool("Open", true);
    }

    public void Close()
    {
        animator.SetBool("Open", false);
    }
}
