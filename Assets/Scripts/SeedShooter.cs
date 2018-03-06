﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SeedShooter : MonoBehaviour {

    public GameObject seed;

    public VRTK_ControllerEvents controller;
    new AudioSource audio;

    public VRTK_ControllerEvents Controller {
        get { return controller; }
    }

    // Use this for initialization
    void Start() {
        controller = GetComponent<VRTK_ControllerEvents>();
        controller.TriggerClicked += new ControllerInteractionEventHandler(OnTriggerClicked);

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerClicked(object sender, ControllerInteractionEventArgs e) {
        Debug.Log("yay");

        GameObject s = Instantiate(seed, transform.position, Quaternion.identity);
        Rigidbody rb = s.GetComponent<Rigidbody>();
        PlanePilot pilot = PlanePilot.Instance;

        if (pilot) {
            Vector3 heading = PlanePilot.Instance.transform.forward;
            float speed = PlanePilot.Instance.flyingSpeed;

            rb.AddForce(heading * speed, ForceMode.VelocityChange);

        }
        Vector3 rForce = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)) * 1f;
        rb.AddTorque(rForce, ForceMode.Impulse);

        rb.AddForce(transform.forward * 10f, ForceMode.Impulse);

        audio.Play();
    }
}
