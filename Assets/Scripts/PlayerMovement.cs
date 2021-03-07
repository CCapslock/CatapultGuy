using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	/*public float YDelay;

	[SerializeField] private InputController _inputController;
	[SerializeField] private Transform _playerTransform;
	[SerializeField] private Vector3 _delay;
	[SerializeField] private Vector3 _startPosition;
	[SerializeField] private Vector3 _screenWall;
	[SerializeField] private Vector2 _minScreenPosition, _maxScreenPosition;
	[SerializeField] private bool _delayCounted;

	private void Start()
	{
		_playerTransform = GetComponent<Transform>();
		_inputController = FindObjectOfType<InputController>();
		_delay = new Vector3(0, YDelay);
		_minScreenPosition = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)) * 3f;
		_maxScreenPosition = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) * 3f;
		_screenWall = new Vector3(Mathf.Clamp(transform.position.x, _minScreenPosition.x, _maxScreenPosition.x), Mathf.Clamp(transform.position.y, _minScreenPosition.y, _maxScreenPosition.y), transform.position.z);
	}
	//передвигает обьект в зависимости от положения пальца
	private void Update()
	{
		if (_inputController.DragingStarted)
		{
			if (!_delayCounted)
			{
				_delay = _inputController.TouchPosition;
				_startPosition = _playerTransform.position;
				_delayCounted = true;
			}
			_playerTransform.position = _startPosition + _inputController.TouchPosition - _delay;
		}
		else
		{
			_delayCounted = false;
		}
		//ограничение экрана
		_screenWall.x = Mathf.Clamp(transform.position.x, _minScreenPosition.x, _maxScreenPosition.x);
		_screenWall.y = Mathf.Clamp(transform.position.y, _minScreenPosition.y, _maxScreenPosition.y);
		transform.position = _screenWall;
	}*/
	public GameObject Arrow;
	public Rigidbody PlayerHips;
	public Transform DirectionTransformStart;
	public Transform DirectionTransformEnd;
	public CameraFollow _cameraFollow;
	public RotatingController _rotatingController;
	public InputController _inputController;
	public LineRenderer LineVisual;
	public int LineSegments;

	private Vector3 _delay;
	private Vector3 _startPlayerPosition;
	private bool _isFlying;
	private bool _delayCounted;
	private void Start()
	{
		_cameraFollow = FindObjectOfType<CameraFollow>();
		_rotatingController = FindObjectOfType<RotatingController>();
		_cameraFollow.enabled = false;
		LineVisual = GetComponent<LineRenderer>();
		LineVisual.positionCount = LineSegments;
	}
	private void FixedUpdate()
	{
		if (_isFlying)
		{
			//todo перемещается в полете
		}
		//Vector3 vo = CalculateVelocity(DirectionTransformEnd.position, DirectionTransformStart.position, 2f);
		//vo.x /= 10f;
		//Visualise(vo);

		if (!_isFlying)
		{
			_startPlayerPosition = PlayerHips.position;
		}

		Vector3 speed = (DirectionTransformEnd.position - _startPlayerPosition) * (_rotatingController._slingShotPower / 3500f);

		ShowTrajectory(_startPlayerPosition, speed);
	}

	public void ShootGuy(float addForceForce)
	{
		_isFlying = true;
		Arrow.SetActive(false);
		_cameraFollow.transform.parent = null;
		_cameraFollow.enabled = true;
		_rotatingController.enabled = false;
		PlayerHips.isKinematic = false;
		Vector3 shootingVector = DirectionTransformEnd.position - _startPlayerPosition;
		//shootingVector.z *= AddForceForce;
		PlayerHips.AddForce(shootingVector * addForceForce);
	}

	private void ShowTrajectory(Vector3 origin, Vector3 speed)
	{
		Vector3[] points = new Vector3[LineSegments];
		LineVisual.positionCount = points.Length;

		for (int i = 0; i < points.Length; i++)
		{
			float time = i * 0.1f;

			points[i] = origin + speed * time + Physics.gravity * time * time / 2;
		}

		LineVisual.SetPositions(points);
	}

	private void Visualise(Vector3 velocity)
	{
		for (int i = 0; i < LineSegments; i++)
		{
			LineVisual.SetPosition(i, CalculatePisitionInTime(velocity, i * (float)LineSegments));
		}
	}

	private Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
	{
		Vector3 distance = target - origin;
		Vector3 distanceXZ = distance;
		distanceXZ.y = 0f;

		float sY = distance.y;
		float sXZ = distanceXZ.magnitude;


		float vXZ = sXZ * time;
		float vY = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);

		Vector3 result = distanceXZ.normalized;
		result *= vXZ;
		result.y = vY;

		return result;
	}
	private Vector3 CalculatePisitionInTime(Vector3 velocity, float time)
	{
		Vector3 velocityXZ = velocity;
		velocityXZ.y = 0f;

		Vector3 result = DirectionTransformStart.position + velocity * time;
		float positionY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (velocity.y * time) + DirectionTransformStart.position.y;

		result.y = positionY;

		return result;
	}
}
