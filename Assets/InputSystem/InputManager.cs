using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private PlayerInputAction playerActions;

    public Vector2 movement { get; private set; }
    private InputAction movementInput;

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

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        movementInput = playerActions.ActionMap.Movement;
        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }

    private void InputMoveRead(Vector2 direction)
    {
        movement = direction;
    }

    // Update is called once per frame
    void Update()
    {
        movement = movementInput.ReadValue<Vector2>();
    }
}
