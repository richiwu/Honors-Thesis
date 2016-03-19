using UnityEngine;
using System.Collections;

public class TriggerController : MonoBehaviour {

	public float triggerSpeed = 10.0f;

//	private Vector3 drillControl;
	private bool triggerControl = false;

	private Rigidbody rb;
	private float startTime;

	private Vector3 startPos;
	private Vector3 endPos;
	public Vector3 posDiff = new Vector3(3.0f, 3.0f, 3.0f);
	private float journeyLength;

	private bool stage;

//	Vector3 triggerSpeed = Vector3.zero;

	private bool triggerTriggered;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody> ();

		if (rb == null) {
			Debug.Log ("Can not find the rigid body! (TriggerController)");
		}
	}

	public void triggerTrigger()
	{
		initialize ();
		triggerControl = true;
	}

	public bool returnTriggerControl()
	{
		return triggerControl;
	}

	void initialize()
	{
		startTime = Time.time;

		startPos = rb.position;
		endPos = startPos + posDiff;

		journeyLength = Vector3.Distance (startPos, endPos);

		stage = false;
	}


	// Update is called once per frame
	void Update () {

		float distCovered;
		float fracJourney;

		if (triggerControl == false)
			return;

		if (!stage) {
			distCovered = (Time.time - startTime) * triggerSpeed;
			fracJourney = distCovered / journeyLength;

			rb.position = Vector3.Lerp (startPos, endPos, fracJourney);

			if (rb.position == endPos) {
				startTime = Time.time;
				stage = true;
			}
		} else {
			distCovered = (Time.time - startTime) * triggerSpeed;
			fracJourney = distCovered / journeyLength;

			rb.position = Vector3.Lerp (endPos, startPos, fracJourney);

			if (rb.position == startPos) {
				startTime = Time.time;
				stage = false;
				triggerControl = false;
			}
		}
			
	}

	void FixedUpdate ()
	{
		
	}
}
