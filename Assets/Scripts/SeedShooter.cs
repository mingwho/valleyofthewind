using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SeedShooter : MonoBehaviour {

    public GameObject seed;

    VRTK_ControllerEvents controller;
    new AudioSource audio;

	// Use this for initialization
	void Start () {
        controller = GetComponent<VRTK_ControllerEvents>();
        controller.TouchpadReleased += new ControllerInteractionEventHandler(OnTouchpadReleased);

        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   void OnTouchpadReleased(object sender, ControllerInteractionEventArgs e) {
        Debug.Log("yay");

        GameObject s = Instantiate(seed, transform.position, Quaternion.identity);
        Rigidbody rb = s.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 10f, ForceMode.Impulse);

        audio.Play();
    }
}
