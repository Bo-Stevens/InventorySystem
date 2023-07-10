using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DebugManager : MonoBehaviour
{
    public static DebugManager Instance;
    public List<DebugCircleRenderer> CircleRenderers;
    [Range(.05f, 2)] public float DebugLineWidth;
    float oldDebugLineRadius;
    bool activeState;

    private void OnEnable()
    {
        Instance = this;
    }

    private void OnValidate()
    {
        if (Instance == null) return;
        if (DebugLineWidth != oldDebugLineRadius) UpdateRadii();

        oldDebugLineRadius = DebugLineWidth;
    }

    void UpdateRadii()
    {
        for(int i = 0; i < CircleRenderers.Count; i++)
        {
            CircleRenderers[i].CreatePoints();
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
