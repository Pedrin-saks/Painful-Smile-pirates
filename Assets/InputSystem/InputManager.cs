using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private PlayerInputAction playerActions;
    private InputAction movementInput;

    public Vector2 movement { get; private set; }

    public delegate void ActionButton();
    public event ActionButton ButtonShootDown;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        playerActions = new PlayerInputAction();
    }

    private void OnEnable()
    {
        movementInput = playerActions.ActionMap.Movement;

        playerActions.ActionMap.Attack1.started += OnButtonShoot;
        playerActions.ActionMap.Attack1.canceled += OnButtonShoot;

        playerActions.Enable();
    }

    private void OnDisable()
    {

        playerActions.ActionMap.Attack1.started -= OnButtonShoot;
        playerActions.ActionMap.Attack1.canceled -= OnButtonShoot;

        playerActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        movement = movementInput.ReadValue<Vector2>();
    }

    private void OnButtonShoot(InputAction.CallbackContext value)
    {
        if (value.started)
            ButtonShootDown?.Invoke();
    }
}
