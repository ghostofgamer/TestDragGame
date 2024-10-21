using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";
    private const string Jump = "Jump";
    
    [SerializeField]private float _walkSpeed = 5f; 
    [SerializeField]private float _runSpeed = 10f; 
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
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _moveSpeed = Input.GetKey(KeyCode.LeftShift) ? _runSpeed : _walkSpeed;
        float horizontal = Input.GetAxis(Horizontal);
        float vertical = Input.GetAxis(Vertical);
        _move = transform.right * -horizontal + transform.forward * -vertical;
        _controller.Move(_move * _moveSpeed * Time.deltaTime);
        
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
        _mouseX = Input.GetAxis(MouseX) * _mouseSensitivity;
        _mouseY = Input.GetAxis(MouseY) * _mouseSensitivity;
        transform.Rotate(Vector3.up * _mouseX);

        Vector3 currentRotation = _cameraTransform.rotation.eulerAngles;
        float desiredRotationX = currentRotation.x - _mouseY;
        
        if (desiredRotationX > 180)
            desiredRotationX -= 360;
        
        desiredRotationX = Mathf.Clamp(desiredRotationX, -_angle, _angle);
        _cameraTransform.rotation = Quaternion.Euler(desiredRotationX, currentRotation.y, currentRotation.z);
    }
}