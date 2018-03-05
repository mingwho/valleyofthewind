using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePilot : MonoBehaviour {
	public float flyingSpeed = 40.0f;

	public Transform head;
	public SteamVR_TrackedObject leftHand;
	public SteamVR_TrackedObject rightHand;
	public float initHeight = 0f;

	static PlanePilot _instance;
	public static PlanePilot Instance {
		get { return _instance; }
	}

	// Use this for initialization
	void Start () {
		Debug.Log ("plane pilot script added to:" + gameObject.name);
		_instance = this;

		//get initial height of controllers at chest level - have players hold it there when game starts
		initHeight = (leftHand.transform.localPosition.y + rightHand.transform.localPosition.y)/2;
		Debug.Log ("initial height is " + initHeight);
	}
	
	// Update is called once per frame
	void Update () {
		// every frame we are moving forward
		transform.position += transform.forward * Time.deltaTime * flyingSpeed;


		// get left and right controller's y position
		float leftDir = leftHand.transform.localPosition.y; //- head.position;
		float rightDir = rightHand.transform.localPosition.y; //- head.position;
 
		// determine upward or downward movement based on the controllers relative position to initial height
		float dir = ((leftDir + rightDir)/2.0f) - initHeight;

		// turn left or right based on the relative position of left and right hands
		float turnControl = leftDir - rightDir;


		//if (transform.rotation.x < 75.0f && transform.rotation.x > -75.0f) {
			transform.Rotate (-dir, 0f, 0f, Space.Self);
			transform.Rotate (0f, turnControl, 0.0f, Space.World);

		// left and right controllers for old trigger turn controls - not needed but included for posterity
//		var lDevice = SteamVR_Controller.Input ((int)leftHand.index);
//		var rDevice = SteamVR_Controller.Input ((int)rightHand.index);


		// Getting head rotation and position values - figuring out best way to orient player in glider right now - Grace
		Debug.Log("head rotation: " + head.rotation.x);
		Debug.Log("head position: " + head.position);

		// check how far we are from the terrain and if we collide to the terrain we will stop
//		float terrainHeightWhereWeAre = Terrain.activeTerrain.SampleHeight (transform.position);
//
//		if (terrainHeightWhereWeAre > transform.position.y) {
//			transform.position = new Vector3 (transform.position.x, terrainHeightWhereWeAre, transform.position.z);
//		}

	}
}
