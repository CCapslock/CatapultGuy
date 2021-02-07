using UnityEngine;

public class Obstacle : MonoBehaviour
{
	private Rigidbody _rb;
	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
		tag = "Obstacle";
	}
	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Debug.Log("Collided with " + other.gameObject.name);
			this.gameObject.tag = "Player";
			_rb.isKinematic = false;
		}
	}
}
