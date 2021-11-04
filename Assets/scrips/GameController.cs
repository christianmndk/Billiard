using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class GameController : MonoBehaviour
{
	// denne skal ændres for at kunne sende web requests til philips hue lamper
	public bool notOnSchool = true;
	// Set in editor
	public int playSceneBuildIndex = 1;
	public int EndMenuSceneBuildIndex = 2;

	public int pointsPrKegle = 2;
	public int pointsForCenter = 4;
	public int PlayerAmount = 2;
	public int PointsToWin = 2;

	public string[] MaterialNames;

	// Used by other scripts 
	public int keglerFallen = 0;
	public bool centerKegleFallen = false;
	public bool fejl = false;
	public bool begun = false;
	public string PointString;

	public Material MainBallMaterial;
	public Material TheGreenMaterial;
	public Material TheEdgeMaterial;
	public Material SecBallMaterial;

	public Player[] Players;
	public int CurrentPlayer = 0;

	// Used only here
	private GameObject UI;
	private MainMenuScript UIS;
	private QueueScript NSC;
	private ball[] BASA;
	private TableScript[] TASA;
	private kegleScrips[] KESA;

	private Rigidbody[] DynamicObjects = new Rigidbody[0]; // List of objects which should not be moving when we end the turn 
	private string hueLogin = "http://192.168.50.206/api/HzfrvP7Kt0cvN4hcNiMx9ypGPsnqioveEPwfpVM3/";

	void Awake()
	{
		
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void GetKegleScripts()
	{
		if (KESA == null)           KESA = GameObject.FindGameObjectWithTag("Kegler").GetComponentsInChildren<kegleScrips>();
		else if (KESA[0] == null)   KESA = GameObject.FindGameObjectWithTag("Kegler").GetComponentsInChildren<kegleScrips>();
	}

	void ResetKeglePosition()
	{
		GetKegleScripts();
		for (int i = 0; i < KESA.Length; i++)
		{
			KESA[i].ResetPosition();
		}
	}

	void GetMaterialScripts()
	{
		if (BASA == null)           GetBAS();
		else if (BASA[0] == null)   GetBAS();

		if (TASA == null)           GetTAS();
		else if (TASA[0] == null)   GetTAS();
	}

	void GetBAS()
	{
		BASA = new ball[3];
		BASA[0] = GameObject.Find("mainball").GetComponent<ball>();
		BASA[1] = GameObject.Find("kugleXP").GetComponent<ball>();
		BASA[2] = GameObject.Find("kugleXN").GetComponent<ball>();
	}

	void GetTAS()
	{
		TASA = GameObject.FindGameObjectWithTag("Table").GetComponentsInChildren<TableScript>();
	}

	public void UpdateMaterials()
	{
		GetMaterialScripts();
		for (int i = 0; i < BASA.Length; i++)
		{
			BASA[i].UpdateMaterial();
		}
		for (int i = 0; i < TASA.Length; i++)
		{
			TASA[i].UpdateMaterial();
		}
	}

	int _Random(int min, int max)
	{
		return Mathf.FloorToInt(Mathf.Min(Random.Range(min, max), max - 0.1f));
	}

	public Material RandomMaterial()
	{
		int i = _Random(0, MaterialNames.Length);
		Material newMaterial = Resources.Load<Material>(MaterialNames[i]);
		if (newMaterial == null) print("Found no material in Resources with the name: " + MaterialNames[i]);
		return newMaterial;
	}

	public void ChangeBallAppearance()
	{
		MainBallMaterial = RandomMaterial();
		SecBallMaterial = RandomMaterial();
		UpdateMaterials();
	}

	public void ChangeTableAppearance()
	{
		TheGreenMaterial = RandomMaterial();
		TheEdgeMaterial = RandomMaterial();
		UpdateMaterials();
	}
	public void InitPlayUI()
	{
		UI = GameObject.FindGameObjectsWithTag("UI")[0];
		UIS = UI.GetComponent<MainMenuScript>();
		NSC = GameObject.Find("mainball").GetComponent<QueueScript>(); // Piggy back rides this function
		Players = new Player[PlayerAmount];
		for (int i = 0; i < PlayerAmount; i++) { 
			 Players[i] = new Player();
		}

		PointString = InitPlayerPointText(PlayerAmount);
		UIS.UpdatePointsText();
		UIS.UpdateCurrentPlayer(CurrentPlayer);
	}

	string InitPlayerPointText(int amount)
	{
		string baseString = "";
		for (int i = 1; i <= amount; i++)
		{
			string it = i.ToString(); 
			baseString += "P" + it + ": " + it + "X\n";
		}
		//print(baseString);
		return baseString;
	}

	int CalculatePoints()
	{
		int points;
		// Beregn points
		points = pointsPrKegle * keglerFallen;
		if ((points == 0))
		{
			if (centerKegleFallen) points = pointsForCenter;
			else fejl = true;
		} 

		// Håndter hvis man har lavet fejl
		if (fejl)
		{
			points *= -1;
			points -= 2;
		}

		return points;
	}

	void GivePoints(int points)
	{
		// Give the player the points once
		Players[CurrentPlayer].points += points;
		if (points < 0)
		{
			// If the points was negative add the positive points to all
			for (int i = 0; i < PlayerAmount; i++)
			{
				Players[i].points -= points;
			}
		}
		UIS.UpdatePointsText();
		CheckWin();
	}

	byte[] ToByte(string str)
	{
		return System.Text.Encoding.UTF8.GetBytes(str);
	}

	IEnumerator UpdateLights(string lights)
	{
		if(notOnSchool)
		{
			byte[] hueOrdre;
			if (keglerFallen == 0)
			{
				hueOrdre = ToByte("{\"on\":false}");
			}
			else
			{
				string HueLevel = (Mathf.Floor(65536 / 5 * (keglerFallen - 1))).ToString();
				hueOrdre = ToByte("{\"on\":true,\"hue\":" + HueLevel + "}");
			}
			UnityWebRequest Put = UnityWebRequest.Put(hueLogin + lights, hueOrdre);
			yield return Put.SendWebRequest();
			if (Put.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError(Put.error);
			}
			else
			{
				//print("working fine");
			}
		}
	}

	void NextPlayer()
	{
		CurrentPlayer += 1;
		if (CurrentPlayer >= PlayerAmount)
		{
			CurrentPlayer = 0;
		}
		UIS.UpdateCurrentPlayer(CurrentPlayer);
	}

	void CheckWin()
	{
		for (int i = 0; i < PlayerAmount; i++)
		{
			if (Players[i].points >= PointsToWin)
			{
				Players[i].won = true;
				print("Player: " + (i + 1).ToString() + " won");
				SceneManager.LoadScene(EndMenuSceneBuildIndex);
			}
		}
	}

	void CheckDynamicObjects()
	{
		if (DynamicObjects.Length != 6)
		{
			GameObject[] OBS = GameObject.FindGameObjectsWithTag("Dynamic");
			DynamicObjects = new Rigidbody[OBS.Length];
			for (int i = 0; i < OBS.Length; i++)
			{
				DynamicObjects[i] = OBS[i].GetComponent<Rigidbody>();
			}
		}
	}

	bool CheckMovement()
	{
		bool NotMoving = true;
		CheckDynamicObjects();
		int i = 0;
		foreach (Rigidbody RB in DynamicObjects)
		{
			i++;
			NotMoving = NotMoving && RB.IsSleeping();
		}
		// return true if we are moving
		return !NotMoving;
	}

	public bool CheckEndTurn()
	{
		StartCoroutine(UpdateLights("lights/29/state"));
		if (!CheckMovement() && begun) {
			EndTurn();
			return true;
		}
		else
		{
			return false;
		}
	}

	void EndTurn()
	{
		print("Turn Ended");
		print(keglerFallen);
		GivePoints(CalculatePoints());
		NSC.nytskud();
		NewTurn(false);
		NextPlayer();
		StartCoroutine(UpdateLights("lights/29/state"));
	}

	public void NewTurn(bool trueEnd)
	{
		// Just to be sure they are actually set to zero when we start a new game
		keglerFallen = 0;
		centerKegleFallen = false;
		fejl = false;
		begun = false;
		if (!trueEnd) { ResetKeglePosition(); }
		
	}

	public class Player
	{
		public int points = 0;
		public bool won = false;
	}
}