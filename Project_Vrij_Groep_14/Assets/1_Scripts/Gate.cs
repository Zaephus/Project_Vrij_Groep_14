using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Animator animator;

    [Header("Audio")]
    [SerializeField] AudioManager audio;
    public void Open()
    {
        audio.Play("Open");
        animator.SetBool("Open", true);
    }

    public void Close()
    {
        animator.SetBool("Open", false);
    }
}
