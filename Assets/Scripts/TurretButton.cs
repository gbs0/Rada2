using UnityEngine;
using System.Collections;

public class TurretButton : MonoBehaviour {

	public TurretScript turret;

	void OnTriggerEnter(Collider collider) {
		if (!turret.ativado && collider.transform.tag == "Player") {
			turret.ativado = true;
		}
	}

}
