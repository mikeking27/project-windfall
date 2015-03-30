using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour {

	public Object explosion;

	// Use this for initialization
	void Start () {
		Invoke ("Explode", 2f);
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Character") {
			Explode();
		}
	}

	public void Explode() {
		Instantiate (explosion, transform.position, Quaternion.identity);
		GameObject.Destroy (gameObject);
	}
}
