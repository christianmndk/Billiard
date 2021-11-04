using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kegleScrips : MonoBehaviour
{
	// Only used here
	private Rigidbody rb;
	private GameController GCS;
	private bool fallen = false;
	private bool landed = false;

	// Set in the editor
	public bool isCenter;
	public float atRestSpeedCutof = 0.5f; // 0.05
	public Vector3 resetPosition;
	public bool debug;

	// Used by other sripts

	
	// Start is called before the first frame update
	void Start()
	{
		rb = gameObject.GetComponent<Rigidbody>();
		GCS = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>();
	}
	// Speed og objekt 
	float vel;
	// Update is called once per frame
	void Update()
	{
		// Fordi vi ikke tager hastighed rotation med i regningen kan ny kode forløbe hurtigere end for ventet
		if(!fallen && landed)
		{
			vel = rb.velocity.magnitude;
			if (vel <= atRestSpeedCutof && (!rb.IsSleeping()))
			{
				if (RotationCheck())
				{
					print("FALLEN");
					fallen = true;
					GCS.keglerFallen += 1;
					// Print("stopped"); good tester
					if (isCenter)
					{
						GCS.centerKegleFallen = true;
					}
					rb.Sleep(); // Force the rb to sleep
				} else
				{
					//print("WENT TO SLEEP");
					rb.Sleep();
				}
				GCS.CheckEndTurn();
			}
		}
	}

	bool RotationCheck()
	{
		Vector3 angle = transform.rotation.eulerAngles;
		float x = angle.x;
		float z = angle.z;
		if (( 45.0f < x && x < 315.0f) || ( 45.0f < z && z < 315.0f ))
		{
			print(angle.ToString());
			return true;
		}
		else
		{
			if (debug) print(angle.ToString());
			return false;
		}
	}

	public void ResetPosition()
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		transform.rotation = Quaternion.identity;
		transform.position = resetPosition;
		landed = false;
		fallen = false;
	}

	private void OnCollisionEnter(Collision other)
	{
		landed = true;
	}
}
