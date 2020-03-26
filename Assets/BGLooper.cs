using UnityEngine;
using System.Collections;

public class BGLooper : MonoBehaviour {

	int numBGPanels = 8;

	float pipeMax = 3.11f;
	float pipeMin = 1.59f;

	void Start() {
		GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");
		
		foreach(GameObject pipe in pipes) {
			Vector3 pos = pipe.transform.position;
			pos.y = Random.Range(pipeMin, pipeMax);
			pipe.transform.position = pos;
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		Debug.Log ("Triggered: " + collider.name);

		float widthOfBGObject = ((BoxCollider2D)collider).size.x;
		
		Vector3 pos = collider.transform.position;

		//pos.x += widthOfBGObject * numBGPanels - widthOfBGObject/2f;
		pos.x += widthOfBGObject * numBGPanels;

		if(collider.tag == "Pipe") {
			pos.y = Random.Range(pipeMin, pipeMax);
		}

		collider.transform.position = pos;
	}
}
