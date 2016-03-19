using UnityEngine;
using System.Collections;

public class DrillCombinationController : MonoBehaviour {

	public float movePositionSpeed = 1.0f;
	public float moveRotationSpeed = 1.0f;

	private float moveUp;
	private float moveLeft;
	private float moveFront;
	private float rotateX;
	private float rotateY;
	private float rotateZ;

	private Vector3 movePosition;
	private Vector3 moveRotation;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
	
		rb = GetComponent<Rigidbody> ();

		if (rb == null) {
			Debug.Log ("Can not find rigid body (DrillCombinationController)!");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setMovePosition (Vector3 movePosition)
	{
		rb.velocity = movePosition;
	}

	public void setMoveRotation (Vector3 moveRotation)
	{
		rb.angularVelocity = moveRotation;
	}

	void FixedUpdate()
	{
		moveUp = Input.GetAxis ("MoveUp");
		moveLeft = Input.GetAxis ("MoveLeft");
		moveFront = Input.GetAxis ("MoveFront");

		rotateX = Input.GetAxis ("RotateX");
		rotateY = Input.GetAxis ("RotateY");
		rotateZ = Input.GetAxis ("RotateZ");

		movePosition = new Vector3(moveLeft, moveUp, moveFront) * movePositionSpeed;
		moveRotation = new Vector3(rotateX, rotateY, rotateZ) * moveRotationSpeed;

		Debug.Log ("movePosition: " + movePosition);
		Debug.Log ("moveRotation: " + moveRotation);

		setMovePosition (movePosition);
		setMoveRotation (moveRotation);
	}
}
