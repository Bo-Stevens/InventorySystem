using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovementComponent : CharacterMovementComponent
{
    //TEMPORARY
    public static PlayerMovementComponent Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (InputManager.InputActionSet == null) Debug.LogWarning("Null");
        if (InputManager.InputActionSet.Combat.Jump == null) Debug.LogWarning("Null");
        InputManager.InputActionSet.Combat.Jump.performed += PlayerJump;
        InputManager.InputActionSet.Combat.Attack.performed += Attacked;
        OnCharacterLanded += Landed;
        Init();
    }

    private void FixedUpdate()
    {
        MovementDirection = InputManager.InputActionSet.Combat.Movement.ReadValue<Vector2>();
        SetVelocity(new Vector3(MovementDirection.x * MovementSpeed, GetVelocity().y));
    }

    void PlayerJump(InputAction.CallbackContext context)
    {
        Jump();
    }

    void Attacked(InputAction.CallbackContext context)
    {
        //if (context.performed) GetComponent<CombatComponent>().Attack();
    }

    void Landed()
    {

    }

}
