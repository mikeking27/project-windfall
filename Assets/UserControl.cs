using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class UserControl : MonoBehaviour {

	private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
	public Transform m_Cam;                  // A reference to the main camera in the scenes transform
	private Image[] cooldownUI;
	private Vector3 m_CamForward;             // The current forward direction of the camera
	private Vector3 m_Move;
	private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
	private bool m_Jetpack;
	private bool crouched = false;
	private float h;
	private float v;

	public Object grenade;
	private float grenadeTimer = 0f;
	private float grenadeCooldown = 3f;

	private float jetpackFuel = 10f;
	private float jetpackFuelMax = 10f;
	private float jetpackFuelBurnRate = 1f;
	private float jetpackFuelRechargeRate = 2f;

	public int playerNumber = 0;
	public Color32 color;
	
	public ParticleSystem[] jets;
	public Vector3 grappleTarget;

	private LineRenderer grapple;
	private Rigidbody rig;

	enum cooldowns {
		jets,
		grenade
	}

	void Start()
	{
		// get the third person character ( this should never be null due to require component )
		m_Character = GetComponent<ThirdPersonCharacter>();
		Input.ResetInputAxes ();
		grapple = GetComponent<LineRenderer> ();
		rig = GetComponent<Rigidbody> ();
		cooldownUI = new Image[] {
			m_Cam.parent.GetChild (1).GetChild (0).GetChild (0).GetComponent<Image>(),
			m_Cam.parent.GetChild (1).GetChild (0).GetChild (1).GetComponent<Image>()
		};
	}
	
	
	void Update()
	{
		if (!m_Jump)
		{
			m_Jump = Input.GetButtonDown("A"+(playerNumber+1));
		}
		if (Input.GetButtonDown ("B" + (playerNumber + 1))) {
			// melee attack?
		}


		if (Input.GetButtonDown ("X" + (playerNumber + 1))) {
			if (grenadeTimer < Time.time) {
				grenadeTimer = Time.time + grenadeCooldown;
				var nade = Instantiate(grenade, transform.FindChild("SpawnPoint").position, Quaternion.identity) as GameObject;
				nade.GetComponent<Rigidbody>().AddForce(transform.forward * 100f);
			}
		}
		if (Input.GetButtonDown ("Y" + (playerNumber + 1))) {
			// dash
		}

		// jetpack
		m_Jetpack = false;
		if (Input.GetButton ("L" + (playerNumber + 1))) {
			jetpackFuel -= jetpackFuelBurnRate * Time.deltaTime;
			if (jetpackFuel < 0f) {
				jetpackFuel = 0f;
			} else {
				m_Jetpack = true;
			}
		} else if (m_Character.m_IsGrounded) {
			jetpackFuel += jetpackFuelRechargeRate * Time.deltaTime;
			if (jetpackFuel > jetpackFuelMax)
				jetpackFuel = jetpackFuelMax;
		}

		if (Input.GetButtonDown ("Z" + (playerNumber + 1))) {
			if (grenadeTimer < Time.time) {
				grenadeTimer = Time.time + grenadeCooldown;
				var nade = Instantiate(grenade, transform.FindChild("SpawnPoint").position, Quaternion.identity) as GameObject;
				nade.GetComponent<Rigidbody>().AddForce(transform.forward * 1000f);
			}
		}
		if (Input.GetButtonDown ("R" + (playerNumber + 1))) {
			// start grappling hook?
			
		}
		if (Input.GetButton ("R" + (playerNumber + 1))) {
			// keep force going
		} else {
			grappleTarget = Vector3.zero;
		}

		if (Input.GetButtonDown ("start"+(playerNumber+1)))
			Debug.Log ("start");

//		if (Input.GetButtonDown ("Dup"+(playerNumber+1)))
//			Debug.Log ("Dup");
		if (Input.GetButtonDown ("Dright"+(playerNumber+1)))
			Debug.Log ("Dright");
		if (Input.GetButtonDown ("Ddown"+(playerNumber+1)))
			crouched = !crouched;
		if (Input.GetButtonDown ("Dleft"+(playerNumber+1)))
			Debug.Log ("Dleft");

		// update UI
		cooldownUI [(int)cooldowns.jets].fillAmount = jetpackFuel / jetpackFuelMax;
		cooldownUI [(int)cooldowns.grenade].fillAmount = 1f - Mathf.Max (0f, grenadeTimer - Time.time) / grenadeCooldown;
	}
	
	
	// Fixed update is called in sync with physics
	void FixedUpdate()
	{
		h = Input.GetAxis ("leftX"+(playerNumber+1));
		v = Input.GetAxis ("leftY"+(playerNumber+1));
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
		if (jets != null) {
			foreach (var jet in jets) {
				if (jet != null)
					jet.enableEmission = m_Jetpack;
			}
		}
		if (m_Jetpack) {
			Vector3 jet = m_Move.normalized;
			jet += Vector3.up;
			jet.Normalize();
			rig.AddForce (20f * jet);
		}
		if (grappleTarget != null && grappleTarget != Vector3.zero) {
			Vector3 current = transform.position + Vector3.up * 0.8f;
			grapple.SetPosition (0, current);
			grapple.SetPosition (1, grappleTarget);
			grapple.enabled = true;
			rig.AddForce ((grappleTarget - current).normalized * 40f);
		} else {
			grapple.enabled = false;
		}
		m_Jump = false;
	}
}
