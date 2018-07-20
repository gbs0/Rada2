using UnityEngine;
using System.Collections;

public class RisingBridge : MonoBehaviour {
    
	private bool isRisen = false;
	private bool rising = false;
	private Vector3 targetPosition;
	private float timeStartedLerping;
	private AudioSource source;

	public float amountToRise;
	public float timeToComplete = 60f;
	public AudioClip riseClip;

	void Awake() {
		source = GetComponent<AudioSource> ();
	}

	void Start() {
		targetPosition = transform.position + Vector3.up * amountToRise;
	}

	void Update () {
		if (rising) {
			RiseBridge ();
		}
	}

	public void executeRising() {
		if (!isRisen) {
			source.PlayOneShot(riseClip, 1.0f);
			isRisen = true;
			rising = true;
			timeStartedLerping = Time.time;
		}
	}

	void RiseBridge() {
		float timeSinceStarted = Time.time - timeStartedLerping;
		float percentageComplete = timeSinceStarted / timeToComplete;

		transform.position = Vector3.Slerp (transform.position, targetPosition, percentageComplete);

		if (percentageComplete >= 1f) {
			rising = false;
		}

	}
}	