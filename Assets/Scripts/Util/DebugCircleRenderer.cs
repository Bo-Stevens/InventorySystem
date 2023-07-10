using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[ExecuteAlways]
public class DebugCircleRenderer : MonoBehaviour
{
    [Min(0.1f)] public float radius;
    [HideInInspector] public LineRenderer lineRenderer;
    float oldRadius;

    [Min(4)] [SerializeField] int segments = 50;

    private void OnEnable()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = segments + 1;
        lineRenderer.useWorldSpace = false;
        StartCoroutine(AddRendererToList());
    }

    IEnumerator AddRendererToList()
    {
        if (DebugManager.Instance.CircleRenderers.Contains(this)) yield break;
        yield return null;
        DebugManager.Instance.CircleRenderers.Add(this);
    }

    private void OnValidate()
    {
        if (lineRenderer == null || lineRenderer.positionCount == segments + 1 && oldRadius == radius) return;
        oldRadius = radius;
        lineRenderer.positionCount = segments + 1;

        CreatePoints();
    }
    public void CreatePoints()
    {
        float circleProgress;
        float currentRadian;
        float x;
        float y;
        lineRenderer.widthMultiplier = DebugManager.Instance.DebugLineWidth;

        for (int i = 0; i < segments + 1; i++)
        {
            circleProgress = i / (float) segments;
            currentRadian = Mathf.PI * 2f * circleProgress;
            x = Mathf.Cos(currentRadian) * radius;
            y = Mathf.Sin(currentRadian) * radius;
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
        currentRadian = Mathf.PI * 2f * 1.05f;
        x = radius;
        y = .2f * lineRenderer.widthMultiplier;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(x, y, 0));
    }

}
