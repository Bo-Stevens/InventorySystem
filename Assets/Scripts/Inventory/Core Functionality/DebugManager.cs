using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DebugManager : MonoBehaviour
{
    public static DebugManager Instance;
    public List<DebugCircleRenderer> CircleRenderers;
    public Material DebugLineMaterial;
    [Range(.05f, 2)] public float DebugLineWidth;
    [Range(4,50)] public int DebugCircleSegments = 50;

    float oldDebugLineRadius;
    float oldCircleSegments;
    bool activeState;

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
    
    public void ToggleActivationState()
    {
        activeState = !activeState;
        for (int i = 0; i < CircleRenderers.Count; i++)
        {
            CircleRenderers[i].lineRenderer.enabled = activeState;
        }

    }
}
