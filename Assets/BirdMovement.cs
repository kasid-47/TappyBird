using UnityEngine;
using System.Collections;

public class BirdMovement : MonoBehaviour {

	Vector3 velocity = Vector3.zero;
	//public Vector3 gravity;
	//public Vector3 flapVelocity;
	//public float maxSpeed = 5f;
	public float flapSpeed = 100f;
	public float forwardSpeed = 2.2f;
	public AudioClip flapSound,deathSound;

	bool didFlap = false;
	bool deathPlayed = false;

	Animator animator;
	public bool dead = false;
	float deathCooldown;

	public bool godMode = false;

	// Use this for initialization
	void Start () {
		animator = transform.GetComponentInChildren<Animator>();
		
		if(animator == null) {
			Debug.LogError("Didn't find animator!");
		}
	}

	//Graphics & Input updates here
	void Update() {
		if(dead) {
			deathCooldown -= Time.deltaTime;
			
			if(deathCooldown <= 0) {
				if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ) {
					Application.LoadLevel( Application.loadedLevel );
					deathPlayed = false;
				}
			}
		}
		else {
			if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ) {
				didFlap = true;
				AudioSource.PlayClipAtPoint(flapSound,transform.position);
			}
		}
	}

	// Physics Engine updates here
	void FixedUpdate () {

		if(dead)
			return;

		GetComponent<Rigidbody2D>().AddForce (Vector2.right * forwardSpeed);

		if (didFlap) {
			GetComponent<Rigidbody2D>().AddForce (Vector2.up * flapSpeed);
			animator.SetTrigger("DoFlap");
			didFlap = false;
		}

		if (GetComponent<Rigidbody2D>().velocity.y > 0) {
			transform.rotation = Quaternion.Euler(0, 0, 0);
		}
		else {
			float angle = Mathf.Lerp (0, -90, (-GetComponent<Rigidbody2D>().velocity.y / 3f) );
			transform.rotation = Quaternion.Euler(0, 0, angle);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if(godMode)
			return;
		
		animator.SetTrigger("Death");
		dead = true;
		deathCooldown = 0.5f;
		if (!deathPlayed) {
			AudioSource.PlayClipAtPoint (deathSound, transform.position);
			deathPlayed = true;
		}
	}

	/*void FixedUpdate () {
		velocity.x = forwardSpeed;
		//velocity += gravity * Time.deltaTime;

		if(didFlap == true){
				didFlap = false;
				if(velocity.y < 0)
					velocity.y = 0;
				velocity += flapVelocity;
		}

		velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

		rigidbody2D.AddForce (velocity);
		
		//transform.position += velocity * Time.deltaTime;

		float angle = 0;
		if (velocity.y < 0) {
			angle = Mathf.Lerp (0, -90, -velocity.y / maxSpeed);
		}
		transform.rotation = Quaternion.Euler(0, 0, angle);
	}*/
}
