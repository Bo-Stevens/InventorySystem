using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DebugManager : MonoBehaviour
{
    public static DebugManager Instance;
    [HideInInspector] public bool DebugLinesActive;

    public List<DebugCircleRenderer> CircleRenderers;
    public Material DebugLineMaterial;
    [Range(.05f, 2)] public float DebugLineWidth;
    [Range(4,50)] public int DebugCircleSegments = 50;

    InteractableObject highlightedDebugCollider;
    float oldDebugLineRadius;
    float oldCircleSegments;

    private void OnEnable()
    {
        Instance = this;
        CircleRenderers = new List<DebugCircleRenderer>();
    }

    private void OnValidate()
    {
        if (DebugLineWidth != oldDebugLineRadius || DebugCircleSegments != oldCircleSegments) StartCoroutine(UpdateCircleRenderers());

        oldDebugLineRadius = DebugLineWidth;
        oldCircleSegments = DebugCircleSegments;
    }

    IEnumerator UpdateCircleRenderers()
    {
        yield return null;
        for(int i = 0; i < CircleRenderers.Count; i++)
        {
            if (CircleRenderers[i] == null) CircleRenderers.RemoveAt(i);
            StartCoroutine(CircleRenderers[i].CreatePoints());
        }
    }

    public void HighlightClosestDebugCircle(Vector3 around)
    {
        if(highlightedDebugCollider != null) highlightedDebugCollider.GetCircleRenderer().lineRenderer.material.color = highlightedDebugCollider.GetCircleRenderer().baseColor;

        if (PlayerController.Instance.InteractablesInRange.Count == 0) return;
        highlightedDebugCollider = FindClosestOverlappingInteractable(PlayerController.Instance.InteractablesInRange);
        highlightedDebugCollider.GetCircleRenderer().lineRenderer.material.color = Color.green;
    }
    public InteractableObject FindClosestOverlappingInteractable(List<InteractableObject> interactablesInRange)
    {
        InteractableObject closestObject = interactablesInRange[0];
        float closestDistance = float.MaxValue;
        float tempDistance;
        for (int i = 0; i < interactablesInRange.Count; i++)
        {
            tempDistance = Vector2.Distance(PlayerController.Instance.transform.position, interactablesInRange[i].transform.position);
            if (tempDistance < closestDistance)
            {
                closestDistance = tempDistance;
                closestObject = interactablesInRange[i];
            }
        }
        return closestObject;
    }
    public void ToggleActivationState()
    {
        DebugLinesActive = !DebugLinesActive;
        for (int i = 0; i < CircleRenderers.Count; i++)
        {
            CircleRenderers[i].lineRenderer.enabled = DebugLinesActive;
        }

    }
}
