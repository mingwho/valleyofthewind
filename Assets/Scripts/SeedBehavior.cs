using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBehavior : MonoBehaviour {

    Rigidbody rb;
    MeshRenderer mr;

    public GameObject[] trees;
    public GameObject spawnParticle;
    public AudioClip spawnSound;
    public Sprite seedSprite;
    public Color color;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();

        mr.material.color = color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision) {
        GameObject part = Instantiate(spawnParticle, transform.position, Quaternion.identity);
        ParticleSystem.MainModule p = part.GetComponentInChildren<ParticleSystem>().main;
        p.startColor = color;
        AudioSource audio = part.GetComponent<AudioSource>();
        if (audio) {
            audio.clip = spawnSound;
            audio.Play();
        }

        GameObject treePrefab = trees[Random.Range(0, trees.Length)];

        GameObject.Instantiate(treePrefab, transform.position + Vector3.down * 0.2f, Quaternion.identity);

        Destroy(gameObject);
    }
}
