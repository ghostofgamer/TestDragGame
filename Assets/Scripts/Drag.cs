using UnityEngine;

public class Drag : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _pickUpDistance = 5f;
    [SerializeField] private LayerMask _itemLayer;
    [SerializeField] private Transform _itemPosition;

    private GameObject _pickedItem;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_pickedItem == null)
                PickUpItem();
            else
                DropItem();
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (_pickedItem != null)
                ThrowItem();
        }
    }

    private void PickUpItem()
    {
        Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _pickUpDistance, _itemLayer))
        {
            _pickedItem = hit.collider.gameObject;
            _pickedItem.GetComponent<Rigidbody>().isKinematic = true;
            _pickedItem.transform.position = _itemPosition.position;
            _pickedItem.transform.parent = _playerCamera.transform;
        }
    }

    private void DropItem()
    {
        _pickedItem.GetComponent<Rigidbody>().isKinematic = false;
        ClearPickedItem();
    }

    private void ThrowItem()
    {
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