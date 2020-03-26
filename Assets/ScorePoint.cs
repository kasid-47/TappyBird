using UnityEngine;
using System.Collections;

public class ScorePoint : MonoBehaviour {

	public AudioClip scoreSound;

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag == "Player") {
			Score.AddPoint();
			UnityEngine.AudioSource.PlayClipAtPoint (scoreSound, transform.position);
		}
	}
}
