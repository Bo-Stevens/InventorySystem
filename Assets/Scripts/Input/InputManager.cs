using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public ControlScheme InputActionSet;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        InputActionSet = new ControlScheme();
        
        InputActionSet.Combat.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
