using UnityEngine;
using System.Collections;

public class ThirdPersonCameraController : MonoBehaviour {

	public Transform target;

	private Transform pivot;
	private Camera cam;
	private float cameraDistance;
	private RaycastHit raycastInfo;

	private float cameraBuffer = 0.5f;

	public LayerMask layerMask;

	public int playerNumber = 0;

	private float grappleDistance = 80f;

	private int invert = 1;

	// Use this for initialization
	void Start () {
		pivot = transform.GetChild (0);
		cam = transform.GetComponentInChildren<Camera> ();
		cameraDistance = Vector3.Distance (pivot.position, cam.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = target.position;

		if (Input.GetButtonDown ("Dup" + (playerNumber + 1)))
			invert *= -1;

		if (Input.GetButtonDown ("R" + (playerNumber + 1))) {
			if (Physics.Raycast (pivot.position, pivot.forward, out raycastInfo, grappleDistance, layerMask)) {
				target.GetComponent<UserControl>().grappleTarget = raycastInfo.point;
			} else {
				target.GetComponent<UserControl>().grappleTarget = Vector3.zero;
			}
		}

		var temp = pivot.localEulerAngles;
		temp.y += (Input.GetAxis ("CX"+(playerNumber+1)) * Time.deltaTime * 100f);
		// vary x rotation based on CY
		temp.x += (Input.GetAxis ("CY"+(playerNumber+1)) * Time.deltaTime * 100f * invert);
		if (temp.x > 180) {
			// set 300 limit
			temp.x = Mathf.Max (temp.x, 300);
		} else {
			// set 80 limit
			temp.x = Mathf.Min (temp.x, 80);
		}
		pivot.localEulerAngles = temp;

		// raycast backwards from pivot point by camera distance
		if (Physics.Raycast (pivot.position, -pivot.forward, out raycastInfo, cameraDistance + cameraBuffer, layerMask)) {
			cam.transform.localPosition = new Vector3(0f, 0f, -(raycastInfo.distance - cameraBuffer));
		} else {
			cam.transform.localPosition = new Vector3(0f, 0f, -cameraDistance);
		}
	}
}
