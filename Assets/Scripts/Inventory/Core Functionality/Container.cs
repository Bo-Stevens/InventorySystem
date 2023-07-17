using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class Container : InteractableObject
{
    [HideInInspector] public bool Open;
    public Inventory ContainerInventory;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        if (!Open)
        {
            ContainerInventory.OpenInventory();
            animator.Play("Opening");
        }
        else
        {
            ContainerInventory.CloseInventory();
            animator.Play("Closing");
        }
        Open = !Open;
    }

}
