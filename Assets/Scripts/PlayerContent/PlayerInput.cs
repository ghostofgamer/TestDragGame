using UnityEngine;
using Zenject;

namespace PlayerContent
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Joystick _moveJoystick;
        [SerializeField] private Joystick _lookJoystick;
    
        private Movement _movement;
        private PlayerLook _playerLook;
        private Vector2 _moveInput;
        private Vector2 _lookInput;
    
        private void Update()
        {
            _moveInput = new Vector2(_moveJoystick.Horizontal, _moveJoystick.Vertical);
            _lookInput = new Vector2(_lookJoystick.Horizontal, _lookJoystick.Vertical);
            _movement.Move(_moveInput);
            _playerLook.Looking(_lookInput);
        }

        [Inject]
        private void Construct(Movement movement, PlayerLook playerLook)
        {
            _movement = movement;
            _playerLook = playerLook;
            Debug.Log("PlayerInput:Construct ");
        }
    }
}
