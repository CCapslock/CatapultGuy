using UnityEngine;

public class RotatingController : MonoBehaviour
{
	private Transform _sligshotTransform;
	private InputController _inputController;
	private Vector3 _startPosition;
	private Vector3 _rotatingVector;
	private Vector2 _tempRotatingVector;
	public float RightBorder;
	public float LeftBorder;
	private bool _delayCounted;

	private void Start()
	{
		_sligshotTransform = GetComponent<Transform>();
		_inputController = FindObjectOfType<InputController>();
		_tempRotatingVector = new Vector2();
		_rotatingVector = new Vector3();
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

			_tempRotatingVector.x = (_inputController.TouchPosition.x - _startPosition.x);
			_tempRotatingVector.y = (_inputController.TouchPosition.y - _startPosition.y);
			if (_sligshotTransform.rotation.eulerAngles.y + _tempRotatingVector.x < LeftBorder && _sligshotTransform.rotation.eulerAngles.y + _tempRotatingVector.x > RightBorder)
			{
				RotateAtSide(_tempRotatingVector);
			}
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
		_rotatingVector.x = vector.y;
		_sligshotTransform.Rotate(_rotatingVector);
		if (_sligshotTransform.rotation.eulerAngles.z != 0)
		{
			_rotatingVector = Vector3.zero;
			_rotatingVector.z = _sligshotTransform.rotation.eulerAngles.z * -1;
			_sligshotTransform.Rotate(_rotatingVector);
		}
	}
}
