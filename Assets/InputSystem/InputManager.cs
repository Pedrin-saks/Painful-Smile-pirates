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
    public event ActionButton ButtonTripleShootDown;

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

        playerActions.ActionMap.Attack1.started += OnButtonShot;
        playerActions.ActionMap.Attack2.started += OnButtonTripleShot;

        playerActions.Enable();
    }

    private void OnDisable()
    {

        playerActions.ActionMap.Attack1.started -= OnButtonShot;
        playerActions.ActionMap.Attack2.started -= OnButtonTripleShot;

        playerActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        movement = movementInput.ReadValue<Vector2>();
    }

    private void OnButtonShot(InputAction.CallbackContext value)
    {
        if (value.started)
            ButtonShootDown?.Invoke();
    }

    private void OnButtonTripleShot(InputAction.CallbackContext value)
    {
        if (value.started)
            ButtonTripleShootDown?.Invoke();
    }
}
