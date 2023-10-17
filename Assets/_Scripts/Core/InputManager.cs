using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public PlayerInput playerInput;
    private static InputManager instance;

    private bool mousePressed = false;
    private Vector2 mousePosition = Vector2.zero;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        instance = this;

    }

    public static InputManager GetInstance()
    {
        return instance;
    }

    public void MousePosition(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            mousePosition = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            mousePosition = context.ReadValue<Vector2>();
        }
    }

    public void MousePressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            mousePressed = true;
        }
        else if (context.canceled)
        {
            mousePressed = false;
        }
    }

    public Vector2 GetMousePosition()
    {
        return mousePosition;
    }

    public bool GetMousePressed()
    {
        bool result = mousePressed;
        mousePressed = false;
        return result;
    }
}
