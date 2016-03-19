using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	private RotationTriggerController triggerController;
	private bool triggerControlDown;
	private bool triggerControlUp;
	private bool triggerTriggered;

	private RotationTriggerController rotationTriggerController;
	private bool rotationTriggerControl;
	private bool rotationTriggerTriggered;
	private bool rotationDirection = false;

	private RotationTriggerController batteryMoverController;
	private bool batteryMoverControl;
	private bool batteryMoverTriggered;


	private DrillBitController drillBitController;
	private float drillBitControl;
	// This value should be modified together with Gravity and Sensitivity in Input Manager.
	private Vector3 drillBitSpeed;
	public float drillBitSpeedMultiplication = 100.0f;


	private DrillCombinationController drillCombinationController;
	private Rigidbody[] drillParts;
	private int drillPartsNumber;
	private float moveUp;
	private float moveLeft;
	private float moveFront;
	private float rotateX;
	private float rotateY;
	private float rotateZ;

	private Vector3 movePosition;
	private Vector3 moveRotation;
	public float movePositionSpeed = 100.0f;
	public float moveRotationSpeed = 100.0f;




	public float explosionRadius = 15.0f;
	private bool explosionAnimationControl;
	private bool explosion = false;
	private Vector3[] startPositions;
	private Vector3[] endPositions;



	// Use this for initialization
	void Start () {

		GameObject drillCombinationObject = GameObject.FindWithTag ("DrillCombination");
		if (drillCombinationObject != null) {
			drillCombinationController = drillCombinationObject.GetComponent<DrillCombinationController> ();

			drillParts = drillCombinationObject.GetComponentsInChildren<Rigidbody> ();
			drillPartsNumber = drillParts.Length;

			startPositions = new Vector3[drillPartsNumber];
			endPositions = new Vector3[drillPartsNumber];

			float angle = 2 * Mathf.PI / drillPartsNumber;

//			Debug.Log ("drillPartsNumber: " + drillPartsNumber);

			for (int i = 0; i < drillPartsNumber; i++) {
			
				startPositions [i] = drillParts [i].position;
				endPositions [i] = new Vector3 (Mathf.Sin (i * angle) * explosionRadius, 0.0f, Mathf.Cos (i * angle) * explosionRadius);

				string tmp = drillParts [i].name;
				Debug.Log ("name: " + tmp);
			
			}

		}

	
		GameObject triggerObject = GameObject.FindWithTag ("Trigger");
		if (triggerObject != null) {
			triggerController = triggerObject.GetComponent<RotationTriggerController> ();
		}

		GameObject rotationTriggerObject = GameObject.FindWithTag ("RotationTrigger");
		if (rotationTriggerObject != null) {
			rotationTriggerController = rotationTriggerObject.GetComponent<RotationTriggerController> ();
		}

		GameObject batteryMoverObject = GameObject.FindWithTag ("BatteryMover");
		if (batteryMoverObject != null) {
			batteryMoverController = batteryMoverObject.GetComponent<RotationTriggerController> ();
		}

		GameObject drillBitObject = GameObject.FindWithTag ("DrillBit");
		if (drillBitObject != null) {
			drillBitController = drillBitObject.GetComponent<DrillBitController> ();
		}
	}
		


	// Update is called once per frame
	void Update () {


//		moveUp = Input.GetAxis ("MoveUp");
//		moveLeft = Input.GetAxis ("MoveLeft");
//		moveFront = Input.GetAxis ("MoveFront");
//
//		rotateX = Input.GetAxis ("RotateX");
//		rotateY = Input.GetAxis ("RotateY");
//		rotateZ = Input.GetAxis ("RotateZ");
//
//		movePosition = new Vector3(moveLeft, moveUp, moveFront) * movePositionSpeed;
//		moveRotation = new Vector3(rotateX, rotateY, rotateZ) * moveRotationSpeed;
//
//		Debug.Log ("movePosition: " + movePosition);
//		Debug.Log ("moveRotation: " + moveRotation);
//
//		drillCombinationController.setMovePosition (movePosition);
//		drillCombinationController.setMoveRotation (moveRotation);




		triggerControlDown = Input.GetButtonDown ("TriggerControl");
		triggerControlUp = Input.GetButtonUp ("TriggerControl");

		if (triggerControlDown && triggerController.returnRotationTriggerControl() == false) {
			triggerController.rotationTriggerTrigger ();
		} else if (triggerControlUp) {
			triggerController.rotationTriggerTrigger ();
		}



		rotationTriggerControl = Input.GetButtonDown ("RotationTriggerControl");
		if (rotationTriggerControl && rotationTriggerController.returnRotationTriggerControl() == false) {
			rotationTriggerController.rotationTriggerTrigger ();
			if (rotationDirection == false) {
				rotationDirection = true;
			} else
				rotationDirection = false;
		} else {

		}



		batteryMoverControl = Input.GetButtonDown ("BatteryMoverControl");
		if (batteryMoverControl && batteryMoverController.returnRotationTriggerControl() == false) {
			batteryMoverController.rotationTriggerTrigger ();
		} else {

		}



		drillBitControl = Input.GetAxis ("DrillControl");
		if (rotationDirection == false)
			drillBitControl = -drillBitControl;
		drillBitSpeed = new Vector3(0.0f, 0.0f, drillBitControl) * drillBitSpeedMultiplication;
		drillBitController.setDrillBitSpeed (drillBitSpeed);



		explosionAnimationControl = Input.GetButtonDown ("ExplosionAnimationControl");
		if (explosionAnimationControl) {

			Debug.Log ("Trigger explosion Animation.");

			if (!explosion) {
				for (int i = 0; i < drillPartsNumber; i++) {
					drillParts [i].position = endPositions [i];
				}
				explosion = true;
			} 
			else {
				for (int i = 0; i < drillPartsNumber; i++) {
					drillParts [i].position = startPositions [i];
				}
				explosion = false;
			}
		}



	}
}
