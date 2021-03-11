using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public Rigidbody PlayerHips;
	public Transform DirectionTransformStart;
	public Transform DirectionTransformEnd;
	public CameraFollow _cameraFollow;
	public RotatingController _rotatingController;
	public InputController _inputController;
	public LineRenderer LineVisual;
	public int LineSegments;

	private Vector3 _startPlayerPosition;
	private Collider[] _ragdollColliders;
	private bool _isFlying;
	private void Start()
	{
		_cameraFollow = FindObjectOfType<CameraFollow>();
		_inputController = FindObjectOfType<InputController>();
		_rotatingController = FindObjectOfType<RotatingController>();
		_cameraFollow.enabled = false;
		LineVisual = GetComponent<LineRenderer>();
		LineVisual.positionCount = LineSegments;
		_ragdollColliders = GetComponentsInChildren<Collider>();
	}
	private void FixedUpdate()
	{
		if (_isFlying)
		{
			//todo перемещается в полете
		}
		if (!_isFlying)
		{
			_startPlayerPosition = PlayerHips.position;
		}

		Vector3 speed = (DirectionTransformEnd.position - _startPlayerPosition) * (_rotatingController._slingShotPower / 3500f);
		if (_rotatingController.IsSecondPhase && _inputController.DragingStarted)
		{
			ShowTrajectory(_startPlayerPosition, speed);
		}
	}

	public void ShootGuy(float addForceForce)
	{
		_isFlying = true;
		_cameraFollow.transform.parent = null;
		_cameraFollow.enabled = true;
		_rotatingController.enabled = false;
		PlayerHips.isKinematic = false;
		Vector3 shootingVector = DirectionTransformEnd.position - _startPlayerPosition;
		PlayerHips.AddForce(shootingVector * addForceForce);
	}

	private void ShowTrajectory(Vector3 origin, Vector3 speed)
	{
		Debug.Log("Visualise");
		Vector3[] points = new Vector3[LineSegments];
		LineVisual.positionCount = points.Length;

		for (int i = 0; i < points.Length; i++)
		{
			float time = i * 0.1f;

			points[i] = origin + speed * time + Physics.gravity * time * time / 2;
		}

		LineVisual.SetPositions(points);
	}
}
