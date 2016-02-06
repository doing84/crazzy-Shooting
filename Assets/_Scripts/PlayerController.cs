using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	// PUBLIC INSTANCE VAL.
	public float fireRate = 0.0f;
	public int playerLife = 3;

	// PRIVATE INSTANCE VAL.
	private float nextFire;
	private AudioSource _shootAudioSource;

	// Player Based Component
	Spaceship spaceship;
    	
	// Use this for initialization
	void Start() {
		spaceship = GetComponent<Spaceship> ();
		this._shootAudioSource = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		Vector2 direction = _CheckInput ();
		Move (direction);

		// Check a press key.
		if (Input.GetKey("q") && Time.time > nextFire){
			nextFire = Time.time + fireRate;
			spaceship.Shot (transform);
			this._shootAudioSource.Play ();
		}
	}

	// Input.
	private Vector2 _CheckInput() {
		float x = Input.GetAxisRaw ("Horizontal");
		float y = Input.GetAxisRaw ("Vertical");
		return new Vector2 (x, y).normalized;
	}

	// Manage the Player Movement in Update()
	private void Move (Vector2 direction) {
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
		Vector2 pos = transform.position;

		pos += direction  * spaceship.speed * Time.deltaTime;
		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);

		transform.position = pos;

	}
	
	// Check the Collider
	void OnTriggerEnter2D (Collider2D c) {
		string layerName = LayerMask.LayerToName(c.gameObject.layer);

		// Crash enemy bullet and enemy
		if( layerName == "Bullet (Enemy)") {
			Destroy(c.gameObject);
		}

		if( layerName == "Bullet (Enemy)" || layerName == "Enemy"){
			Destroy(c.gameObject);
			this.playerLife--;

			// Display Player Life
			int localPlayerLife = this.playerLife % 3;
			if( localPlayerLife == 2) {
				
			}
			if( localPlayerLife == 1) {
				
			}

			spaceship.Explosion();


			if( playerLife <= 0 ) {
				FindObjectOfType<GameManager>().GameOver();
				Destroy (gameObject);
			}
		}
	}
}