using UnityEngine;
using System.Collections;

public enum TurretType { Energy, Arrow };

public class TurretScript : MonoBehaviour {
	public Transform energyPrefab;
	public Transform arrowPrefab;
	public Transform turretHead;
	public Transform turretCannonTip;
	public TurretType turretType;
	public bool ativado = true;
	public float fireRate;
	public float lastShotTime;
	public bool turretCanFollowPlayer;
	public LayerMask targetingMask;
	public float targetingDetectionRange;
	public Transform target;

	void Update () {
		if (!ativado) {
			return;
		}

		Collider[] targets = Physics.OverlapSphere(transform.position, targetingDetectionRange, targetingMask);

		if (targets.Length > 0) {

			Transform bestTarget = null;

			foreach (Collider col in targets) {

				if (bestTarget == null)
					bestTarget = col.transform;
				else {
					if (Vector3.Distance(col.transform.position, transform.position) < Vector3.Distance(bestTarget.position, transform.position)) 
						bestTarget = col.transform;
				}
			}

			target = bestTarget;
		} else {
			target = null;
		}

		if (target != null) {
			if (turretCanFollowPlayer == true)
				turretHead.LookAt(target);

			RaycastHit hit;
			if (Physics.Raycast(turretCannonTip.position, turretCannonTip.forward, out hit, 100, targetingMask) == true) {

				if (Time.time - lastShotTime > fireRate || lastShotTime == 0) {

					Transform projectile = null;

					if (turretType == TurretType.Energy)
						projectile = energyPrefab;
					else
						projectile = arrowPrefab;

					Instantiate(projectile, turretCannonTip.position, turretCannonTip.rotation);

					lastShotTime = Time.time;
				}
			}
		}
	}
}