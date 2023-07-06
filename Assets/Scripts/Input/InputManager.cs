using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static ControlScheme InputActionSet;
    private void Awake()
    {
        InputActionSet = new ControlScheme();

        InputActionSet.Combat.Enable();
    }

    public static void ChangeActionMap(InputActionMap newActionMap)
    {
        if (newActionMap.enabled) return;

        InputActionSet.Disable();
        newActionMap.Enable();

    }

}
