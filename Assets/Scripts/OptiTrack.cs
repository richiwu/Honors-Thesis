/**
 * Adapted from johny3212
 * Written by Matt Oskamp
 */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class OptiTrack : MonoBehaviour {

	public int rigidbodyIndex;
	public Vector3 rotationOffset;


	public Text log1;
	public Text log2;
	public Text log3;
	public Text log4;
	public Text log5;
	public Text log6;

	public float smoothPos;
	public float smoothRot;


	private Vector3 originalPos;
	private Vector3 currentPos;
	private Quaternion originalRot;
	private Quaternion currentRot;

//	Optitrack Precision Control
	private int precisionLength = 4;
	private int precisionLengthBeforeDot = 3;

	private float expBase = 10.0f;
	private float precision;




	// Use this for initialization
	void Start () {

		precision = Mathf.Pow(expBase, precisionLength);

//		do {
			originalPos = OptiTrackManager.Instance.getPosition (rigidbodyIndex);
			originalRot = OptiTrackManager.Instance.getOrientation (rigidbodyIndex);

			originalRot = originalRot * Quaternion.Euler (rotationOffset);
//		} while(originalPos == Vector3.zero);
	}
	
	// Update is called once per frame
	void Update () {
		currentPos = OptiTrackManager.Instance.getPosition(rigidbodyIndex);
		currentRot = OptiTrackManager.Instance.getOrientation(rigidbodyIndex);

		this.transform.position = (currentPos - originalPos) * smoothPos;

		Vector3 euler = currentRot.eulerAngles - originalRot.eulerAngles;
		this.transform.rotation = Quaternion.Euler(euler);

//		printLog();
	}


	public float returnSmoothPos()
	{
		return smoothPos;
	}
	public float returnPrecision()
	{
		return precision;
	}

	public int returnPrecisionLength()
	{
		return precisionLength;
	}

	public int returnPrecisionLengthBeforeDot()
	{
		return precisionLengthBeforeDot;
	}

	public Vector3 returnOriginalPos()
	{
		return originalPos;
	}

	public Vector3 returnCurrentPos()
	{
		return currentPos;
	}

	public Quaternion returnOriginalRot()
	{
		return originalRot;
	}

	public Quaternion returnCurrentRot()
	{
		return currentRot;
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
//		String log1Text;
//		String log2Text;
//		String log3Text;
//		String log4Text;
//		String log5Text;
//		String log6Text;
//
//		Vector3 originalEuler 	= originalRot.eulerAngles;
//		Vector3 currentEuler 	= currentRot.eulerAngles;
//		Vector3 diffEuler = this.transform.rotation.eulerAngles;
//
//		float Pos_oldX = Mathf.Round(originalPos.x * precision) / precision;
//		float Pos_oldY = Mathf.Round(originalPos.y * precision) / precision;
//		float Pos_oldZ = Mathf.Round(originalPos.z * precision) / precision;
//
//		float Pos_newX = Mathf.Round(currentPos.x * precision) / precision;
//		float Pos_newY = Mathf.Round(currentPos.y * precision) / precision;
//		float Pos_newZ = Mathf.Round(currentPos.z * precision) / precision;
//
//		float Pos_diffX = Mathf.Round(this.transform.position.x * precision) / precision;
//		float Pos_diffY = Mathf.Round(this.transform.position.y * precision) / precision;
//		float Pos_diffZ = Mathf.Round(this.transform.position.z * precision) / precision;
//
//		float Rot_oldX = Mathf.Round(originalEuler.x * precision) / precision;
//		float Rot_oldY = Mathf.Round(originalEuler.y * precision) / precision;
//		float Rot_oldZ = Mathf.Round(originalEuler.z * precision) / precision;
//
//		float Rot_newX = Mathf.Round(currentEuler.x * precision) / precision;
//		float Rot_newY = Mathf.Round(currentEuler.y * precision) / precision;
//		float Rot_newZ = Mathf.Round(currentEuler.z * precision) / precision;
//
//		float Rot_diffX = Mathf.Round(diffEuler.x * precision) / precision;
//		float Rot_diffY = Mathf.Round(diffEuler.y * precision) / precision;
//		float Rot_diffZ = Mathf.Round(diffEuler.z * precision) / precision;
//
//
//
//
//		log1Text =	"Pos_X1: " + String.Format ("{0, 10}", addDigit(Pos_oldX.ToString ())) + "\t\t ";
//		log1Text +=	"Pos_Y1: " + String.Format ("{0, 10}", addDigit(Pos_oldY.ToString ())) + "\t\t ";
//		log1Text +=	"Pos_Z1: " + String.Format ("{0, 10}", addDigit(Pos_oldZ.ToString ())) + "\t\t ";
//
//		log2Text =	"Pos_X2: " + String.Format ("{0, 10}", addDigit(Pos_newX.ToString ())) + "\t\t ";
//		log2Text +=	"Pos_Y2: " + String.Format ("{0, 10}", addDigit(Pos_newY.ToString ())) + "\t\t ";
//		log2Text +=	"Pos_Z2: " + String.Format ("{0, 10}", addDigit(Pos_newZ.ToString ())) + "\t\t ";
//
//		log3Text =	"Pos_X3: " + String.Format ("{0, 10}", addDigit(Pos_diffX.ToString ())) + "\t\t ";
//		log3Text +=	"Pos_Y3: " + String.Format ("{0, 10}", addDigit(Pos_diffY.ToString ())) + "\t\t ";
//		log3Text +=	"Pos_Z3: " + String.Format ("{0, 10}", addDigit(Pos_diffZ.ToString ())) + "\t\t ";
//
//
//		log4Text =	"Rot_X1: " + String.Format ("{0, 10}", addDigit(Rot_oldX.ToString ())) + "\t\t ";
//		log4Text +=	"Rot_Y1: " + String.Format ("{0, 10}", addDigit(Rot_oldY.ToString ())) + "\t\t ";
//		log4Text +=	"Rot_Z1: " + String.Format ("{0, 10}", addDigit(Rot_oldZ.ToString ())) + "\t\t ";
//
//		log5Text =	"Rot_X2: " + String.Format ("{0, 10}", addDigit(Rot_newX.ToString ())) + "\t\t ";
//		log5Text +=	"Rot_Y2: " + String.Format ("{0, 10}", addDigit(Rot_newY.ToString ())) + "\t\t ";
//		log5Text +=	"Rot_Z2: " + String.Format ("{0, 10}", addDigit(Rot_newZ.ToString ())) + "\t\t ";
//
//		log6Text =	"Rot_X3: " + String.Format ("{0, 10}", addDigit(Rot_diffX.ToString ())) + "\t\t ";
//		log6Text +=	"Rot_Y3: " + String.Format ("{0, 10}", addDigit(Rot_diffY.ToString ())) + "\t\t ";
//		log6Text +=	"Rot_Z3: " + String.Format ("{0, 10}", addDigit(Rot_diffZ.ToString ())) + "\t\t ";
//
//		log1.text = log1Text;
//		log2.text = log2Text;
//		log3.text = log3Text;
//		log4.text = log4Text;
//		log5.text = log5Text;
//		log6.text = log6Text;
//	}
}
