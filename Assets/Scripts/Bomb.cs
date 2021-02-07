using UnityEditor.SceneManagement;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	public float BombForce;
	public float BombRadius;

	private SphereCollider _sphereCollider;
	private void Start()
	{
		_sphereCollider = gameObject.AddComponent<SphereCollider>();
		_sphereCollider.enabled = false;
		_sphereCollider.radius = BombRadius;
		_sphereCollider.isTrigger = true;
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			ActivateBomb();
		}
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			ActivateBomb();
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Obstacle") || other.CompareTag("Player"))
		{
			other.attachedRigidbody.isKinematic = false;
			other.attachedRigidbody.AddForce((other.transform.position - transform.position) * BombForce);
			Debug.Log("boom");
		}
	}
	private void ActivateBomb()
	{
		_sphereCollider.enabled = true;
	}
}
