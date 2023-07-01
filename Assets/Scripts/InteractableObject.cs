using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public float interactionRadius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnGUI()
    {
        Rect toDraw = new Rect(transform.position - Vector3.one * interactionRadius, Vector2.one * interactionRadius * 2);
        Graphics.DrawTexture(toDraw, InventoryDebugManager.Instance.DebugCircleSprite.texture);
    }
}
