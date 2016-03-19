using UnityEngine;
using System.Collections;

public class RotationTriggerController : MonoBehaviour {

	public float rotationTriggerSpeed = 10.0f;

	//	private Vector3 drillControl;
	private bool rotationTriggerControl = false;

	private Rigidbody rb;
	private float startTime;

	private Vector3 startPos;
	private Vector3 endPos;
	public Vector3 posDiff = new Vector3(3.0f, 3.0f, 3.0f);
	private float journeyLength;

	private bool stage = false;

	//	Vector3 triggerSpeed = Vector3.zero;

	private bool rotationTriggerTriggered;

	// Use this for initialization
	void Start () {

//		this.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);

		rb = GetComponent<Rigidbody> ();

		if (rb == null) {
			Debug.Log ("Can not find the rigid body! (RotationTriggerController)");
		}
	}

	public void rotationTriggerTrigger()
	{
		initialize ();
		rotationTriggerControl = true;
	}

	public bool returnRotationTriggerControl()
	{
		return rotationTriggerControl;
	}

	void initialize()
	{
		startTime = Time.time;

		if (!stage) {
			startPos = rb.position;
			endPos = startPos + posDiff;
		} else {
			startPos = rb.position - posDiff;
			endPos = rb.position;
		}
		journeyLength = Vector3.Distance (startPos, endPos);
	}


	// Update is called once per frame
	void Update () {

		float distCovered;
		float fracJourney;

		if (rotationTriggerControl == false)
			return;

		if (!stage) {
			distCovered = (Time.time - startTime) * rotationTriggerSpeed;
			fracJourney = distCovered / journeyLength;

			rb.position = Vector3.Lerp (startPos, endPos, fracJourney);

			if (rb.position == endPos) {
				startTime = Time.time;
				stage = true;

				rotationTriggerControl = false;
			}
		} else {
			distCovered = (Time.time - startTime) * rotationTriggerSpeed;
			fracJourney = distCovered / journeyLength;

			rb.position = Vector3.Lerp (endPos, startPos, fracJourney);

			if (rb.position == startPos) {
				startTime = Time.time;
				stage = false;
				rotationTriggerControl = false;
			}
		}
	}

	void FixedUpdate ()
	{

	}
}
