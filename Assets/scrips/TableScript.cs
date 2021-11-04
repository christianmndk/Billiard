using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableScript : MonoBehaviour
{
	GameController GCS;
	public bool isGreen;
	// Start is called before the first frame update
	void Start()
	{
		GCS = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>();
		UpdateMaterial();
	}

	public void UpdateMaterial()
	{
		if (isGreen) gameObject.GetComponent<Renderer>().material = GCS.TheGreenMaterial;
		else gameObject.GetComponent<Renderer>().material = GCS.TheEdgeMaterial;
	}
}
