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
        InputManager.Instance.InputActionSet.Combat.Jump.performed += PlayerJump;
        InputManager.Instance.InputActionSet.Combat.Attack.performed += Attacked;
        OnCharacterLanded += Landed;
        Init();
    }

    private void FixedUpdate()
    {
        MovementDirection = InputManager.Instance.InputActionSet.Combat.Movement.ReadValue<Vector2>();
        SetVelocity(new Vector3(MovementDirection.x * MovementSpeed, GetVelocity().y));
    }

    void PlayerJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump key pressed!");
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
