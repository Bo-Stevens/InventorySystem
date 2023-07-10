using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[ExecuteAlways]
public class DebugCircleRenderer : MonoBehaviour
{
    [SerializeField] bool active;


    [Min(0.1f)] public float radius;
    LineRenderer lineRenderer;
    float oldRadius;

    [Min(4)] [SerializeField] int segments = 50;


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = segments + 1;
        lineRenderer.useWorldSpace = false;
        lineRenderer.widthMultiplier = 0.1f;

        CreatePoints();
    }
    private void OnValidate()
    {
        if (!active) { lineRenderer.enabled = false; return; }
        else lineRenderer.enabled = true;
        if(lineRenderer.positionCount == segments + 1 && oldRadius == radius) return;
        oldRadius = radius;
        lineRenderer.positionCount = segments + 1;
        lineRenderer.widthMultiplier = 0.1f;

        CreatePoints();
    }
    void CreatePoints()
    {
        float circleProgress = 0f;
        float currentRadian = 0f;
        float x = 0f;
        float y = 0f;
        for(int i = 0; i < segments + 1; i++)
        {
            circleProgress = i / (float) segments;
            currentRadian = Mathf.PI * 2f * circleProgress;
            x = Mathf.Cos(currentRadian) * radius;
            y = Mathf.Sin(currentRadian) * radius;
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
        currentRadian = Mathf.PI * 2f * 1.05f;
        x = radius;
        y = .05f;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(x, y, 0));
    }

}
