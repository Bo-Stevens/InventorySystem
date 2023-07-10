using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[ExecuteAlways]
public class DebugCircleRenderer : MonoBehaviour
{
    [HideInInspector] public LineRenderer lineRenderer;
    [HideInInspector] public Color color;
    float oldRadius;
    float radius;
    int segments;

    private void OnEnable()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        StartCoroutine(AddRendererToList());
    }

    IEnumerator AddRendererToList()
    {
        yield return null;
        lineRenderer.material = DebugManager.Instance.DebugLineMaterial;
        if (DebugManager.Instance.CircleRenderers.Contains(this)) yield break;
        DebugManager.Instance.CircleRenderers.Add(this);
    }

    private void OnValidate()
    {
        if (lineRenderer.positionCount == segments + 1 && oldRadius == radius) return;
        oldRadius = radius;

        StartCoroutine(CreatePoints());
    }

    public IEnumerator CreatePoints()
    {
        yield return null;
        float circleProgress;
        float currentRadian;
        float x;
        float y;
        lineRenderer.widthMultiplier = DebugManager.Instance.DebugLineWidth;
        segments = DebugManager.Instance.DebugCircleSegments;
        lineRenderer.positionCount = segments + 1;

        for (int i = 0; i < segments + 1; i++)
        {
            circleProgress = i / (float) segments;
            currentRadian = Mathf.PI * 2f * circleProgress;
            x = Mathf.Cos(currentRadian) * radius;
            y = Mathf.Sin(currentRadian) * radius;
            lineRenderer.SetPosition(i, new Vector3(x, y, -10));
        }
        currentRadian = Mathf.PI * 2f * 1.05f;
        x = radius;
        y = .2f * lineRenderer.widthMultiplier;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(x, y, -10));
    }

    public void SetRadius(float newRadius)
    {
        radius = newRadius;
        StartCoroutine(CreatePoints());
    }
    
}
