using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {
	private bool isColliding = false;
	public PlayerScript player;
	public AudioClip respawnClip;


	public GameObject fire1;
	public GameObject fireLight1;
	public GameObject fire2;
	public GameObject fireLight2;

	private AudioSource source;

	void Awake() {
		source = GetComponent<AudioSource> ();
	}

	void Update () {
		if (isColliding && Input.GetKeyDown("e")) {

			fire1.SetActive (false);
			fireLight1.SetActive (false);

			fire2.SetActive (true);
			fireLight2.SetActive (true);

			source.PlayOneShot(respawnClip, 1.0f);
            player.salvarRespawn (transform.position);
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.transform.tag == "Player") {
			isColliding = true;
		}
	}

	void OnTriggerExit(Collider collider) {
		if (collider.transform.tag == "Player") {
			isColliding = false;
		}
	}

}