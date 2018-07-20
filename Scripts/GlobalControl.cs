using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//https://www.sitepoint.com/saving-data-between-scenes-in-unity/
public class GlobalControl : MonoBehaviour 
{
	public static GlobalControl Instance;
	public int levelAtual = 0;
	public int ultimoLevel = 0;
	private PlayerData PlayerData = new PlayerData ();

	void Awake ()   
	{
		if (Instance == null) {
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	public void setPlayerData(PlayerData data) {
		PlayerData = data;
	}

	public PlayerData getPlayerData() {
		return PlayerData;
	}
}