using UnityEngine;
using Zenject;

namespace PlayerContent
{
    public class Drag : MonoBehaviour
    {
        private Camera _playerCamera;
    
        [SerializeField] private float _pickUpDistance = 5f;
        [SerializeField] private LayerMask _itemLayer;
        [SerializeField] private Transform _itemPosition;
    
        private GameObject _pickedItem;
        private Vector2 _screenCenter;
        private float _factor = 2;
        private Ray _ray;
        private RaycastHit _hit;

        [Inject]
        private void Construct(Camera camera)
        {
            _playerCamera = camera;
        }   
    
        public void PickUpItem()
        {
            if (_pickedItem != null) return;
        
            _screenCenter = new Vector2(Screen.width / _factor, Screen.height / _factor);
            _ray = _playerCamera.ScreenPointToRay(_screenCenter);

            if (Physics.Raycast(_ray, out _hit, _pickUpDistance, _itemLayer))
            {
                _pickedItem = _hit.collider.gameObject;
                _pickedItem.GetComponent<Rigidbody>().isKinematic = true;
                _pickedItem.transform.position = _itemPosition.position;
                _pickedItem.transform.parent = _playerCamera.transform;
            }
        }

        public void DropItem()
        {
            if (_pickedItem == null) return;
        
            _pickedItem.GetComponent<Rigidbody>().isKinematic = false;
            ClearPickedItem();
        }

        public void ThrowItem()
        {
            if (_pickedItem == null) return;
        
            _pickedItem.GetComponent<Rigidbody>().isKinematic = false;
            _pickedItem.GetComponent<Rigidbody>().AddForce(_playerCamera.transform.forward * 10f, ForceMode.Impulse);
            ClearPickedItem();
        }

        private void ClearPickedItem()
        {
            _pickedItem.transform.parent = null;
            _pickedItem = null;
        }
    }
}