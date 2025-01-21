using System.Collections;
using UnityEngine;

public class GarageDoor : MonoBehaviour
{
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(1f);
    private Coroutine _coroutine;
    private float _elapsedTime;
    private float _duration = 1.5f;
    private Quaternion _startRotation;
    private Quaternion _targetRotation;
    private  float _openAngle = 90f;
    
    private void Start()
    {
        _startRotation = transform.rotation;
        _targetRotation = Quaternion.Euler(_openAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        Open();
    }

    private void Open()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(OpenDoor());
    }

    private IEnumerator OpenDoor()
    {
        _elapsedTime = 0f;
        
        while (_elapsedTime <= _duration)
        {
            transform.rotation = Quaternion.Lerp(_startRotation, _targetRotation, _elapsedTime / _duration);
            _elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return _waitForSeconds;
    }
}