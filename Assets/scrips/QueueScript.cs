using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class QueueScript : MonoBehaviour
{
	// Start is called before the first frame update
	int MUTI = 5000;
	InputSystem I;
	public Rigidbody QueueRB;
	public Rigidbody BallRB;
	private GameObject queue;
	private GameObject ball;

	public float OpladeTime;
	public bool allowToShoot = true;
	public Vector3 resetPosition;
	private GameObject mainCam;
	private GameObject queueCam;
	void Awake()
	{

		I = new InputSystem();
		//I.Queue.opladeQueue.performed += CollectionExtensions => Oplader();
		I.Queue.opladeQueue.started += ctx => OnClick();
		//I.Queue.opladeQueue.performed += ctx => Oplader();
		I.Queue.opladeQueue.canceled += ctx => stopped();

		I.Queue.turnQueue.performed += ctx => QueueRotate(ctx);
		
	}
	void Start()
	{
//get/set cameras
		mainCam = GameObject.Find("MainCam");
		queueCam = GameObject.Find("QueueCam");

		setCam(1);
		queue = GameObject.FindGameObjectsWithTag("queue")[0];
		ball = GameObject.Find("mainball");


		QueueRB = GameObject.FindGameObjectsWithTag("queue")[0].GetComponent<Rigidbody>();
		BallRB = gameObject.GetComponent<Rigidbody>();
	}
	void OnEnable()
	{
		I.Queue.Enable();
	}
	void OnDisable()
	{
		I.Queue.Disable();
	}

	// Update is called once per frame
	void Update()
	{
		
	}
	public void nytskud()	
	{
		print("new shot");
		setCam(1);
		ResetPosition();
		queue.SetActive(true);

	}
	private void skyd()
	{
		setCam(0);
		QueueRB.AddRelativeForce(Vector3.down * OpladeTime * MUTI);
		//Vector3(0,0,0);
		
	}
	public void OnClick()
	{
		if(allowToShoot)
		{
			OpladeTime = 0f;
			
			/*   
			BallRB.rotation = new Vector3(0,0,0));
			QueueRB.rotation(Vector3(0,0,0));

			QueueRB.transform(Vector3(-3.5,1,0));*/

			StartCoroutine("Oplade");
		}
	}
	private void OnCollisionExit(Collision other)
	{
		if(other.gameObject.name == "Queue")
		{
			other.gameObject.SetActive(false);
		}
	}
	public void stopped()
	{
		StopCoroutine("Oplade");
		skyd();
		setCam(0);
		print("queueCam");
	}
	IEnumerator WaitToDisable()//disable queue after hit with ball
	{
		yield return new WaitForSeconds(1);
		queue.SetActive(false);
	}

	IEnumerator Oplade()
	{
		while(true)
		{
			OpladeTime += 0.05f;
			
			//print(OpladeTime);
			yield return new WaitForSeconds(0.05F);
		}
	}
	public void ResetPosition()
	{
		BallRB.velocity = Vector3.zero;
		transform.rotation = Quaternion.identity;
		BallRB.angularVelocity = Vector3.zero;

		queue.transform.localPosition = new Vector3(-7.39f,0.57f,0);
		QueueRB.velocity = Vector3.zero;
		QueueRB.angularVelocity = Vector3.zero;
		queue.transform.eulerAngles = new Vector3(0,0,85) ;
	}
	
	void setCam(int Camindex = 0)// 0==mainCam, 1 == queueCam
	{
		if(Camindex == 0)
		{
			mainCam.GetComponent<Camera>().enabled = true;
			queueCam.GetComponent<Camera>().enabled = false;
		}
		else if (Camindex == 1)
		{
			queueCam.GetComponent<Camera>().enabled = true;
			mainCam.GetComponent<Camera>().enabled = false;
		}
	}

	void QueueRotate(InputAction.CallbackContext ctx)
	{
		float v1 = ctx.ReadValue<float>();
		//print(v1);
		BallRB.transform.eulerAngles += new Vector3(0,v1,0);
	}
}
