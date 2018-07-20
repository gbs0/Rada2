using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
	CharacterController charController;

	public Transform compass;
	public Transform model;
	public Transform shieldModel;
	public Transform rotDummy;
	public LayerMask PickableItemsLayer;
	public float pickRange;
	public float speed;
	public float speed_Defending;
	public float turnSpeed;
	public float turnSpeed_Defending;
	public bool isDefending;
	public AudioClip morteClip;
	public AudioClip escudoClip;
	public AudioClip passos_neve_clip;
	public AudioClip passo_madeira;
	public AudioClip passo_pedra;

	private AudioSource source;
	private PlayerData playerData;
	private GameObject scripts;

	Vector3 mousePos;
	float h, v;
	Vector3 lastDirection;

	void Awake() {
		source = GetComponent<AudioSource> ();
	}

	void Start () {
		scripts = GameObject.Find ("Scripts");
		playerData = GlobalControl.Instance.getPlayerData ();
		if (GlobalControl.Instance.levelAtual == 1) {
			if (GlobalControl.Instance.ultimoLevel == 2) {
				transform.position = new Vector3 (12,1.5f,-8);
			} else if (GlobalControl.Instance.ultimoLevel == 3) {
				transform.position = new Vector3 (-14,1.5f,-9);
			}
		}

		charController = GetComponent<CharacterController> ();
		compass.localEulerAngles = new Vector3 (0, Camera.main.transform.localEulerAngles.y, 0);
		SetIsDefending(false);

		if (playerData.RunaAlgiz) {
			scripts.SendMessage ("AtivaAlgiz");
		}

		if (playerData.RunaPerthro) {
			scripts.SendMessage ("AtivaPerthro");
		}
    }
		
	void Update()
	{
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(h, 0, v);

		float currentSpeed = 0;

		if (isDefending == true)
			currentSpeed = speed_Defending;
		else
			currentSpeed = speed;		

		movement = compass.TransformDirection(movement);

		movement *= currentSpeed;

		movement -= new Vector3(0, 20, 0);

		movement *= Time.deltaTime;

		charController.Move(movement);

		RaycastHit hit_chao;
		if (Physics.Raycast (transform.position, Vector3.down, out hit_chao, 10)) {
			if (hit_chao.transform.tag == "Neve") {
				source.PlayOneShot (passos_neve_clip, 1.0f);
				Debug.Log ("Neve");
			}

			if (hit_chao.transform.tag == "Ponte_Madeira_CENA00") {
				source.PlayOneShot (passo_madeira, 1.0f);
				Debug.Log ("Madeira");
			}

			if (hit_chao.transform.tag == "Pedra") {
				source.PlayOneShot (passo_pedra, 1.0f);
				Debug.Log ("Pedra");
			}
			Debug.Log (hit_chao.transform.name);
		}

		if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
		{
			lastDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

			lastDirection = compass.TransformDirection(lastDirection).normalized;
		}

		Debug.DrawLine(transform.position, (transform.position + lastDirection) * 5, Color.blue);

		RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit) == true)
			mousePos = hit.point;

		Debug.DrawLine(transform.position, mousePos, Color.cyan);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);


		if (isDefending == true)
		{
			rotDummy.LookAt(mousePos);

			rotDummy.localEulerAngles = new Vector3(0, rotDummy.localEulerAngles.y, 0);

			model.rotation = Quaternion.Lerp(model.rotation, rotDummy.rotation, turnSpeed_Defending);
		}
		else
		{
			Quaternion newRot = Quaternion.LookRotation(lastDirection);

			model.rotation = Quaternion.Lerp(model.rotation, newRot, turnSpeed);
		}
			
		Collider[] detectedItems = Physics.OverlapSphere(transform.position, pickRange, PickableItemsLayer);

		if (detectedItems.Length > 0)
		{
			foreach (Collider col in detectedItems)
			{
				if (col.transform.tag == "Shield")
				{
					source.PlayOneShot(escudoClip, 1.0f);
					playerData.gotShield = true;
					Destroy(col.gameObject);
				}
			}
		}

		if (Input.GetMouseButtonDown(0))
		{
			if (playerData.gotShield == true)
				SetIsDefending(true);
		}

		if (Input.GetMouseButtonUp(0))
		{
			if (isDefending == true)
				SetIsDefending(false);
		}
	}

	void SetIsDefending(bool answer)
	{
		if (answer == true)
		{
			isDefending = true;

			shieldModel.GetComponent<Renderer>().enabled = true;

			shieldModel.GetComponent<Collider>().enabled = true;
		}
		else
		{
			isDefending = false;

			shieldModel.GetComponent<Renderer>().enabled = false;

			shieldModel.GetComponent<Collider>().enabled = false;
		}
	}

	public void morte() {
		source.PlayOneShot(morteClip, 1.0f);
		transform.position = playerData.PontoRespawn;
    }

	public bool hasAllRunes() {
		return (playerData.RunaAlgiz && playerData.RunaPerthro);
	}

	public void pegarRuneAlgiz() {
		playerData.RunaAlgiz = true;
		scripts.SendMessage ("AtivaAlgiz");
	}

	public void pegarRunePerthro() {
		playerData.RunaPerthro = true;
		scripts.SendMessage ("AtivaPerthro");
	}

	public void salvarDados() {
		GlobalControl.Instance.setPlayerData (playerData);
	}

	public void salvarRespawn(Vector3 respawn) {
        playerData.PontoRespawn = respawn;
    }

}
