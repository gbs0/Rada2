using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	public int level;
	public bool precisa_interagir = true;
	public bool precisa_runas = false;
	public PlayerScript jogador;
	private bool isColliding;

	void Update () {
		if (isColliding) {
			if (!precisa_interagir || Input.GetKeyDown ("e")) {
				if (precisa_runas && !jogador.hasAllRunes()) {
					return;
				}

				GlobalControl.Instance.ultimoLevel = GlobalControl.Instance.levelAtual;
				GlobalControl.Instance.levelAtual = level;
				jogador.salvarDados ();
				SceneManager.LoadScene (level);
			}
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
