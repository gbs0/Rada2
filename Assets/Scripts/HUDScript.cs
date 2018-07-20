using UnityEngine;
using System.Collections;

public class HUDScript : MonoBehaviour {
	public GameObject algiz;
	public GameObject perthro;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void AtivaAlgiz(){
		algiz.SetActive (true);
	}

	void AtivaPerthro(){
		perthro.SetActive (true);
	}
}
