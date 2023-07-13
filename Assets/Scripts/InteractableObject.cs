using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DebugCircleRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
[ExecuteAlways]
public abstract class InteractableObject : MonoBehaviour
{
    [Min(0)]public float InteractionRadius;

    DebugCircleRenderer circleRenderer;
    CircleCollider2D circleCollider;

    private void OnEnable()
    {
        circleRenderer = GetComponent<DebugCircleRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.isTrigger = true;
    }

    private void OnValidate()
    {
        StartCoroutine(UpdateRadius());
    }

    IEnumerator UpdateRadius()
    {
        yield return null;
        circleRenderer.SetRadius(InteractionRadius);
        circleCollider.radius = InteractionRadius;
    }
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;
        player.InteractablesInRange.Add(this);
        if (player.InteractablesInRange.Count == 1) player.StartCoroutine(player.Cycle());
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;
        player.InteractablesInRange.Remove(this);
    }
    
    public abstract void Interact();
    
    public DebugCircleRenderer GetCircleRenderer()
    {
        return circleRenderer;
    }
}
