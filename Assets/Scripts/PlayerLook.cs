using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 2f;
    [SerializeField] private Transform _cameraTransform;
    private float _angle = 90;
    private float _mouseX;
    private float _mouseY;

    public void Looking(Vector2 lookInput)
    {
        _mouseX = lookInput.x * _mouseSensitivity;
        _mouseY = lookInput.y * _mouseSensitivity;
        transform.Rotate(Vector3.up * _mouseX);

        Vector3 currentRotation = _cameraTransform.rotation.eulerAngles;
        float desiredRotationX = currentRotation.x - _mouseY;

        if (desiredRotationX > 180)
            desiredRotationX -= 360;

        desiredRotationX = Mathf.Clamp(desiredRotationX, -_angle, _angle);
        _cameraTransform.rotation = Quaternion.Euler(desiredRotationX, currentRotation.y, currentRotation.z);
    }
}