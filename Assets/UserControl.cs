using UnityEngine;
using System.Collections;
//using InControl;
using UnityStandardAssets.Characters.ThirdPerson;

public class UserControl : MonoBehaviour {

	private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
	public Transform m_Cam;                  // A reference to the main camera in the scenes transform
	private Vector3 m_CamForward;             // The current forward direction of the camera
	private Vector3 m_Move;
	private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
	private bool crouched = false;
	private float h;
	private float v;
	
	public int playerNumber = 0;
	
	
	void Start()
	{
		// get the third person character ( this should never be null due to require component )
		m_Character = GetComponent<ThirdPersonCharacter>();
		Input.ResetInputAxes ();
	}
	
	
	void Update()
	{
		if (!m_Jump)
		{
			m_Jump = Input.GetButtonDown("A"+(playerNumber+1));
		}
		if (Input.GetButtonDown("B"+(playerNumber+1)))
			crouched = !crouched;

		if (Input.GetButtonDown ("X"+(playerNumber+1)))
		    Debug.Log ("X");
		if (Input.GetButtonDown ("Y"+(playerNumber+1)))
		    Debug.Log ("Y");
		if (Input.GetButtonDown ("L"+(playerNumber+1)))
			Debug.Log ("L");
		if (Input.GetButtonDown ("R"+(playerNumber+1)))
			Debug.Log ("R");
		if (Input.GetButtonDown ("Z"+(playerNumber+1)))
			Debug.Log ("Z");

		if (Input.GetButtonDown ("start"+(playerNumber+1)))
			Debug.Log ("start");

		if (Input.GetButtonDown ("Dup"+(playerNumber+1)))
			Debug.Log ("Dup");
		if (Input.GetButtonDown ("Dright"+(playerNumber+1)))
			Debug.Log ("Dright");
		if (Input.GetButtonDown ("Ddown"+(playerNumber+1)))
			Debug.Log ("Ddown");
		if (Input.GetButtonDown ("Dleft"+(playerNumber+1)))
			Debug.Log ("Dleft");

	}
	
	
	// Fixed update is called in sync with physics
	void FixedUpdate()
	{
		h = Input.GetAxis ("leftX"+(playerNumber+1)) * 2f;
		v = Input.GetAxis ("leftY"+(playerNumber+1)) * 2f;
		// calculate move direction to pass to character
		if (m_Cam != null)
		{
			// calculate camera relative direction to move:
			m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
			m_Move = v*m_CamForward + h*m_Cam.right;
		}
		else
		{
			// we use world-relative directions in the case of no main camera
			m_Move = v*Vector3.forward + h*Vector3.right;
		}

		// pass all parameters to the character control script
		m_Character.Move(m_Move, crouched, m_Jump);
		m_Jump = false;
	}
}
