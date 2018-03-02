using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBehavior : MonoBehaviour {

    Rigidbody rb;
    MeshRenderer mr;
    new AudioSource audio;

    public GameObject[] trees;
    public GameObject spawnParticle;
    public Sprite seedSprite;
    public Color color;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        mr = GetComponent<MeshRenderer>();

        mr.material.color = color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision) {
        audio.Play();

        GameObject part = Instantiate(spawnParticle, transform.position, Quaternion.identity);
        ParticleSystem.MainModule p = part.GetComponentInChildren<ParticleSystem>().main;
        p.startColor = color;

        GameObject treePrefab = trees[Random.Range(0, trees.Length)];

        GameObject.Instantiate(treePrefab, transform.position, Quaternion.identity);
    }
}
