using UnityEngine;
using Zenject;

namespace PlayerContent
{
    public class PlayerLook : MonoBehaviour,ILook
    {
        [SerializeField] private float _mouseSensitivity = 2f;
    
        private Camera _cameraTransform;
        private float _angle = 90;
        private float _mouseX;
        private float _mouseY;
        private Vector3 _currentRotation;
        private float _desiredRotationX;

        [Inject]
        private void Construct(Camera camera)
        {
            _cameraTransform = camera;
        }   
    
        public void Looking(Vector2 direction)
        {
            _mouseX = direction.x * _mouseSensitivity;
            _mouseY = direction.y * _mouseSensitivity;
            transform.Rotate(Vector3.up * _mouseX);

            _currentRotation = _cameraTransform.transform.rotation.eulerAngles;
            _desiredRotationX = _currentRotation.x - _mouseY;

            if (_desiredRotationX > 180)
                _desiredRotationX -= 360;

            _desiredRotationX = Mathf.Clamp(_desiredRotationX, -_angle, _angle);
            _cameraTransform.transform.rotation = Quaternion.Euler(_desiredRotationX, _currentRotation.y, _currentRotation.z);
        }
    }
}