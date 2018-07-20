using UnityEngine;
using System.Collections;

public class Rune : MonoBehaviour {
	private bool isColliding = false;

    public PlayerScript player;
	public string nome;

	void Update () {
		if (isColliding && Input.GetKeyDown("e")) {
			if (nome == "Algiz") {
				player.pegarRuneAlgiz ();
			} else if (nome == "Perthro") {
				player.pegarRunePerthro ();
			} else {
				Debug.Log ("NOME NAO RECONHECIDO PARA UMA RUNA");
			}
            gameObject.GetComponent<Renderer>().enabled = false;
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
