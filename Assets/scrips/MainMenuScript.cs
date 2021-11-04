using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{

	public int MainMenuBuildIndex = 0;
	public int playSceneBuildIndex = 1;
	public int EndMenuSceneBuildIndex = 2;

	private GameObject GC;
	private GameController GCS;

	TMPro.TextMeshProUGUI textMesh;
	TMPro.TextMeshProUGUI pointTextMesh;
	TMPro.TextMeshProUGUI currentPlayerMesh;

	// Start is called before the first frame update
	void Start()
	{
		int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
		print(currentBuildIndex);
		print(EndMenuSceneBuildIndex);
		GetGameController();
		if (currentBuildIndex == 2) //EndMenuSceneBuildIndex)
		{
			print("found GameController");
			ShowWinner();
		}

		if (currentBuildIndex == playSceneBuildIndex)
		{
			pointTextMesh = GameObject.FindGameObjectsWithTag("PointText")[0].GetComponent<TMPro.TextMeshProUGUI>();
			currentPlayerMesh = GameObject.Find("CurrentPlayerText").GetComponent<TMPro.TextMeshProUGUI>();
			GCS.InitPlayUI();
		}
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void GetGameController()
	{
		GC = GameObject.FindGameObjectsWithTag("GameController")[0];
		GCS = GC.GetComponent<GameController>();
	}

	public void UpdatePointsText()
	{
		string UpdatedPointString = GCS.PointString;

		for (int i = 1; i <= GCS.PlayerAmount; i++)
		{
			UpdatedPointString = UpdatedPointString.Replace(i.ToString() + "X", GCS.Players[i - 1].points.ToString());
		}
		pointTextMesh.text = UpdatedPointString;

	}

	public void UpdateCurrentPlayer(int playerIndex)
	{
		currentPlayerMesh.text = "Player " + (playerIndex + 1).ToString() + "'s turn";
	}

	void ShowWinner()
	{
		for (int i = 0; i < GCS.PlayerAmount; i++)
		{
			if (GCS.Players[i].won)
			{
				textMesh = GameObject.FindGameObjectsWithTag("WinnerText")[0].GetComponent<TMPro.TextMeshProUGUI>();
				textMesh.text = "Player " + (i + 1).ToString();
			}
		}
			
	}

	public void ChangeSceneToPlay()
	{
		SceneManager.LoadScene(playSceneBuildIndex);
	}

	public void NewGame()
	{
		GCS.NewTurn(true);
		GCS.CurrentPlayer = 0;
		SceneManager.LoadScene(0);//MainMenuBuildIndex);
	}

	public void ShowSettings()
	{
		Time.timeScale = 0f;
	}

	public void QuitGame()
	{
		print("Should quit the game");
		Application.Quit();
	}
}
