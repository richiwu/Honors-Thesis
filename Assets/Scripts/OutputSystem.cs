using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class OutputSystem : MonoBehaviour {

	// Use this for initialization


	private OptiTrack optiTrack;
	private ObjectController objectController;

	// From "OptiTrack.cs"
	private int precisionLength;
	private int precisionLengthBeforeDot;
	private float precision;
	private Vector3 originalPos;
	private Vector3 currentPos;
	private Quaternion originalRot;
	private Quaternion currentRot;

	// From "ObjectController.cs"
	public float smoothPos;
	private Vector3 controlMovement;

	public Text log1;
	public Text log2;
	public Text log3;
	public Text log4;
	public Text log5;
	public Text log6;
	public Text controlParameters;

	void Start () {

		// First, using "tag" to find "GameObject"
		// Then, using "component" to find the script
		GameObject optiTrackObject = GameObject.FindWithTag ("OptiTrack");
		if (optiTrackObject != null) {
			optiTrack = optiTrackObject.GetComponent<OptiTrack> ();
		}

		if (optiTrack == null) {
			Debug.Log ("Cannot find 'OptiTrack' script");
		}

		GameObject targetDeviceObject = GameObject.FindWithTag ("TargetDevice");
		if (targetDeviceObject != null) {
			objectController = targetDeviceObject.GetComponent<ObjectController> ();
		}

		if (objectController == null) {
			Debug.Log ("Cannot find 'ObjectController' script");
		}

	}

	void initialValues()
	{
		// From "OptiTrack.cs"
		precisionLength = optiTrack.returnPrecisionLength();
		precisionLengthBeforeDot = optiTrack.returnPrecisionLengthBeforeDot();
		precision = optiTrack.returnPrecision ();

		originalPos = optiTrack.returnOriginalPos ();
		currentPos = optiTrack.returnCurrentPos ();

		originalRot = optiTrack.returnOriginalRot ();
		currentRot = optiTrack.returnCurrentRot ();


		// From "ObjectController.cs"
		smoothPos = optiTrack.returnSmoothPos ();

		controlMovement = objectController.returnObjectControl ();

	}
	
	// Update is called once per frame
	void Update () {
		
		initialValues ();
		
		printLog();
	
	}

	String addDigit(String s)
	{
		int dotPos = s.IndexOf (".");
		int stringLength = s.Length;

		int gap = precisionLength + 1 - (stringLength - dotPos);

		for (int i = 0; i < gap; i++) {
			s += "0";
		}

		for (int i = 0; i < precisionLengthBeforeDot + - dotPos; i++) {
			if (s [0] == '-') {
				s = s.Insert (1, "0");
			} else
				s = s.Insert (0, "0");
		}

		return s;
	}
		
//	void printLog()
//	{
//		String controlParametersText;
//
//
//		float rotateX = Mathf.Round(controlMovement.x * precision) / precision;
//		float rotateY = Mathf.Round(controlMovement.y * precision) / precision;
//		float rotateZ = Mathf.Round(controlMovement.z * precision) / precision;
//
//		Debug.Log ("2: " + rotateX.ToString() + " " + rotateY.ToString() + " " + rotateZ.ToString());
//
//
//		controlParametersText =		"rotateX: " + String.Format ("{0, 10}", addDigit(rotateX.ToString ())) + "\t\t ";
//		controlParametersText +=	"rotateY: " + String.Format ("{0, 10}", addDigit(rotateY.ToString ())) + "\t\t ";
//		controlParametersText +=	"rotateZ: " + String.Format ("{0, 10}", addDigit(rotateZ.ToString ())) + "\t\t ";
//
//		controlParameters.text = controlParametersText;
//	}

	void printLog()
	{
		String controlParametersText;
		
		
		float rotateX = Mathf.Round(controlMovement.x * precision) / precision;
		float rotateY = Mathf.Round(controlMovement.y * precision) / precision;
		float rotateZ = Mathf.Round(controlMovement.z * precision) / precision;
		
//		Debug.Log ("2: " + rotateX.ToString() + " " + rotateY.ToString() + " " + rotateZ.ToString());
		
		controlParametersText =		"rotateX: " + String.Format ("{0, 10}", addDigit(rotateX.ToString ())) + "\t\t ";
		controlParametersText +=	"rotateY: " + String.Format ("{0, 10}", addDigit(rotateY.ToString ())) + "\t\t ";
		controlParametersText +=	"rotateZ: " + String.Format ("{0, 10}", addDigit(rotateZ.ToString ())) + "\t\t ";
		
		controlParameters.text = controlParametersText;



		String log1Text;
		String log2Text;
		String log3Text;
		String log4Text;
		String log5Text;
		String log6Text;

		Vector3 originalEuler 	= originalRot.eulerAngles;
		Vector3 currentEuler 	= currentRot.eulerAngles;
		Vector3 diffEuler = this.transform.rotation.eulerAngles;

		Vector3 PosDiff = (currentPos - originalPos) * smoothPos;

		float Pos_oldX = Mathf.Round(originalPos.x * precision) / precision;
		float Pos_oldY = Mathf.Round(originalPos.y * precision) / precision;
		float Pos_oldZ = Mathf.Round(originalPos.z * precision) / precision;

		float Pos_newX = Mathf.Round(currentPos.x * precision) / precision;
		float Pos_newY = Mathf.Round(currentPos.y * precision) / precision;
		float Pos_newZ = Mathf.Round(currentPos.z * precision) / precision;

		float Pos_diffX = Mathf.Round(PosDiff.x * precision) / precision;
		float Pos_diffY = Mathf.Round(PosDiff.y * precision) / precision;
		float Pos_diffZ = Mathf.Round(PosDiff.z * precision) / precision;

		float Rot_oldX = Mathf.Round(originalEuler.x * precision) / precision;
		float Rot_oldY = Mathf.Round(originalEuler.y * precision) / precision;
		float Rot_oldZ = Mathf.Round(originalEuler.z * precision) / precision;

		float Rot_newX = Mathf.Round(currentEuler.x * precision) / precision;
		float Rot_newY = Mathf.Round(currentEuler.y * precision) / precision;
		float Rot_newZ = Mathf.Round(currentEuler.z * precision) / precision;

		float Rot_diffX = Mathf.Round(diffEuler.x * precision) / precision;
		float Rot_diffY = Mathf.Round(diffEuler.y * precision) / precision;
		float Rot_diffZ = Mathf.Round(diffEuler.z * precision) / precision;




		log1Text =	"Pos_X1: " + String.Format ("{0, 10}", addDigit(Pos_oldX.ToString ())) + "\t\t ";
		log1Text +=	"Pos_Y1: " + String.Format ("{0, 10}", addDigit(Pos_oldY.ToString ())) + "\t\t ";
		log1Text +=	"Pos_Z1: " + String.Format ("{0, 10}", addDigit(Pos_oldZ.ToString ())) + "\t\t ";

		log2Text =	"Pos_X2: " + String.Format ("{0, 10}", addDigit(Pos_newX.ToString ())) + "\t\t ";
		log2Text +=	"Pos_Y2: " + String.Format ("{0, 10}", addDigit(Pos_newY.ToString ())) + "\t\t ";
		log2Text +=	"Pos_Z2: " + String.Format ("{0, 10}", addDigit(Pos_newZ.ToString ())) + "\t\t ";

		log3Text =	"Pos_X3: " + String.Format ("{0, 10}", addDigit(Pos_diffX.ToString ())) + "\t\t ";
		log3Text +=	"Pos_Y3: " + String.Format ("{0, 10}", addDigit(Pos_diffY.ToString ())) + "\t\t ";
		log3Text +=	"Pos_Z3: " + String.Format ("{0, 10}", addDigit(Pos_diffZ.ToString ())) + "\t\t ";


		log4Text =	"Rot_X1: " + String.Format ("{0, 10}", addDigit(Rot_oldX.ToString ())) + "\t\t ";
		log4Text +=	"Rot_Y1: " + String.Format ("{0, 10}", addDigit(Rot_oldY.ToString ())) + "\t\t ";
		log4Text +=	"Rot_Z1: " + String.Format ("{0, 10}", addDigit(Rot_oldZ.ToString ())) + "\t\t ";

		log5Text =	"Rot_X2: " + String.Format ("{0, 10}", addDigit(Rot_newX.ToString ())) + "\t\t ";
		log5Text +=	"Rot_Y2: " + String.Format ("{0, 10}", addDigit(Rot_newY.ToString ())) + "\t\t ";
		log5Text +=	"Rot_Z2: " + String.Format ("{0, 10}", addDigit(Rot_newZ.ToString ())) + "\t\t ";

		log6Text =	"Rot_X3: " + String.Format ("{0, 10}", addDigit(Rot_diffX.ToString ())) + "\t\t ";
		log6Text +=	"Rot_Y3: " + String.Format ("{0, 10}", addDigit(Rot_diffY.ToString ())) + "\t\t ";
		log6Text +=	"Rot_Z3: " + String.Format ("{0, 10}", addDigit(Rot_diffZ.ToString ())) + "\t\t ";




		log1.text = log1Text;
		log2.text = log2Text;
		log3.text = log3Text;
		log4.text = log4Text;
		log5.text = log5Text;
		log6.text = log6Text;
	}
}
