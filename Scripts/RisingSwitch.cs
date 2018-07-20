using UnityEngine;
using System.Collections;

public class RisingSwitch : MonoBehaviour {
    
	public RisingBridge bridge;

	private bool isColliding = false;

	void Update () {
		if (isColliding && Input.GetKeyDown("e")) {
			bridge.executeRising();
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