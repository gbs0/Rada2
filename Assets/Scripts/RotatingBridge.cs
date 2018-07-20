using UnityEngine;
using System.Collections;

public class RotatingBridge : MonoBehaviour {
    
	private bool moving;
	private bool executing;
	private float newRotationYAngle;
	private float timeStartedLerping;
	private AudioSource source;

	public float timeToComplete = 60f;
	public AudioClip rotateClip;

	void Awake() {
		source = GetComponent<AudioSource> ();
	}

	void Start() {
        newRotationYAngle = transform.localRotation.eulerAngles.y;
	}

	void Update () {
		if (moving) {
			RotateBridge ();
		}
	}

	public void executeRotation(float incrementalDegree) {
		if (!executing) {
    
			executing = true;
			moving = true;
			timeStartedLerping = Time.time;

			if (newRotationYAngle > 360) {
				newRotationYAngle += incrementalDegree - 360;
			} else {
				newRotationYAngle += incrementalDegree;
			}
		}
	}

	void RotateBridge() {
		float timeSinceStarted = Time.time - timeStartedLerping;
		float percentageComplete = timeSinceStarted / timeToComplete;

		Quaternion quaternion = Quaternion.AngleAxis (newRotationYAngle, Vector3.up);
        transform.rotation = Quaternion.Slerp (transform.rotation, quaternion, percentageComplete);

		float diff = Mathf.Abs (transform.rotation.eulerAngles.y - newRotationYAngle);

		if (executing && diff <= 5) {			
			executing = false;
		}
		if (percentageComplete >= 1f) {
			moving = false;
		}
	}
}	