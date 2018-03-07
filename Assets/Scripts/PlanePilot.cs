using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePilot : MonoBehaviour
{
	public float flyingSpeed = 15.0f;

	public Transform head;
	public SteamVR_TrackedObject leftHand;
	public SteamVR_TrackedObject rightHand;
	public float initHeight = 0f;
	private float checkInitHeight = 0f;

	static PlanePilot _instance;

	public static PlanePilot Instance {
		get { return _instance; }
	}

	AudioSource windSource;
	[Range (0, 1)]
	public float maxVolume = 1f;

	void Start ()
	{
		Debug.Log ("plane pilot script added to:" + gameObject.name);
		_instance = this;

		windSource = GetComponent<AudioSource> ();
	}



	void Update ()
	{
		// every frame we are moving forward
		transform.position += transform.forward * Time.deltaTime * flyingSpeed;


		// get left and right controller's y position
		float leftDir = leftHand.transform.localPosition.y; //- head.position;
		float rightDir = rightHand.transform.localPosition.y; //- head.position;


		//Initial calibration for controller height
		if (checkInitHeight == 50) {
			initHeight = (leftDir + rightDir) / 2;
			checkInitHeight++;
		} else if (checkInitHeight < 60) {
			checkInitHeight++;
		}

		// determine upward or downward movement based on the controllers relative position to initial height
		float dir = ((leftDir + rightDir) / 2.0f) - initHeight;

		// turn left or right based on the relative position of left and right hands
		float turnControl = leftDir - rightDir;

		//Movement through rotation
		transform.Rotate (-dir, 0f, 0f, Space.Self);
		transform.Rotate (0f, turnControl, 0f, Space.World);


		// Left Controller Button actions
		//To recalibrate controller height
		var lDevice = SteamVR_Controller.Input ((int)leftHand.index);
		if (lDevice.GetPressDown (SteamVR_Controller.ButtonMask.Grip)) {
			initHeight = ((leftDir + rightDir) / 2.0f);
		}

		//To change flying speed
		if (lDevice.GetPress (SteamVR_Controller.ButtonMask.Trigger)) {
			flyingSpeed = 0f;
		} else if (lDevice.GetPress (SteamVR_Controller.ButtonMask.Touchpad)) {
			flyingSpeed = 50f;
		} else {
			flyingSpeed = 15f;
		}


		// check how far we are from the terrain and if we collide to the terrain we will stop
		float terrainHeightWhereWeAre = Terrain.activeTerrain.SampleHeight (transform.position);

		if (terrainHeightWhereWeAre > transform.position.y) {
			transform.position = new Vector3 (transform.position.x, (terrainHeightWhereWeAre), transform.position.z);
		}



//attempt at tilt- partially successful
		float z = turnControl * 5.0f; 
		//Its in radians - this is the problem
		float tiltAngle = transform.Find ("Glider Model").rotation.z;
		float tiltAngleInDegrees = Mathf.Rad2Deg * tiltAngle;
		Debug.Log ("tilt angle: " + tiltAngleInDegrees);
		if ((tiltAngleInDegrees >= 30f) || (tiltAngleInDegrees <= -30f)) {
			transform.Find ("Cube (3)").Rotate (0f, 0f, 0f, Space.Self);
		} else {
			transform.Find ("Cube (3)").Rotate (0f, 0f, z, Space.Self);
		}

		AdjustWindVolume ();

	}

	void AdjustWindVolume ()
	{
		float factor = flyingSpeed / 50f;
		float targetVolume = Mathf.Lerp (0, maxVolume, factor);
		windSource.volume = Mathf.Lerp (windSource.volume, targetVolume, 0.1f);
	}
}
