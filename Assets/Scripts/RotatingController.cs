using UnityEngine;
using UnityEngine.UIElements;

public class RotatingController : MonoBehaviour
{
    private Transform _sligshotTransform;
    private InputController _inputController;

    private Vector3 _startPosition;
    private Vector3 _rotatingVector;
    private bool _delayCounted;

    private void Start()
    {
        _sligshotTransform = GetComponent<Transform>();
        _inputController = FindObjectOfType<InputController>();
    }
    private void FixedUpdate()
    {
        if (_inputController.DragingStarted)
        {
            if (!_delayCounted)
            {
                _startPosition = _inputController.TouchPosition;
                _delayCounted = true;
            }

            Vector2 tempVector = new Vector2(_inputController.TouchPosition.x - _startPosition.x, _inputController.TouchPosition.y - _startPosition.y);
            RotateAtSide(tempVector);
        }
        else
        {
            _delayCounted = false;
        }
    }
    private void RotateAtSide(Vector2 vector)
    {
        _rotatingVector = Vector3.zero;
        _rotatingVector.y = vector.x;
        _sligshotTransform.Rotate(_rotatingVector);
        _rotatingVector = Vector3.zero;
        _rotatingVector.x =vector.y;
        _sligshotTransform.Rotate(_rotatingVector);
        if(_sligshotTransform.rotation.eulerAngles.z != 0)
        {
            _rotatingVector = Vector3.zero;
            _rotatingVector.z = _sligshotTransform.rotation.eulerAngles.z * -1;
            _sligshotTransform.Rotate(_rotatingVector);
        }
    }
}
