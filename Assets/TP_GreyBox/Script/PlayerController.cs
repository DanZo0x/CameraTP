using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 10.0f;

	Rigidbody _rigidbody = null;
	protected bool IsActive { get; private set; }

	public void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
        //Cursor.lockState = CursorLockMode.Locked;

    }

    void FixedUpdate()
    {
		Vector3 direction = Vector3.zero;
		direction += Quaternion.Euler(Camera.main.transform.eulerAngles) * Vector3.right * Input.GetAxisRaw("Horizontal");
		direction += Quaternion.Euler(Camera.main.transform.eulerAngles) * Vector3.forward * Input.GetAxisRaw("Vertical");
		direction = new Vector3(direction.x, 0, direction.z); // dafuck ?
		direction.Normalize();
		float finalspeed = speed * (Input.GetKey(KeyCode.LeftControl)? 2.5f :1f);
		_rigidbody.velocity = direction * finalspeed + Vector3.up * _rigidbody.velocity.y;
	}
}
