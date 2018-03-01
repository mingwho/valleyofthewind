using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBehavior : MonoBehaviour {

    Rigidbody rb;
    new AudioSource audio;

    public GameObject[] trees;

    public GameObject spawnParticle;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision) {
        audio.Play();

        GameObject part = Instantiate(spawnParticle, transform.position, Quaternion.identity);
        ParticleSystem.MainModule p = part.GetComponentInChildren<ParticleSystem>().main;
        p.startColor = Color.red;

        GameObject treePrefab = trees[Random.Range(0, trees.Length)];

        GameObject.Instantiate(treePrefab, transform.position, Quaternion.identity);
    }
}
