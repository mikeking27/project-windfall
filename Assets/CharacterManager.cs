using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterManager : MonoBehaviour {

	public int numberOfPlayers = 4;

	public Color32[] playerColors;
	public Material[] playerMaterials;
	public Transform[] spawnPoints;

	public Object cameraRig;
	public Object character;

	// Use this for initialization
	void Start () {

		GameObject[] cameraRigs = new GameObject[numberOfPlayers];
		GameObject[] characters = new GameObject[numberOfPlayers];

		for (int i = 0; i < numberOfPlayers; i++) {
			// create character
			characters[i] = Instantiate(character, spawnPoints[i].position, Quaternion.identity) as GameObject;
			// create camera
			cameraRigs[i] = Instantiate(cameraRig, spawnPoints[i].position, Quaternion.identity) as GameObject;
			// set attributes
			characters[i].GetComponent<UserControl>().m_Cam = cameraRigs[i].transform.GetChild(0).GetChild(0);
			characters[i].GetComponent<UserControl>().color = playerColors[i];
			characters[i].GetComponent<UserControl>().playerNumber = i;
			characters[i].GetComponent<LineRenderer>().SetColors(playerColors[i], playerColors[i]);
			characters[i].transform.GetChild (0).GetComponent<SkinnedMeshRenderer>().materials = new Material[] { playerMaterials[i] };
			cameraRigs[i].GetComponent<ThirdPersonCameraController>().target = characters[i].transform;
			cameraRigs[i].GetComponent<ThirdPersonCameraController>().playerNumber = i;
			if (numberOfPlayers > 2) {
				cameraRigs[i].GetComponentInChildren<CanvasScaler>().scaleFactor = 0.5f;
			}
		}

		switch (numberOfPlayers) {
		case 1:
			cameraRigs[0].transform.GetChild (0).GetChild (0).GetComponent<Camera>().rect = new Rect(0f, 0f, 1f, 1f);
			cameraRigs[0].transform.GetChild (0).GetChild (1).GetComponent<Camera>().rect = new Rect(0f, 0f, 1f, 1f);
			break;
		case 2:
			cameraRigs[0].transform.GetChild (0).GetChild (0).GetComponent<Camera>().rect = new Rect(0f, 0.5f, 1f, 0.5f);
			cameraRigs[0].transform.GetChild (0).GetChild (1).GetComponent<Camera>().rect = new Rect(0f, 0.5f, 1f, 0.5f);
			cameraRigs[1].transform.GetChild (0).GetChild (0).GetComponent<Camera>().rect = new Rect(0f, 0f, 1f, 0.5f);
			cameraRigs[1].transform.GetChild (0).GetChild (1).GetComponent<Camera>().rect = new Rect(0f, 0f, 1f, 0.5f);
			break;
		case 3:
			cameraRigs[0].transform.GetChild (0).GetChild (0).GetComponent<Camera>().rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
			cameraRigs[0].transform.GetChild (0).GetChild (1).GetComponent<Camera>().rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
			cameraRigs[1].transform.GetChild (0).GetChild (0).GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
			cameraRigs[1].transform.GetChild (0).GetChild (1).GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
			cameraRigs[2].transform.GetChild (0).GetChild (0).GetComponent<Camera>().rect = new Rect(0f, 0f, 0.5f, 0.5f);
			cameraRigs[2].transform.GetChild (0).GetChild (1).GetComponent<Camera>().rect = new Rect(0f, 0f, 0.5f, 0.5f);
			break;
		case 4:
			cameraRigs[0].transform.GetChild (0).GetChild (0).GetComponent<Camera>().rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
			cameraRigs[0].transform.GetChild (0).GetChild (1).GetComponent<Camera>().rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
			cameraRigs[1].transform.GetChild (0).GetChild (0).GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
			cameraRigs[1].transform.GetChild (0).GetChild (1).GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
			cameraRigs[2].transform.GetChild (0).GetChild (0).GetComponent<Camera>().rect = new Rect(0f, 0f, 0.5f, 0.5f);
			cameraRigs[2].transform.GetChild (0).GetChild (1).GetComponent<Camera>().rect = new Rect(0f, 0f, 0.5f, 0.5f);
			cameraRigs[3].transform.GetChild (0).GetChild (0).GetComponent<Camera>().rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
			cameraRigs[3].transform.GetChild (0).GetChild (1).GetComponent<Camera>().rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
			break;
		}
	}
	
	// Update is called once per frame
//	void Update () {
	
//	}
}
