using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Joystick _moveJoystick;
    [SerializeField] private Joystick _lookJoystick;
    [SerializeField]private Movement _movement;
    [SerializeField]private PlayerLook _playerLook;

    private Vector2 _moveInput;
    private Vector2 _lookInput;
    
    private void Update()
    {
        _moveInput = new Vector2(_moveJoystick.Horizontal, _moveJoystick.Vertical);
        _lookInput = new Vector2(_lookJoystick.Horizontal, _lookJoystick.Vertical);
        _movement.Move(_moveInput);
        _playerLook.Looking(_lookInput);
    }
}
