using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePilot : MonoBehaviour {
	public float flyingSpeed = 40.0f;

	public Transform head;
	public SteamVR_TrackedObject leftHand;
	public SteamVR_TrackedObject rightHand;

	// Use this for initialization
	void Start () {
		Debug.Log ("plane pilot script added to:" + gameObject.name);
	}
	
	// Update is called once per frame
	void Update () {

		// fly upward or downward based on the controllers relative position to head
		Vector3 leftDir = leftHand.transform.position - head.position;
		Vector3 rightDir = rightHand.transform.position - head.position;
		Vector3 dir = (leftDir + rightDir);
		print (dir.y);
		transform.Rotate (-dir.y, 0f, 0f, Space.Self);

		// every frame we are moving forward
		transform.position += transform.forward * Time.deltaTime * flyingSpeed;

		// left and right controllers
		var lDevice = SteamVR_Controller.Input ((int)leftHand.index);
		var rDevice = SteamVR_Controller.Input ((int)rightHand.index);

		// if player triggers the left trigger, we will turn left
		if (lDevice.GetTouch (SteamVR_Controller.ButtonMask.Trigger)){
			transform.Rotate (0f, 0.4f, 0.0f, Space.World);
		}

		// if player triggers the right trigger, we will turn right
		if (rDevice.GetTouch (SteamVR_Controller.ButtonMask.Trigger)){
			transform.Rotate (0f, -0.4f, 0.0f, Space.World);
		}

		// the parameter value is between -1 and 1
		//transform.Rotate (-Input.GetAxis ("Vertical"), Input.GetAxis ("Horizontal"), 0.0f);
		//Debug.Log ("the x value of axis is ");
		//Debug.Log (-Input.GetAxis ("Vertical"));
		//Debug.Log ("the y value of axis is " + -Input.GetAxis ("Horizontal"));

		// check how far we are from the terrain and if we collide to the terrain we will stop
		float terrainHeightWhereWeAre = Terrain.activeTerrain.SampleHeight (transform.position);

		if (terrainHeightWhereWeAre > transform.position.y) {
			transform.position = new Vector3 (transform.position.x, terrainHeightWhereWeAre, transform.position.z);
		}

	}
}
