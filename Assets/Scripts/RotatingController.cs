using UnityEngine;

public class RotatingController : MonoBehaviour
{
	public Transform PlayerTransfom;
	public float RightBorder;
	public float LeftBorder;
	public float _slingShotPower;

	private Transform _sligshotTransform;
	private InputController _inputController;
	private PlayerMovement _playerMovement;
	private Vector3 _startPosition;
	private Vector3 _rotatingVector;
	private Vector3 _playerPositionVector;
	private Vector2 _tempRotatingVector;
	private bool _delayCounted;
	private bool _rotationCounted;
	private bool _slingShotPowerCounted;
	public bool IsSecondPhase;

	private void Start()
	{
		_sligshotTransform = GetComponent<Transform>();
		_inputController = FindObjectOfType<InputController>();
		_playerMovement = FindObjectOfType<PlayerMovement>();
		_playerPositionVector = new Vector3();
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
			if (!IsSecondPhase)
			{
				if (_sligshotTransform.rotation.eulerAngles.y + _tempRotatingVector.x < LeftBorder && _sligshotTransform.rotation.eulerAngles.y + _tempRotatingVector.x > RightBorder)
				{
					RotateAtSide(_tempRotatingVector);
					_rotationCounted = true;
				}
			}
			else
			{
				ChargeTheSlingShot();
			}
		}
		else
		{
			_delayCounted = false;
			if (_rotationCounted && !IsSecondPhase)
			{
				IsSecondPhase = true;
			}
			else if (_slingShotPowerCounted && IsSecondPhase)
			{
				_playerMovement.ShootGuy(_slingShotPower);
			}
		}
	}
	private void ChargeTheSlingShot()
	{
		if (_inputController.TouchPosition.y < _startPosition.y)
		{
			_slingShotPower = (_inputController.TouchPosition.y - _startPosition.y) * -2f * 1400f;
		}
		else
		{
			_startPosition.y = _inputController.TouchPosition.y;
		}

		_playerPositionVector = PlayerTransfom.localPosition;
		_playerPositionVector.z = 2f + (_slingShotPower * (4f / 10000f));
		PlayerTransfom.localPosition = _playerPositionVector;

		Debug.Log("_slingShotPower = " + _slingShotPower);
		_slingShotPowerCounted = true;
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
