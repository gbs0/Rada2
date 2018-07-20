using UnityEngine;
using System.Collections;

public class RotationSwitch : MonoBehaviour {
    
	public RotatingBridge bridge;
	private bool isColliding = false;
	private AudioSource source;


	void Update () {
		if (isColliding && Input.GetKeyDown("e")) {
			bridge.executeRotation(90f);


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