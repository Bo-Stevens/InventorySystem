using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class Container : InteractableObject
{
    public Inventory Inventory;
    bool open;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        if (!open)
        {
            Inventory.OpenInventory();
            animator.Play("Opening");
        }
        else
        {
            Inventory.CloseInventory();
            animator.Play("Closing");
        }
        open = !open;
    }

}
