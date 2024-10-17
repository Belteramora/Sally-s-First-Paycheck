using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : Interactable
{
    public Animator animator;
    private bool isOpen;

    private void Start()
    {
        isOpen = animator.GetBool("isOpen");
    }

    protected override void Interact()
    {
        isOpen = !isOpen;
        animator.SetBool("isOpen", isOpen);
    }
}
