using UnityEngine;

public class Obstacle : MonoBehaviour
{
	private Rigidbody _rb;
	private void Awake()
	{
		_rb = GetComponent<Rigidbody>();
		tag = "Obstacle";
		this.gameObject.isStatic = false;
	}
	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			//Debug.Log("Collided with " + other.gameObject.name);

			this.gameObject.tag = "Player";
			this.gameObject.isStatic = false;
			_rb.isKinematic = false;
		}
	}
}
