using System;
using UnityEngine;
using inputSystem = UnityEngine.InputSystem;
public class InputController : MonoBehaviour
{
    public static InputController Instance;
    public event Action<Vector2> OnTouch;
    private Input _input;
    private Vector2 _touchPosition;


    private void Awake()
    {
        Instance = this;
        _input = new Input();        
        _input.Touch.Position.performed += position => _touchPosition = position.ReadValue<Vector2>();
        _input.Touch.Phase.performed += phase => SendTouchPosition(phase);
    }

    private void SendTouchPosition(inputSystem.InputAction.CallbackContext phase)
    {       
        if (phase.ReadValue<inputSystem.TouchPhase>() == inputSystem.TouchPhase.Ended)
            OnTouch?.Invoke(_touchPosition);
    }
  


    private void OnEnable() => _input.Enable();
    private void OnDisable() => _input.Disable();
}
