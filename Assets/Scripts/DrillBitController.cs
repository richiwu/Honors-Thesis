using UnityEngine;
using System.Collections;

public class DrillBitController : MonoBehaviour {

	private Rigidbody rb;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody> ();

		if (rb == null) {
			Debug.Log ("Can not find the rigid body! (DrillBitController)");
		}
	}

	// Update is called once per frame
	void Update () {

	}


	public void setDrillBitSpeed(Vector3 drillBitSpeed)
	{
		rb.angularVelocity = drillBitSpeed;
	}

	void FixedUpdate ()
	{

	}
}
