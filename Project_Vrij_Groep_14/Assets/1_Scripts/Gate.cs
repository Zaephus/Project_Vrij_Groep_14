using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Animator animator;
    public void Open()
    {
        animator.SetBool("Open", true);
    }

    public void Close()
    {
        animator.SetBool("Open", false);
    }
}
