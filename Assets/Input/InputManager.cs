using System;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    private TouchControls touchControls;

    private void Awake()
    {
        touchControls = new TouchControls();
    }
    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }

    private void Start()
    {
        touchControls.Player.Direction.performed += ctx => PerformedTouch(ctx);
        touchControls.Player.Pause.performed += ctx => PerformedPause(ctx);
    }
    private void PerformedTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch position " + context.ReadValue<Vector2>());
        GameManager.instance.snake.TouchBehaviour(context.ReadValue<Vector2>());
    }
    private void PerformedPause(InputAction.CallbackContext ctx)
    {
        if (GameManager.instance.menu != null)
            GameManager.instance.menu.OnPause();
    }
}
