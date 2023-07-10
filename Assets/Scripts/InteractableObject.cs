using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DebugCircleRenderer))]
[ExecuteAlways]
public class InteractableObject : MonoBehaviour
{
    [Min(0)]public float InteractionRadius;
    DebugCircleRenderer circleRenderer;
    private void OnEnable()
    {
        circleRenderer = GetComponent<DebugCircleRenderer>();
    }

    private void OnValidate()
    {
        StartCoroutine(UpdateRadius());
    }

    IEnumerator UpdateRadius()
    {
        yield return null;
        circleRenderer.SetRadius(InteractionRadius);
    }
}
