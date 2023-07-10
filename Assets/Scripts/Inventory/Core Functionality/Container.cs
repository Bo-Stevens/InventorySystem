using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : InteractableObject
{
    static Color interactionRadiusColor = new Color(0.95f, 0.1f, 0.1f, 0.1f);
    [SerializeField] Texture tex;
    [SerializeField] Material mat;
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
