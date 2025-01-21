using UnityEngine;

namespace PlayerContent
{
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour,IMovement
    {
        [SerializeField]private float _walkSpeed = 5f; 

        private CharacterController _controller;
        private Vector3 _move;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        public void Move(Vector2 direction)
        {
            _move = transform.right * -direction.x + transform.forward * -direction.y;
            _controller.Move(_move * _walkSpeed * Time.deltaTime);
        }
    }
}