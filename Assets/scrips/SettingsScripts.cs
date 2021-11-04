using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScripts : MonoBehaviour
{
	// Used by other scripts

	GameController GCS;

	// Start is called before the first frame update
	void Start()
	{
		GCS = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>();
	}

	public void ChangeBallAppearance()
	{
		print("Changing the appearance of the ball");
		GCS.ChangeBallAppearance();

	}

	public void ChangeTableAppearance()
	{
		print("Changing the appearance of the table");
		GCS.ChangeTableAppearance();
	}

	public void ShowMain()
	{
		Time.timeScale = 1f;
	}
}
