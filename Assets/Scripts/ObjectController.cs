using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour {

	private Rigidbody rb;
	public float rotationSpeed;
	public float speed;

	private Vector3 objectControl;

	// Use this for initialization
	void Start () {
	
		rb = GetComponent<Rigidbody> ();
	
		if (rb == null) {
			Debug.Log ("Can not find rigid body");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Vector3 returnObjectControl()
	{
		return objectControl;
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

//		Debug.Log ("moveHorizontal: " + moveHorizontal + " moveVertical: " + moveVertical);

		objectControl = new Vector3(moveVertical, 0.0f, moveHorizontal);

		//		Vector3 accelerationRaw = Input.acceleration;

		//		Vector3 acceleration = FixAcceleration (accelerationRaw);

		//		Vector2 direction = touchPad.GetDirection ();
		//		Vector3 movement = new Vector3(direction.x, 0.0f, direction.y);
		rb.velocity = objectControl * speed;

//		Debug.Log ("2: " + "x: " + rb.velocity.x + " y: " + rb.velocity.y + " z: " + rb.velocity.z);

//		GetComponent<Rigidbody>().position = new Vector3 
//			(
//				Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
//				0.0f,
//				Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
//			);
//
//		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
//		Debug.Log ("2: " + "x: " + rb.velocity.x + " y: " + rb.velocity.y + " z: " + rb.velocity.z);
	}
}
