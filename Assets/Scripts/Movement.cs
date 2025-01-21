using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    private const string Jump = "Jump";
    
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Joystick _rotationJoystick;
    [SerializeField]private float _walkSpeed = 5f; 
    [SerializeField]private float _jumpForce = 5f; 
    [SerializeField]private Transform _cameraTransform; 
    [SerializeField]private float _mouseSensitivity = 2f; 

    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private bool _isJumping;
    private float _moveSpeed;
    private float _mouseX;
    private float _mouseY;
    private Vector3 _move;
    private float _angle = 90;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        // Cursor.lockState = CursorLockMode.Locked;
    }

    public void Move(Vector2 movement)
    {
        _move = transform.right * -movement.x + transform.forward * -movement.y;
        _controller.Move(_move * _walkSpeed * Time.deltaTime);
    }
    
    
    
    private void Update()
    {
        /*float horizontal = _joystick.Horizontal;
        float vertical = _joystick.Vertical;
        _move = transform.right * -horizontal + transform.forward * -vertical;
        _controller.Move(_move * _walkSpeed * Time.deltaTime);*/
        
        if (_controller.isGrounded)
        {
            _playerVelocity.y = 0f;
            _isJumping = false;
        }

        if (Input.GetButtonDown(Jump) && !_isJumping)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpForce * -2f * Physics.gravity.y);
            _isJumping = true;
        }
        
        _playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);

        /*_mouseX = _rotationJoystick.Horizontal*_mouseSensitivity;
        _mouseY = _rotationJoystick.Vertical*_mouseSensitivity;
        
       
        transform.Rotate(Vector3.up * _mouseX);

        Vector3 currentRotation = _cameraTransform.rotation.eulerAngles;
        float desiredRotationX = currentRotation.x - _mouseY;
        
        if (desiredRotationX > 180)
            desiredRotationX -= 360;
        
        desiredRotationX = Mathf.Clamp(desiredRotationX, -_angle, _angle);
        _cameraTransform.rotation = Quaternion.Euler(desiredRotationX, currentRotation.y, currentRotation.z);*/
    }
}