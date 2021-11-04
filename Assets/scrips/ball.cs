using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{

	// Only used here
	private float vel;
	private Rigidbody rb;
	private GameController GCS;
	private int FrameCounter = 1;

	// Set in Editor
	public float atRestSpeedCutof = 0.05f; // twice as high as kegler because of less friction
	public bool isMain;  

	void Start()
	{
		GCS = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>();
		rb = gameObject.GetComponent<Rigidbody>();
		UpdateMaterial();
	}

	void Update()
	{
		FrameCounter++;
		vel = rb.velocity.magnitude;
		if (vel <= atRestSpeedCutof && !rb.IsSleeping())
		{
			//print("BALL SLEEP");
			rb.Sleep();
			GCS.CheckEndTurn();
		}
		if (FrameCounter % 100 == 0)
		{
			GCS.CheckEndTurn();
			FrameCounter = 1;
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (isMain && other.gameObject.name.ToLower().Contains("kegle"))
		{
			GCS.fejl = true;
		}
		else if (isMain && other.gameObject.CompareTag("queue") || other.gameObject.tag == "queue")
		{
			print("hit by queue");
			GCS.begun = true;
		}
	}

	private void OnCollisionExit(Collision other)
	{
		if (other.gameObject.tag == "Bound")
		{
			transform.position = new Vector3(-10, 2, 0);
			rb.velocity = Vector3.zero;
			GCS.fejl = true;
		}
	}

	public void UpdateMaterial() 
	{
		if (isMain)
		{
			gameObject.GetComponent<Renderer>().material = GCS.MainBallMaterial;
		} 
		else
		{
			gameObject.GetComponent<Renderer>().material = GCS.SecBallMaterial;
		}  
	}
}
